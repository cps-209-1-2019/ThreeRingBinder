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
    public class Building : ISerialization<Building>, WorldObject
    {
        public int Width { get; set; }
        public int Length { get; set; }

        public Dictionary<string, Items> Collection;


        //Adds the Item object in its params to the Collection
        public void AddItem(Items item)
        {
            Collection[item.Name] = item;
        }

        public void RmvItm(Items item)
        {
            Collection.Remove(item.Name);
        }

        //Moves the map with respect to the player position
        public void Move(int[] pPos)
        {

        }

        //Turn the object into a string
        public string Serialize()
        {
            throw new NotImplementedException();
        }

        //Take a string and turn it into a Building object
        public Building Deserialize(string obj)
        {
            throw new NotImplementedException();
        }

        
    }
}
