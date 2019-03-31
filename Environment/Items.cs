//--------------------------------------------------------------------------------------------
//File:   Items.cs
//Desc:   This class contains logic for the items in the room.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environments
{
    //Added public accessibility - Day
    public class Items
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int[] Position { get; set; }
        public bool Found { get; set; }

        public Items(string name, string image, int[] pos)
        {
            Name = name;
            Image = image;
            Position = pos;
        }

        //Returns the string for the GUI to display
        public string Show()
        {
            return Image;
        }

        public abstract string Serialize();
        public abstract Items Deserialize();
    }

    public class InventoryItem: Items, ISerialization<InventoryItem>
    {

        //Turn the object into a string
        public string Serialize()
        {
            throw new NotImplementedException();
        }

        //Take a string and return an InventoryItem object
        public InventoryItem Deserialize(string obj)
        {
            throw new NotImplementedException();
        }

    }

    public class DecoyItem: Items, ISerialization<DecoyItem>
    {
        //Turn the object into a string
        public string Serialize()
        {
            throw new NotImplementedException();
        }

        //Take a string and return a DecoyItem object
        public DecoyItem Deserialize(string obj)
        {
            throw new NotImplementedException();
        }
    }
}
