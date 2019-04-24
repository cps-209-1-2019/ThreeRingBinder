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
        
        public string Name { get; set; }            //Holds the name of the building to Display

        public List<Walls> WallsCol;                //Holds the collection of Walls

        public Building()
        {            
            BuildFA();
            WallsCol = new List<Walls>();
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

        //Builds the FA plans
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
                                int[] temp = new int[2];
                                temp[0] = 0;
                                temp[1] = 0;
                                Walls walls = new Walls(0, 0, temp);
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

        //Building Plans

        //Perimeter for the other Building Plans
        List<int[]> Perimeter = new List<int[]>()
        {
            //Coords Format: `x`, `y`, `l`, `w`

            //Perimeter
            new int[4] {-3105, -1380, 69, 6210},
            new int[4] {-3105, 1962, 69, 6210},
            new int[4] {-3105, -1380, 3342, 69},
            new int[4] {3105, -1380, 3342, 69},
        };

        //Perimeter for the Menacing Maze
        List<int[]> MazePerimeter = new List<int[]>()
        {
            new int[4] {-3105, -1380, 69, 6210},
            new int[4] {-3105, 2889, 69, 6210},
            new int[4] {-3105, -1380, 4278, 69},
            new int[4] {3105, -1380, 4278, 69},
        };

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

        //Holds coords for the Finest Artists Walls
        public static List<int[]> FAPlans = new List<int[]>()
        {        };

        //Holds coords for the Menacing Maze
        public static List<int[]> Maze = new List<int[]>()
        {
           new int[4] { -2829, -1380, 621, 69},  //1
           new int[4] {-2622, -1070, 69, 690},
           new int[4] { -1725, -1173, 276, 69 },
           new int[4] {-1173, -1380, 1035, 69},
           new int[4] {-759, -1173, 552, 69},    //5

           new int[4] {-2898, -483, 69, 621},
           new int[4] {-3105, -1760, 69, 138},
           new int[4] {-2277, -794, 759, 69},
           new int[4] {-1932, -690, 69, 552},
           new int[4] {-1932, -690, 345, 90},    //10

           new int[4] {-1932, -345, 69, 759},
           new int[4] {-2277, 69, 69, 1725},
           new int[4] {-552, -276, 345, 69},
           new int[4] {-552, -276, 69, 414},
           new int[4] {-276, 0, 207, 69},   //15


           new int[4] {-2691, -207, 483, 69},
           new int[4] {-2691, 276, 69, 621},
           new int[4] {-1587, 207, 138, 69},
           new int[4] {-1587, 345, 69, 828},
           new int[4] {-276, 276, 69, 621},   //20

           new int[4] {-2076, 276, 828, 69},
           new int[4] {-1242, 345, 759, 69},
           new int[4] {-2829, 483, 828, 69},
           new int[4] {-2616, 483, 621, 69},
           new int[4] {-2616, 1104, 69, 609 },  //25

           new int[4] {-1587, 621, 867, 69}, 
           new int[4] {-1869, 690, 69, 138},
           new int[4] {-1587, 1104, 69, 966},
           new int[4] {-897, 622, 207, 69},
           new int[4] {-1242, 832, 69, 552}, //30           

           new int[4] {-2829, 1380, 69, 1931},
           new int[4] {-2898, 1725, 69, 207},
           new int[4] {-2691, 1656, 414, 69},
           new int[4] {-2622, 1863, 69, 690},
           new int[4] {-1932, 1656, 207, 69}, //35

           new int[4] {-1587, 1380, 659, 69},
           new int[4] {-1587, 1794, 69, 828}, //37

           new int[4] {-759, -1104, 552, 69},
           new int[4] {-483, -1104, 552, 69}, 
           new int[4] {-207, -1104, 276, 69}, //40

           new int[4] {2004, -1104, 69, 414},
           new int[4] {2487, -1104, 828, 69},
           new int[4] {2763, -897, 69, 345},
           new int[4] {-759, -552, 69, 1104},
           new int[4] {2004, -345, 690, 69 },   //45

           new int[4] {-483, -828, 69, 1311},
           new int[4] {0, -69, 276, 69},
           new int[4] {414, -276, 69, 690},
           new int[4] {1590,-69, 69, 345},
           new int[4] {2004, 345, 69, 612},     //50

           new int[4] {2556, 345, 828, 69},
           new int[4] {762, 345, 69, 1104},
           new int[4] {897, 414, 414, 69 },
           new int[4] {828, 888, 69, 828},
           new int[4] {2208, 819, 828, 69},    //55
           
           new int[4] {828, 1164, 69, 828},
           new int[4] {2829, 1164, 69, 276},
           new int[4] {1104, 1164, 414, 69},
           new int[4] {1518, 1509, 621, 69},
           new int[4] {1863, 1647, 690, 69},   //60

           new int[4] {1863, 1647, 69, 483},
           new int[4] {414, 1578, 69, 690},
           new int[4] {897, 1578, 966, 69},
           new int[4] {1173, 1854, 483, 69},
           new int[4] {1173, 2337, 69, 621},  //65

           new int[4] {2484, 1923, 690, 69},
           new int[4] {897, 2613, 69, 1582},
           new int[4] {552, 2061, 828, 69},    //68
           new int[4] {-2346, 2475, 69, 2346},
           new int[4] {0, 1854, 621, 69}
            
        };

        //Adds the Perimeter of to the Building Plan passed in its perimeters.
        void BuildPerim(List<int[]> plans)
        {
            if(plans == Maze)
            {
                plans.AddRange(MazePerimeter);
            }
            else
            {
                plans.AddRange(Perimeter);
            }           
        }
    }
}
