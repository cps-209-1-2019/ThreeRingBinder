﻿//--------------------------------------------------------------------------------------------
//File:   Items.cs
//Desc:   This class contains logic for the items in the room.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    //Added public accessibility - Day
    public class Items : WorldObject
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Found { get; set; }
        public override int[] Position { get; set; }

        //public Items(string name, string image, int[] pos)
        //{
        //    Name = name;
        //    Image = image;
        //    Position = pos;
        //}

        //Returns the string for the GUI to display
        public string Show()
        {
            return Image;
        }

        //public abstract string Serialize();
        //public abstract Items Deserialize();
    }

    public class InventoryItem: Items, ISerialization<InventoryItem>
    {
        //Sets found to true to say that the player has picked up item.
        public void PickUp()
        {
            Found = true;
        }

        //Provides ability to use the object
        public void Use()
        {

        }
        //Turn the object into a string
        public string Serialize()
        {
            throw new NotImplementedException();
        }

        //Take a string and return an InventoryItem object
        public InventoryItem Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));

            for(int i = 0; i < properties.Count; i++)
            {
                switch (properties[i])
                {
                    case "NAME":
                        Name = properties[i + 1];
                        break;
                    case "IMAGE":
                        Image = properties[i + 1];
                        break;
                    case "FOUND":
                        Found = "TRUE" == properties[i + 1];
                        break;
                }
            }

            return this;
        }

    }

    public class DecoyItem: Items, ISerialization<DecoyItem>
    {
        public int Strength { get; set; }           //

        public void DecrHealth()
        {

        }

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

    //Defines the methods and actions for the Binder class
    public class Binder : InventoryItem, ISerialization<Binder>
    {

        //Reveals the Binder Image;
        public void Reveal(InventoryItem item)
        {

        }
        
        //Take an object and turn it into a string
        new public string Serialize()
        {
            throw new NotImplementedException();
        }

        //Take a string and return a DecoyItem object
        new public Binder Deserialize(string obj)
        {
            throw new NotImplementedException();
        }
    }
    
}
