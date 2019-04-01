using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binder.Environment;
using System.IO;  //added IO using statement - ZD

namespace Binder
{
    class Game : ISerialization<Game>
    {
        public int CurrScore { get; set; }          //Keeps track of the current score as player plays
        public int HighScore { get; set; }          //Keeps track of the High Score so far
        public int Composure { get; set; }          //Keeps track of the health of the Player
        public int Time { get; set; }               //Keeps track of the amount of time remaining
        public int NumItems { get; set; }           //Keeps track of the number of items in players inventory


        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public Game Deserialize(string obj)
        {
            throw new NotImplementedException();
        }

        //Creaated Load method with initial loading algorithm
        public void Load(string filename)
        {
            using (StreamReader rd = new StreamReader(filename))
            {
                rd.ReadLine();
                string building = rd.ReadLine();
                Building build = new Building();
                build.Deserialize(building);
                CurrScore = int.Parse(rd.ReadLine());
                HighScore = int.Parse(rd.ReadLine());
                Composure = int.Parse(rd.ReadLine());
                NumItems = int.Parse(rd.ReadLine());
                Time = int.Parse(rd.ReadLine());
                Player player = new Player("");
                player.Deserialize(rd.ReadLine());
                AI ai = new AI();
                ai.Deserialize(rd.ReadLine());
                Walls walls = new Walls(0, 0, [0,0] );
                walls.Deserialize(rd.ReadLine());
                InventoryItem inventoryItem = new InventoryItem();
                inventoryItem.Deserialize(rd.ReadLine());
                DecoyItem decoyItem = new DecoyItem();
                decoyItem.Deserialize(rd.ReadLine());
                rd.ReadLine();
            }
        }

        //Created Save method with initial saving algorithm
        public void Save(string filename)
        {

            using (StreamWriter wr = new StreamWriter(filename))
            {
                wr.WriteLine("BEGIN");
                wr.WriteLine();
                wr.WriteLine("BUILDING,WIDTH,LENGTH");
                wr.WriteLine("LVLNUMBER");
                wr.WriteLine("ENVIRONMENTIMAGE");
                wr.WriteLine("REMAININGAI");
                wr.WriteLine(Time);
                wr.WriteLine(NumItems);
                wr.WriteLine(CurrScore);
                wr.WriteLine(HighScore);
                wr.WriteLine("PLAYER,XPOSITION,YPOSITION," + Composure.ToString() + ",INVENTORY1,INVENTORY2,INVENTORY3,INVENTORY4");
                wr.WriteLine("AI,XPOSITION,YPOSITION,HEALTH,PATHX,PATHY");
                wr.WriteLine("WALLS,WIDTH,LENGTH,POSX,POSY");
                wr.WriteLine("INVENTORYITEM,NAME,IMAGE,POSX,POSY,FOUND");
                wr.WriteLine("DECOYITEM,NAME,IMAGE,POSX,POSY,FOUND");
                wr.WriteLine("END");
            }
        }
    }
}
