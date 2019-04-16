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
        //public int Width { get; set; }
        //public int Length{ get; set; }

        public Dictionary<string, Items> Collection;
        public List<Walls> WallsCol;

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
            BuildPerim(coords);
            foreach (int[] dt in coords)
            {
                int[] wcoord = new int[2] { dt[0], dt[1] };
                Walls wall = new Walls(dt[3], dt[2], wcoord);
                WallsCol.Add(wall);
            }
        }

        void BuildFA()
        {
            bool full = false;
            
            for(int i = 621; i < 6210; i+= 621)
            {
                int[] coords = null;
                int[] rcoords = null;
                int[] lcoords = null;

                if (full == false)
                {
                    coords = new int[4] { -3105 + i, -1350, 567, 69};
                    lcoords = new int[4] { -3105 + i, -783, 69, 207 };
                    rcoords = new int[4] { -3105 + i + 138, -783, 69, 207 };
                     
                }
                if(full == true)
                {
                    coords = new int[4] { -3105 + i, 1395, 567, 69};
                    lcoords = new int[4] { -3105 + i, 1395, 69, 207 };
                    rcoords = new int[4] { -3105 + i + 138, 1395, 69, 207 };
                }
                
                if(i + 621 >= 6210)
                {                    
                    if(full != true)
                    {
                        i = 621;
                        full = true;
                    }                   
                }

                FAPlans.Add(coords);
                FAPlans.Add(lcoords);
                FAPlans.Add(rcoords);
            }
        }


        //Turn the object into a string
        public string Serialize()
        {
            string theBuild = "";
            string theCollection = "";
            string theWalls = "";
            
            foreach (string key in Collection.Keys)
            { 
                theCollection += key + ":" + Collection[key].Serialize() + ";"; 
            }

            foreach(Walls walls in WallsCol)
            {
                theWalls += walls.Serialize() + ";";
            }


            theBuild = string.Format("BUILDING?3,WIDTH!{0},LENGTH!{1},COLLECTION#{2}!{3}ENDLIST,WALLSCOL#{4}!{5}ENDLIST", Width, Length, Collection.Count, theCollection, WallsCol.Count, theWalls);

            return theBuild;
        }

        //Take a string and turn it into a Building object
        public Building Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));

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

                                string inven = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9},{10}!{11}", properties[j], properties[j + 1], properties[j + 2], properties[j + 3], properties[j + 4], properties[j + 5], properties[j + 6], properties[j + 7], properties[j + 8], properties[j + 9], properties[j + 10], properties[j + 11]);

                                Collection.Add(properties[j - 1], inventory.Deserialize(inven));
                            }
                            else if (properties[j] == "ENDLIST")
                            {
                                break;
                            }
                        }
                        break;
                    case "WALLSCOL":
                        for (int j = i; j < properties.Count; j++)
                        {
                            if (properties[j] == "WALLS")
                            {
                                Walls walls = new Walls(0, 0, new int[0]);
                                string theWalls = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9}", properties[j], properties[j + 1], properties[j + 2], properties[j + 3], properties[j + 4], properties[j + 5], properties[j + 6], properties[j + 7], properties[j + 8], properties[j + 9]);
                                WallsCol.Add(walls.Deserialize(theWalls));
                            }
                            else if (properties[j] == "ENDLIST")
                            {
                                break;
                            }
                        }

                        break;
                }
            }
            return this;
        }

        List<int[]> Perimeter = new List<int[]>()
        {
            //Coords Format: `x`, `y`, `l`, `w`

            //Perimeter
            new int[4] {-3105, -1380, 69, 6210},
            new int[4] {-3105, 1962, 69, 6210},
            new int[4] {-3105, -1380, 3342, 69},
            new int[4] {3105, -1380, 3342, 69},
        };

        //Building Plans
        public static List<int[]> LibPlans = new List<int[]>()
        {            
            //Computer Labs
            new int[4] {-3519, -669, 69 , 897},
            new int[4] {-2415, -669, 69, 414},
            new int[4] { -2001, -1350, 690, 69},

            new int[4] {-1794, -1005, 69, 621},
            new int[4] {-1173, -1350, 1173, 69},
            new int[4] {-1173, 30, 414, 69},

            new int[4] {-966, -177, 69, 1380},
            new int[4] {414, -1350, 1173, 69},

            new int[4] {-3105, -315, 69, 1035},
            new int[4] {-2070, -315, 897, 69},
            new int[4] {-3105, 582, 69, 828},

            new int[4] {-2070, 720, 345, 69},
            new int[4] {-3105, 1065, 69, 1035},
            new int[4] {-2553, 1272, 690, 69},
            new int[4] {-2070, 1272, 690, 69},

            new int[4] {-2070, 1548, 69, 897},
            new int[4] {-1863, 1341, 207, 69},
            new int[4] {-1863, 927, 138, 69},
            new int[4] {-1863, 927, 69, 966 },
            new int[4] {-897, 927, 621, 69},

            new int[4] {-414, 1548, 69, 414},
            new int[4] {207, 1134, 414, 69},
            new int[4] {-414, 1134, 69, 414},
            new int[4] {-414, 1134, 414, 69}
        };

        public static List<int[]> FAPlans = new List<int[]>()
        {
            //Rooms
            //new int[4] {-2049, -1250, 1000, 24},
            new int[4] {-2049, 500, 1000, 769}

        };


        public static List<int[]> Maze = new List<int[]>()
        {
            new int[4] {-4000, -1100, 69, 1000},
            new int[4] {-3000, -1238, 200, 69},
            new int[4] {-3000, -1238, 69, 700},
            new int[4] {-2300, -1238, 800, 69},
            new int[4] {-3000, -438, 69, 700},
            new int[4] {-3000, -438, 600, 69},
            new int[4] {-3000, 181, 69, 700},

            new int[4] {-3400, -1100, 600, 69},
            new int[4] {-3400, -800, 69, 700},
            new int[4] {-3500, -238, 69, 500},
            
        };

        void BuildPerim(List<int[]> plans)
        {
            plans.AddRange(Perimeter);
        }
    }
}
