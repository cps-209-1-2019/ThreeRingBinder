//--------------------------------------------------------------------------------------------
//File:   Building.cs
//Desc:   This is the class that contains logic for the Buildings the player will be in.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    //Added public accessibility modifier - Day
    public class Building : WorldObject, ISerialization<Building>
    {
        private int length = 0;
        public int Width { get; set; }
        public int Length
        {
            get;
            set;
        }

        public Dictionary<string, Items> Collection;
        public List<Walls> WallsCol;

        public List<int[]> LibPlans = new List<int[]>()
        {
            //Coords Format: `x`, `y`, `l`, `w`
            new int[4] {0, 0, 24, 2000},
            new int[4] {0, 1000, 24, 2000},
            new int[4] {0, 0, 1000, 24},
            new int[4] {2000, 0, 1000, 24}
        };

        //Adds the Item object in its params to the Collection
        public void AddItem(Items item)
        {
            Collection[item.Name] = item;
        }

        //Removes an item object from the Collection of items in the Building
        public void RmvItm(Items item)
        {
            Collection.Remove(item.Name);
        }

        //Builds walls from the List of Coords
        public void BuildWalls(List<int[]> coords)
        {
            foreach(int[] dt in coords)
            {
                int[] wcoord = new int[2] { dt[0], dt[1] };
                Walls wall = new Walls(dt[2], dt[3], wcoord);
                WallsCol.Add(wall);
            }
        }

        ////Moves the map with respect to the player position and direction
        //public void Move(int[] pPos, int dir)
        //{
            
        //}

        //Turn the object into a string
        public string Serialize()
        {
            throw new NotImplementedException();
        }

        //Take a string and turn it into a Building object
        public Building Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',','!','#',':','?',';'));


            for (int i = 0; i < properties.Count; i++)
            {
                switch (properties[i])
                {
                    case "WIDTH":
                        Width = int.Parse(properties[i + 1]);
                        break;
                    case "LENGTH":
                        Length = int.Parse(properties[i + 1]);
                        break;
                    case "COLLECTION":
                        for (int j = i; j < properties.Count; j++)
                        {
                            if (properties[j] == "INVENTORYITEM")
                            {
                                InventoryItem inventory = new InventoryItem();

                                string inven = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7}", properties[j], properties[j+1],properties[j+2],properties[j+3],properties[j+4],properties[j+5],properties[j+6],properties[j+7]);

                                Collection.Add(properties[j-1], inventory.Deserialize(inven));
                            }
                        }
                        break;
                }
            }
            return this;
        }

        
    }
}
