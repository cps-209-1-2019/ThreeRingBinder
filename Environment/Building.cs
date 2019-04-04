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
        public List<int[]> LibPlans = new List<int[]>()
        {
            //Perimeter
            new int[4] {-2016, -1547, 5476, 24},
            new int[3] {0, 0, 0}
        };

        public int Width { get; set; }
        public int Length { get; set; }

        public Dictionary<string, Items> Collection;
        public List<Walls> WallsCol;


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
            List<string> properties = new List<string>(obj.Split(','));

            int start = 0;
            int end = 0;
            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i].Contains("]"))
                {
                    end = i;
                }
                else if (properties[i].Contains("["))
                {
                    start = i;
                }
            }
            
            for (int i = start; i < end; i++)
            {
                properties[start] = properties[start] + "," + properties[start + 1];
                properties.RemoveAt(start + 1);
            }


            Width = int.Parse(properties[1].Split('!')[1]);
            Length = int.Parse(properties[2].Split('!')[1]);

            string[] collectA = properties[3].Split('!');
            string[] collectB = collectA[1].Split(';');
            foreach (string s in collectB)
            {
                string[] item = s.Split();
                string value = item[1].Trim('[',']');
                string[] nextObj = value.Split(',');
                Items nextItem = new Items();
                switch (nextObj[0])
                {
                    case "INVENTORYITEM":
                        InventoryItem inven = new InventoryItem();
                        inven.Deserialize(value);
                        nextItem = inven as Items;
                        break;
                    case "DECOYITEM":
                        DecoyItem decoy = new DecoyItem();
                        decoy.Deserialize(value);
                        nextItem = decoy as Items;
                        break;
                }

                Collection.Add(item[0],nextItem);
            }



            return this;
        }

        
    }
}
