﻿//--------------------------------------------------------------------------------------------
//File:   Games.cs
//Desc:   This class contains logic and information for the Game.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binder.Environment;
using System.IO;
using System.ComponentModel;
using System.Media;

namespace Binder
{
    public class Game: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        SoundPlayer BSoundPlayer;       //Holds Reference to SoundPlayer object

        public Player Marcus { get; set; }      //Holds the instance of the player
        private int currScore;                  
        public int CurrScore
        {
            get
            {
                return currScore;
            }
            set
            {
                currScore = value;
                SetProperty("CurrScore");
            }
        }         //Keeps track of the current score as player plays
        public int HighScore { get; set; }          //Keeps track of the High Score so far
        public int Composure { get; set; }          //Keeps track of the health of the Player
        public double Time { get; set; }               //Keeps track of the amount of time remaining
        public int NumItems { get; set; }           //Keeps track of the number of items in players inventory
        public bool IsCheatOn { get; set; }         //Determines whether or not the cheat mode should be on
        public static int Difficulty { get; set; }         //Holds difficulty level
        public static List<WorldObject> Environ { get; set; }    //Holds current game objects
        public Building CurBuilding { get; set; }          //Holds the current building reference
        public int LevelNum { get; set; }                  //Holds the current level number
        public bool isPauseScreenShown = false;            //Tells whether the pause screen is currently shown

        private int totalScore;
        public int TotalScore {
            get
            {
                return totalScore;
            }
            set
            {
                totalScore = value;
                SetProperty("TotalScore");
            }
        }

