using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binder.Environment;
using System.IO;  //added IO using statement - ZD

namespace Binder
{
    class Game
    {
        public int CurrScore { get; set; }          //Keeps track of the current score as player plays
        public int HighScore { get; set; }          //Keeps track of the High Score so far
        public int Composure { get; set; }          //Keeps track of the health of the Player
        public int Time { get; set; }               //Keeps track of the amount of time remaining
        public int NumItems { get; set; }           //Keeps track of the number of items in players inventory
        public int[] StartPoint { get; set; }       //Keeps track of where the player starts and will be used to calculate where everything is positioned on the map
        public bool IsCheatOn { get; set; }         //Determines whether or not the cheat mode should be on
        public int Difficulty { get; set; }        //Holds difficulty level

        
        //Creaated Load method with initial loading algorithm
        public void Load(string filename)
        {
            using (StreamReader rd = new StreamReader(filename))
            {
                rd.ReadLine();
                CurrScore = int.Parse(rd.ReadLine());
                HighScore = int.Parse(rd.ReadLine());
                Composure = int.Parse(rd.ReadLine());
                NumItems = int.Parse(rd.ReadLine());
                Time = int.Parse(rd.ReadLine());
                string stPoint = rd.ReadLine();
                string[] startPoints = stPoint.Split(',');
                StartPoint[0] = int.Parse(startPoints[0]);
                StartPoint[1] = int.Parse(startPoints[1]);
                IsCheatOn = bool.Parse(rd.ReadLine());
                string building = rd.ReadLine();
                Building build = new Building();
                build.Deserialize(building);
                 //Player player = new Player("");
                rd.ReadLine();//player.Deserialize(rd.ReadLine());
                //AI ai = new AI(0, 0, 0);
                rd.ReadLine();//ai.Deserialize(rd.ReadLine());
                //int[] ar = new int[2]{0, 0};
                //Walls walls = new Walls(0, 0, ar);
                rd.ReadLine();//walls.Deserialize(rd.ReadLine());
                //InventoryItem inventoryItem = new InventoryItem();
                rd.ReadLine();//inventoryItem.Deserialize(rd.ReadLine());
                //DecoyItem decoyItem = new DecoyItem();
                rd.ReadLine();//decoyItem.Deserialize(rd.ReadLine());
                rd.ReadLine();
            }
        }

        //Created Save method with initial saving algorithm
        public void Save(string filename)
        {

            using (StreamWriter wr = new StreamWriter(filename))
            {
                wr.WriteLine("BEGIN");
                wr.WriteLine(CurrScore);
                wr.WriteLine(HighScore);
                wr.WriteLine(Composure);
                wr.WriteLine(Time);
                wr.WriteLine(NumItems);
                wr.WriteLine(StartPoint);
                wr.WriteLine(IsCheatOn);
                Building building = new Building();
                wr.WriteLine(building.Serialize());
                Player player = new Player("fred");
                wr.WriteLine(player.Serialize());
                AI ai = new AI(0, 0, 0);
                wr.WriteLine(ai.Serialize());
                int[] ar = new int[2] { 0, 0 };
                Walls walls = new Walls(0, 0, ar);
                wr.WriteLine(walls.Serialize());
                InventoryItem inventoryItem = new InventoryItem();
                wr.WriteLine(inventoryItem.Serialize());
                DecoyItem decoyItem = new DecoyItem();
                wr.WriteLine(decoyItem.Serialize());
                Environment.Binder binder = new Environment.Binder();
                wr.WriteLine(binder.Serialize());
                wr.WriteLine("END");
            }
        }
    }
}
