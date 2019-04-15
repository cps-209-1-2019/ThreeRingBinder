using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binder.Environment;
using System.IO;  //added IO using statement - ZD
using System.ComponentModel;

namespace Binder
{
    public class Game: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Player Marcus { get; set; }
        public int CurrScore { get; set; }          //Keeps track of the current score as player plays
        public int HighScore { get; set; }          //Keeps track of the High Score so far
        public int Composure { get; set; }          //Keeps track of the health of the Player
        public int Time { get; set; }               //Keeps track of the amount of time remaining
        public int NumItems { get; set; }           //Keeps track of the number of items in players inventory
        //public int[] StartPoint { get; set; }       //Keeps track of where the player starts and will be used to calculate where everything is positioned on the map
        public bool IsCheatOn { get; set; }         //Determines whether or not the cheat mode should be on
        public static int Difficulty { get; set; }         //Holds difficulty level
        public static List<WorldObject> Environ { get; set; }
        public Building CurBuilding { get; set; }
        public static bool isPaused { get; set; }    //Determines if the game is paused
        public BinderRing ring;                      //Current binder ring
        public bool isRingFound;                     //Determines if the player has found the ring
        public static List<InventoryItem> itemsHeld = new List<InventoryItem>();   //Items currently held by the player
        public int currentItem = 0;                          //Shows item that currently needs to be used
        public int PsiZetaShamed = 0;

        public double timeLeft;
        private string time;
        private int min = 2;
        private int sec = 60;
        public string TimeLeft
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

        public Game(double startTime)
        {
            timeLeft = startTime;
            Marcus = new Player("Marcus");
            Environ = new List<WorldObject>();
            isPaused = false;

            CurBuilding = new Building() { Length = 2500, Width = 5464};
            CurBuilding.BuildWalls(CurBuilding.Maze);
            CurBuilding.Name = "Maze";

            Environ.AddRange(Building.WallsCol);
            //StartPoint = new int[] { 0, 0 };

            //StartPoint = new int[2];
            ring = new BinderRing();
            ring.X = 700;
            ring.Y = 450;
            MakeItems();
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
            }

            string minutes = min.ToString();
            string seconds = sec.ToString();

            if(seconds.Length == 1)
            {
                seconds = "0" + seconds;
            }

            TimeLeft = "Time: 0" + min + ":" + seconds;            
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
                        case "COMPOSURE":
                            Composure = int.Parse(line.Split('!')[1]);
                            break;
                        case "NUMITEMS":
                            NumItems = int.Parse(line.Split('!')[1]);
                            break;
                        case "TIME":
                            Time = int.Parse(line.Split('!')[1]);
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
                            CurBuilding.BuildWalls(CurBuilding.Maze);
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

                                        string inven = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9},{10}!{11}", identify[j], identify[j + 1], identify[j + 2], identify[j + 3], identify[j + 4], identify[j + 5], identify[j + 6], identify[j + 7], identify[j + 8], identify[j + 9], identify[j + 10], identify[j + 11]);

                                        Environ.Add(inventory.Deserialize(inven));
                                        break;

                                    case "AI":
                                        AI aI = new AI(0,0,0);
                                        string aiStr = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9}", identify[j], identify[j + 1], identify[j + 2], identify[j + 3], identify[j + 4], identify[j + 5], identify[j + 6], identify[j + 7], identify[j + 8], identify[j + 9]);
                                        Environ.Add(aI.Deserialize(aiStr));

                                        break;

                                    case "WALLS":
                                        Walls walls = new Walls(0,0,new int[0]);
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
        public void MakeItems()
        {
            InventoryItem item = new InventoryItem();
            item.X = 800;
            item.Y = 450;
            item.isTheOne = true;
            item.Image = "/Sprites/rubberDuck.png";
            item.Name = "duck";
            Environ.Add(item);
            InventoryItem itemTwo = new InventoryItem();
            itemTwo.X = 700;
            itemTwo.Y = 900;
            itemTwo.Image = "/Sprites/schaubJacket.png";
            itemTwo.Name = "jacket";
            Environ.Add(itemTwo);
            InventoryItem itemThree = new InventoryItem();
            itemThree.X = 360;
            itemThree.Y = 100;
            itemThree.Image = "/Sprites/waterFountain.png";
            itemThree.canBePickedUp = false;
            itemThree.Name = "fountain";
            Environ.Add(itemThree);
        }

        //Created Save method with initial saving algorithm
        public void Save(string filename)
        {
            using (StreamWriter wr = new StreamWriter(filename))
            {
                wr.WriteLine("BEGIN");
                wr.WriteLine("CURRSCORE!" + CurrScore);
                wr.WriteLine("HIGHSCORE!" + HighScore);
                wr.WriteLine("COMPOSURE!" + Composure);
                wr.WriteLine("TIME!" + Time);
                wr.WriteLine("NUMITEMS!" + NumItems);
                wr.WriteLine("ISCHEATON!" + IsCheatOn.ToString().ToUpper());
                wr.WriteLine("CURBUILDING!" + CurBuilding.Serialize());
                wr.WriteLine("MARCUS!"+ Marcus.Serialize());

                string theItems = "";

                foreach (WorldObject item in Environ)
                { 
                    if (item is AI)
                    {
                        theItems += (item as AI).Serialize() + ";";
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
        public int CalculateScores()
        {
            return Convert.ToInt32((PsiZetaShamed * 200) + (timeLeft * 15));
        }
        protected void SetProperty(string source)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (handle != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(source));
            }
        }

    }
}
