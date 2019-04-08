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
        public int[] StartPoint { get; set; }       //Keeps track of where the player starts and will be used to calculate where everything is positioned on the map
        public bool IsCheatOn { get; set; }         //Determines whether or not the cheat mode should be on
        public int Difficulty { get; set; }         //Holds difficulty level
        public List<WorldObject> Environ { get; set; }
        public Building CurBuilding { get; set; }
        public static bool isPaused { get; set; }    //Determines if the game is paused

        public Game()
        {
            Marcus = new Player("Marcus");
            Environ = new List<WorldObject>();
            isPaused = false;

            CurBuilding = new Building() { Length = 2500, Width = 5464 };
            CurBuilding.BuildWalls(CurBuilding.FAPlans);
            Environ.AddRange(Building.WallsCol);
            StartPoint = new int[] { 0, 0 };
        }

        //Creaated Load method with initial loading algorithm
        public void Load(string filename)
        {
            StartPoint = new int[2];

            using (StreamReader rd = new StreamReader(filename))
            {
                string line = rd.ReadLine();

                while (line != "END")
                {
                    string identify = line.Split(',', '!', '#', ':', '?', ';')[0];
                    switch (identify)
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
                            break;
                        case "MARCUS":
                            Marcus = Marcus.Deserialize(line);
                            break;
                        case "STARTPOINTX":
                            StartPoint[0] = int.Parse(line.Split('!')[1]);
                            break;
                        case "STARTPOINTY":
                            StartPoint[1] = int.Parse(line.Split('!')[1]);
                            break;
                    }

                    line = rd.ReadLine();
                }

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
                wr.WriteLine("COMPOSURE!" + Composure);
                wr.WriteLine("TIME!" + Time);
                wr.WriteLine("NUMITEMS!" + NumItems);
                wr.WriteLine("STARTPOINTX!" + StartPoint[0]);
                wr.WriteLine("STARTPOINTY!" + StartPoint[1]);
                wr.WriteLine("ISCHEATON!" + IsCheatOn.ToString().ToUpper());
                wr.WriteLine("CURBUILDING!" + CurBuilding.Serialize());
                wr.WriteLine("MARCUS!"+ Marcus.Serialize());
                wr.WriteLine("END");
            }
        }
    }
}
