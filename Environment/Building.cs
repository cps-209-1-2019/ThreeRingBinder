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
        public string Name { get; set; }
        public int Width { get; set; }
        public int Length{ get; set; }


        public Dictionary<string, Items> Collection;
        public static List<Walls> WallsCol;

        public List<int[]> LibPlans = new List<int[]>()
        {
            //Coords Format: `x`, `y`, `l`, `w`

            //Perimeter
            new int[4] {-3000, -1500, 69, 6000},
            new int[4] {-3000, 1500, 69, 6000},
            new int[4] {-3000, -1500, 3000, 69},
            new int[4] {3000, -1500, 3000, 69},
            
            //Computer Labs
            new int[4] {-2510, 990, 260, 69},
            new int[4] {-2289, 990, 260, 69},
            new int[4] { -3000, 960, 69, 443}
        };

        public List<int[]> FAPlans = new List<int[]>()
        {
            //Perimeter
            new int[4] {-3000, -1500, 24, 6000},
            new int[4] {-3000, 1500, 24, 6000},
            new int[4] {-3000, -1500, 2500, 69},
            new int[4] {3000, -1500, 2500, 69},

            //Rooms
            //new int[4] {-2049, -1250, 1000, 24},
            new int[4] {-2049, 500, 1000, 69}

        };

        public Building()
        {
            BuildFA();
            Collection = new Dictionary<string, Items>();
            WallsCol = new List<Walls>();
        }

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
                Walls wall = new Walls(dt[3], dt[2], wcoord);
                WallsCol.Add(wall);
            }
        }

        void BuildFA()
        {
            bool full = false;
            
            for(int i = 750; i < 6000; i+= 750)
            {
                int[] coords = null;
                if (full == false)
                {
                    coords = new int[4] { -3000 + i, -1500, 1000, 69 };
                }
                if(full == true)
                {
                    coords = new int[4] { -3000 + i, 500, 1000, 69 };
                }
                
                if(i + 750 >= 6000)
                {                    
                    if(full != true)
                    {
                        i = 750;
                        full = true;
                    }                   
                }

                FAPlans.Add(coords);
            }
        }

        //Turn the object into a string
        public string Serialize()
        {
            string theBuild = "";
            string theCollection = "";
            
            foreach (string key in Collection.Keys)
            { 
                theCollection += key + ":" + Collection[key].Serialize() + ";"; 
            }


            theBuild = string.Format("BUILDING?3,WIDTH!{0},LENGTH!{1},COLLECTION#{2}!{3}", Width, Length, Collection.Count,  theCollection);

            return theBuild;
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

                                string inven = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9},{10}!{11}", properties[j], properties[j+1],properties[j+2],properties[j+3],properties[j+4],properties[j+5],properties[j+6],properties[j+7], properties[j + 8], properties[j + 9], properties[j + 10], properties[j + 11]);

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