        private string currLevel;
        public string CurrLevel                        //Holds information on the current level
        {
            get
            {
                return currLevel;
            }
            set
            {
                currLevel = "Level " + value;
                SetProperty("CurrLevel");
            }
        }                                            
        public static bool isPaused { get; set; }    //Determines if the game is paused
        public BinderRing ring;                      //Current binder ring
        public bool isRingFound;                     //Determines if the player has found the ring
        public static List<InventoryItem> itemsHeld = new List<InventoryItem>();   //Items currently held by the player
        public int currentItem = 0;                  //Shows item that currently needs to be used
        public int PsiZetaShamed = 0;                //Holds number of AI characters that have been defeated 
        private int min = 2;                         //Gives number of starting minutes
        private int sec = 60;                        //Gives number of starting seconds
        private string time;
        public string TimeLeft                       //Holds current remaining time display

        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                SetProperty("TimeLeft");
            }
        }                                         
        public Game(double startTime, int level)
        {
            Time = startTime;
            if(Time == 0)
            {
                min = 0;
            }
            else
            {
                min = (int)Time / 60 - 1;
            }           
            
            Marcus = new Player("Marcus");
            Environ = new List<WorldObject>();
            isPaused = false;

            LevelNum = level;
            NLevel(LevelNum);

            ring = new BinderRing();
            ring.X = 700;
            ring.Y = 450;
            Environ.Add(ring);
            MakeItems(level);
            MakeAIPerLevel();

            Play("GamePlay.wav");
        }

        public Game()
        {
            Marcus = new Player("");
            Environ = new List<WorldObject>();
        }

        //Time Logic
        public void DecrTime()
        {
            if(sec == 0 && min != 0)
            {
                min -= 1;
                sec = 59;
            }
            else
            {
                sec -= 1;
                Time -= 1;
            }

            string minutes = min.ToString();
            string seconds = sec.ToString();

            if(seconds.Length == 1)
            {
                seconds = "0" + seconds;
            }

            TimeLeft = "Time: 0" + min + ":" + seconds;            
        }

        //Plays the sound from the source passed in its parameters.
        public void Play(string sound)
        {
            BSoundPlayer = new SoundPlayer(sound);
            BSoundPlayer.Play();                   
        }

        //Creates new level based the number in params
        public void NLevel(int level)
        {
            Dictionary<int, List<int[]>> Plans = new Dictionary<int, List<int[]>>();
            Plans[1] = Building.FAPlans;
            Plans[2] = Building.LibPlans;
            Plans[3] = Building.Maze;
           

            CurBuilding = new Building();

            CurBuilding.BuildWalls(Plans[level]);

            Environ = null;

            Environ = new List<WorldObject>();

            Environ.AddRange(CurBuilding.WallsCol);
                
            switch (level){
                case 2:
                    CurBuilding.Name = "2: Macey's Library";
                    break;
                case 3:
                    CurBuilding.Name = "3: Menacing Maze";
                    break;
                default:
                    CurBuilding.Name = "1: Finest Artists";
                    break;
            }

            CurrLevel = CurBuilding.Name;
        }

        //Creaated Load method with initial loading algorithm
        public void Load(string filename)
        {
            using (StreamReader rd = new StreamReader(filename))
            {
                string line = rd.ReadLine();

                while (line != "END")
                {
                    string[] identify = line.Split(',', '!', '#', ':', '?', ';');
                    switch (identify[0])
                    {
                        case "CURRSCORE":
                            CurrScore = int.Parse(line.Split('!')[1]);
                            break;
                        case "HIGHSCORE":
                            HighScore = int.Parse(line.Split('!')[1]);
                            break;
                        case "TOTALSCORE":
                            TotalScore = int.Parse(line.Split('!')[1]);
                            break;
                        case "COMPOSURE":
                            Composure = int.Parse(line.Split('!')[1]);
                            break;
                        case "NUMITEMS":
                            NumItems = int.Parse(line.Split('!')[1]);
                            break;
                        case "TIME":
                            Time = int.Parse(line.Split('!')[1]);
                            break;
                        case "TIMELEFT":
                            TimeLeft = line.Split('!')[1];
                            break;
                        case "SEC":
                            sec = int.Parse(line.Split('!')[1]);
                            break;
                        case "MIN":
                            min = int.Parse(line.Split('!')[1]);
                            break;
                        case "DIFFICULTY":
                            Difficulty = int.Parse(line.Split('!')[1]);
                            break;
                        case "ISCHEATON":
                            IsCheatOn = "TRUE" == line.Split('!')[1];
                            break;
                        case "CURBUILDING":
                            Building build = new Building();
                            CurBuilding = build.Deserialize(line);
                            CurBuilding.BuildWalls(Building.Maze);
                            break;
                        case "LEVELNUM":
                            LevelNum = int.Parse(line.Split('!')[1]);
                            break;
                        case "CURRLEVEL":
                            currLevel = line.Split('!')[1];
                            break;
                        case "RING":
                            BinderRing binder = new BinderRing();
                            ring = binder.Deserialize(line);
                            Environ.Add(ring);
                            break;
                        case "MARCUS":
                            Marcus = Marcus.Deserialize(line);
                            break;
                        case "ENVIRON":
                            for (int j = 0; j < identify.Length; j++)
                            {
                                switch (identify[j])
                                {
                                    case "INVENTORYITEM":
                                        InventoryItem inventory = new InventoryItem();
                                        string inven = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9},{10}!{11},{12}!{13},{14}!{15}", identify[j], identify[j + 1], identify[j + 2], identify[j + 3], identify[j + 4], identify[j + 5], identify[j + 6], identify[j + 7], identify[j + 8], identify[j + 9], identify[j + 10], identify[j + 11], identify[j + 12], identify[j + 13], identify[j + 14], identify[j + 15]);
                                        Environ.Add(inventory.Deserialize(inven));
                                        break;

                                    case "AI":
                                        AI aI = new AI(0, 0, 0, 0, 0);
                                        string aiStr = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9},{10}!{11},{12}!{13}", identify[j], identify[j + 1], identify[j + 2], identify[j + 3], identify[j + 4], identify[j + 5], identify[j + 6], identify[j + 7], identify[j + 8], identify[j + 9], identify[j + 10], identify[j + 11], identify[j + 12], identify[j + 13] );
                                        Environ.Add(aI.Deserialize(aiStr));
                                        break;

                                    case "WALLS":
                                        int[] temp = new int[2];
                                        temp[0] = 0;
                                        temp[1] = 0;
                                        Walls walls = new Walls(0, 0, temp);
                                        string wallsStr = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9}", identify[j], identify[j + 1], identify[j + 2], identify[j + 3], identify[j + 4], identify[j + 5], identify[j + 6], identify[j + 7], identify[j + 8], identify[j + 9]);
                                        Environ.Add(walls.Deserialize(wallsStr));
                                        break;
                                }
                            }
                            break;
                    }

                    line = rd.ReadLine();
                }

            }
        }

        //Instantiate InventoryItems
        public void MakeItems(int level)
        {
            if (level == 1) {
                ring.X = 2060;
                ring.Y = -1070;
                InventoryItem item = new InventoryItem();
                item.X = -2700;
                item.Y = -900;
                item.Image = "/Sprites/guitar.png";
                item.Name = "guitar";
                Environ.Add(item);
                InventoryItem itemTwo = new InventoryItem();
                itemTwo.X = 1700;
                itemTwo.Y = 3000;
                itemTwo.Image = "/Sprites/schaubJacket.png";
                itemTwo.Name = "jacket";
                Environ.Add(itemTwo);
                InventoryItem itemThree = new InventoryItem();
                itemThree.X = -3000;
                itemThree.Y = -1300;
                itemThree.Image = "/Sprites/flag.png";
                itemThree.canBePickedUp = false;
                itemThree.Name = "flag";
                Environ.Add(itemThree);
                InventoryItem itemFour = new InventoryItem();
                itemFour.X = 1000;
                itemFour.Y = -560;
                itemFour.Image = "/Sprites/pencil.png";
                itemFour.Name = "pencil";
                Environ.Add(itemFour);
                InventoryItem itemFive = new InventoryItem();
                itemFive.X = 2000;
                itemFive.Y = -700;
                itemFive.Image = "/Sprites/printer.png";
                itemFive.canBePickedUp = false;
                itemFive.Name = "printer";
                Environ.Add(itemFive);
                InventoryItem itemSix = new InventoryItem();
                itemSix.X = -1660;
                itemSix.Y = -1170;
                itemSix.Image = "/Sprites/chair.png";
                itemSix.Name = "chair";
                Environ.Add(itemSix);
                InventoryItem itemSeven = new InventoryItem();
                itemSeven.X = -100;
                itemSeven.Y = -1200;
                itemSeven.isTheOne = true;
                itemSeven.Image = "/Sprites/trumpet.png";
                itemSeven.Name = "trumpet";
                Environ.Add(itemSeven);
                InventoryItem itemEight = new InventoryItem();
                itemEight.X = 2060;
                itemEight.Y = -1070;
                itemEight.canBePickedUp = false;
                itemEight.Image = "/Sprites/bulletinBoard.png";
                itemEight.Name = "bulletin board";
                Environ.Add(itemEight);
                InventoryItem itemNine = new InventoryItem();
                itemNine.X = 160;
                itemNine.Y = 1670;
                itemNine.canBePickedUp = false;
                itemNine.Image = "/Sprites/piano.png";
                itemNine.Name = "piano";
                Environ.Add(itemNine);
                InventoryItem item10 = new InventoryItem();
                item10.X = 1600;
                item10.Y = 1600;
                item10.canBePickedUp = false;
                item10.Image = "/Sprites/coffeeMachine.png";
                item10.Name = "Coffee Machine";
                Environ.Add(item10);
            }
            else if (level == 2)
            {
                ring.X = -3000;
                ring.Y = -1200;
                InventoryItem item = new InventoryItem();
                item.X = 200;
                item.Y = -1200;
                item.isTheOne = true;
                item.Image = "/Sprites/Bible.png";
                item.Name = "Bible";
                Environ.Add(item);
                InventoryItem itemTwo = new InventoryItem();
                itemTwo.X = -3000;
                itemTwo.Y = 500;
                itemTwo.Image = "/Sprites/schaubJacket.png";
                itemTwo.Name = "jacket";
                Environ.Add(itemTwo);
                InventoryItem itemThree = new InventoryItem();
                itemThree.X = 60;
                itemThree.Y = 100;
                itemThree.Image = "/Sprites/table.png";
                itemThree.canBePickedUp = false;
                itemThree.Name = "table";
                Environ.Add(itemThree);
                InventoryItem itemFour = new InventoryItem();
                itemFour.X = 2000;
                itemFour.Y = -1200;
                itemFour.Image = "/Sprites/bookshelf.png";
                itemFour.canBePickedUp = false;
                itemFour.Name = "bookshelf";
                Environ.Add(itemFour);
                InventoryItem itemFive = new InventoryItem();
                itemFive.X = -100;
                itemFive.Y = -900;
                itemFive.Image = "/Sprites/backpack.png";
                itemFive.Name = "backpack";
                Environ.Add(itemFive);
                InventoryItem itemSix = new InventoryItem();
                itemSix.X = 1450;
                itemSix.Y = 170;
                itemSix.Image = "/Sprites/chair.png";
                itemSix.Name = "chair";
                Environ.Add(itemSix);
                InventoryItem itemSeven = new InventoryItem();
                itemSeven.X = 800;
                itemSeven.Y = -500;
                itemSeven.isTheOne = true;
                itemSeven.Image = "/Sprites/rubberDuck.png";
                itemSeven.Name = "duck";
                Environ.Add(itemSeven);
                InventoryItem itemEight = new InventoryItem();
                itemEight.X = -3000;
                itemEight.Y = -1200;
                itemEight.Image = "/Sprites/waterFountain.png";
                itemEight.canBePickedUp = false;
                itemEight.Name = "water fountain";
                Environ.Add(itemEight);
                InventoryItem itemNine = new InventoryItem();
                itemNine.X = -2150;
                itemNine.Y = 100;
                itemNine.Image = "/Sprites/pencil.png";
                itemNine.canBePickedUp = false;
                itemNine.Name = "pencil";
                Environ.Add(itemNine);
                InventoryItem itemTen = new InventoryItem();
                itemTen.X = -1000;
                itemTen.Y = 100;
                itemTen.Image = "/Sprites/mouse.png";
                itemTen.Name = "mouse";
                Environ.Add(itemTen);
                InventoryItem itemEleven = new InventoryItem();
                itemEleven.X = -1000;
                itemEleven.Y = -700;
                itemEleven.Image = "/Sprites/piano.png";
                itemEleven.canBePickedUp = false;
                itemEleven.Name = "piano";
                Environ.Add(itemEleven);
            }
            else if (level == 3)
            {
                ring.X = 2060;
                ring.Y = -1070;
                InventoryItem item = new InventoryItem();
                item.X = -3000;
                item.Y = -1280;
                item.isTheOne = true;
                item.Image = "/Sprites/Bible.png";
                item.Name = "Bible";
                Environ.Add(item);
                InventoryItem itemTwo = new InventoryItem();
                itemTwo.X = -3000;
                itemTwo.Y = 900;
                itemTwo.Image = "/Sprites/schaubJacket.png";
                itemTwo.Name = "jacket";
                Environ.Add(itemTwo);
                InventoryItem itemThree = new InventoryItem();
                itemThree.X = -1700;
                itemThree.Y = 100;
                itemThree.Image = "/Sprites/clock.png";
                itemThree.canBePickedUp = false;
                itemThree.Name = "clock";
                Environ.Add(itemThree);
                InventoryItem itemFour = new InventoryItem();
                itemFour.X = -1414;
                itemFour.Y = 1600;
                itemFour.isTheOne = true;
                itemFour.Image = "/Sprites/compass.png";
                itemFour.Name = "compass";
                Environ.Add(itemFour);
                InventoryItem itemFive = new InventoryItem();
                itemFive.X = 3000;
                itemFive.Y = 2000;
                itemFive.Image = "/Sprites/phone.png";
                itemFive.Name = "phone";
                Environ.Add(itemFive);
                InventoryItem itemSix = new InventoryItem();
                itemSix.X = 2560;
                itemSix.Y = -1070;
                itemSix.Image = "/Sprites/chair.png";
                itemSix.canBePickedUp = true;
                itemSix.Name = "chair";
                Environ.Add(itemSix);
                InventoryItem itemSeven = new InventoryItem();
                itemSeven.X = 2700;
                itemSeven.Y = -680;
                itemSeven.Image = "/Sprites/backpack.png";
                itemSeven.Name = "backpack";
                Environ.Add(itemSeven);
                InventoryItem itemEight = new InventoryItem();
                itemEight.X = -1400;
                itemEight.Y = 170;
                itemEight.Image = "/Sprites/waterFountain.png";
                itemEight.canBePickedUp = false;
                itemEight.Name = "water fountain";
                Environ.Add(itemEight);
                InventoryItem itemNine = new InventoryItem();
                itemNine.X = 2060;
                itemNine.Y = -970;
                itemNine.canBePickedUp = false;
                itemNine.Image = "/Sprites/flag.png";
                itemNine.Name = "flag";
                Environ.Add(itemNine);
                InventoryItem item10 = new InventoryItem();
                item10.X = -1300;
                item10.Y = -77;
                item10.Image = "/Sprites/laptop.png";
                item10.Name = "laptop";
                Environ.Add(item10);
                InventoryItem itemEleven = new InventoryItem();
                itemEleven.X = 3000;
                itemEleven.Y = -1200;
                itemEleven.Image = "/Sprites/drSchaub.png";
                itemEleven.canBePickedUp = true;
                itemEleven.Name = "drSchaub";
                Environ.Add(itemEleven);

                InventoryItem item12 = new InventoryItem();
                item12.X = -1080;
                item12.Y = -700;
                item12.Image = "/Sprites/piano.png";
                item12.canBePickedUp = false;
                item12.Name = "piano";
                Environ.Add(item12);

                InventoryItem item13 = new InventoryItem();
                item13.X = -1100;
                item13.Y = 500;
                item13.Image = "/Sprites/drMcGee.png";
                item13.Name = "drMcGee";
                Environ.Add(item13);
            }
        }

        //Created Save method with initial saving algorithm
        public void Save(string filename)
        {
            using (StreamWriter wr = new StreamWriter(filename))
            {
                wr.WriteLine("BEGIN");
                wr.WriteLine("CURRSCORE!" + CurrScore);
                wr.WriteLine("HIGHSCORE!" + HighScore);
                wr.WriteLine("TOTALSCORE!" + TotalScore);
                wr.WriteLine("COMPOSURE!" + Composure);
                wr.WriteLine("TIME!" + Time);
                wr.WriteLine("TIMELEFT!" + TimeLeft);
                wr.WriteLine("MIN!" + min);
                wr.WriteLine("SEC!" + sec);
                wr.WriteLine("DIFFICULTY!" + Difficulty);
                wr.WriteLine("NUMITEMS!" + NumItems);
                wr.WriteLine("LEVELNUM!" + LevelNum);
                wr.WriteLine("CURRLEVEL!" + currLevel);
                wr.WriteLine("ISCHEATON!" + IsCheatOn.ToString().ToUpper());
                wr.WriteLine("CURBUILDING!" + CurBuilding.Serialize());
                wr.WriteLine("RING!" + ring.Serialize());
                wr.WriteLine("MARCUS!"+ Marcus.Serialize());

                string theItems = "";

                foreach (WorldObject item in Environ)
                { 
                    if (item is AI)
                    {
                        theItems += (item as AI).Serialize() + ";";
                    }
                    else if (item is BinderRing)
                    {
                        theItems += (item as BinderRing).Serialize() + ";";
                    }
                    else if (item is InventoryItem)
                    {
                        theItems += (item as InventoryItem).Serialize() + ";";
                    }
                    else if (item is Walls)
                    {
                        theItems += (item as Walls).Serialize() + ";";
                    }
                    else if (item is Airplane)
                    {
                        continue;
                    }
                    else
                    {
                        throw new Exception("Houston we have a problem determining what 'item' is");
                    }
                }
                wr.WriteLine(string.Format("ENVIRON?{0}!{1}", Environ.Count, theItems));
                wr.WriteLine("END");
            }
        }
        public int CalculateScores(bool includeTime)
        {
            if (includeTime)
                CurrScore = Convert.ToInt32((PsiZetaShamed * 200) + (Time * 15));
            else
                CurrScore = Convert.ToInt32((PsiZetaShamed * 200));

            TotalScore = CurrScore + HighScore;
            return TotalScore;
        }
        protected void SetProperty(string source)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (handle != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(source));
            }
        }
        public void MakeAIPerLevel()
        {
            if (LevelNum == 1)
            {
                MakeAI(1050, 400, 2, 1);
                MakeAI(2000, 600, 2, 1);
                MakeAI(800, 1600, 2, 1);
            }
            else if (LevelNum == 2)
            {
                MakeAI(850, -600, 2, 1);
                MakeAI(-20, 1000, 2, 1);
                MakeAI(-1200, 500, 2, 1);
                MakeAI(20, -1000, 2, 1);
                MakeAI(1200, -500, 3, 1);
            }
            else if (LevelNum == 3)
            {
                MakeAI(-2000, -900, 2, 1);
                MakeAI(-2500, 1000, 2, 1);
                MakeAI(2000, 100, 2, 1);
                MakeAI(3000, -1300, 3, 1);
                MakeAI(0, 1500, 5, 1);
                MakeAI(0, -100, 2, 1);
                MakeAI(-500, 850, 2, 1);
            }
        }
        public void MakeAI(int x, int y, int health, int damage)
        {
            if (Difficulty == 1)
            {
                AI ai = new AI(health, damage, 5, x, y);
                Environ.Add(ai);
            }
            else if (Difficulty == 2)
            {
                AI ai = new AI(health * 2, damage, 5, x, y);
                Environ.Add(ai);
            }
            else if (Difficulty == 3)
            {
                AI ai = new AI((health * 3), (damage * 2), 5, x, y);
                Environ.Add(ai);
            }
        }
    }
}