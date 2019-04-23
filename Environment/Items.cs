//--------------------------------------------------------------------------------------------
//File:   Items.cs
//Desc:   This class contains logic for the items in the room.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binder;

namespace Binder.Environment
{
    //Added public accessibility - Day
    public class Items : WorldObject
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Found { get; set; }

        
        public string Show()
        {
            return Image;
        }

        public string Serialize()
        {
            string theItem = "";

            theItem = string.Format("INVENTORYITEM?5,NAME!{0},IMAGE!{1},FOUND!{2},POSX!{3},POSY!{4}", Name.ToUpper(), Image, Found.ToString().ToUpper(), X, Y);

            return theItem;
        }
    }

    public class InventoryItem: Items, ISerialization<InventoryItem>
    {
        public bool canBePickedUp = true;
        public bool isTheOne = false;

        //Provides ability to use the object
        public void Use(Game game)
        {
            if ((200 * 200) >= (((X - game.ring.X) * (X - game.ring.X)) + ((Y - game.ring.Y) * (Y - game.ring.Y))))
            {
                if (isTheOne == true)
                    game.isRingFound = true;
            }
        }
        //Turn the object into a string
        new public string Serialize()
        {
            string theItem;

            string dir = Directory.GetCurrentDirectory();
            string theImage = Image.Replace(dir.Replace("\\bin\\Debug", ""), "");

            theItem = string.Format("INVENTORYITEM?5,NAME!{0},IMAGE!{1},FOUND!{2},POSX!{3},POSY!{4},CANBEPICKEDUP!{5},ISTHEONE!{6}",Name.ToUpper(), theImage, Found.ToString().ToUpper(), X, Y, canBePickedUp.ToString().ToUpper(), isTheOne.ToString().ToUpper());

            return theItem;
        }

        //Take a string and return an InventoryItem object
        public InventoryItem Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));
            
            Position = new int[2];

            for(int i = 0; i < properties.Count; i++)
            {
                switch (properties[i])
                {
                    case "NAME":
                        Name = properties[i + 1];
                        break;
                    case "IMAGE":
                        if(properties[i + 1] == "null")
                        {
                            Image = properties[i + 1];
                        }
                        else
                        {
                            Image = "\\Sprites\\binderRingSilver.png";
                        }
                        break;
                    case "FOUND":
                        Found = "TRUE" == properties[i + 1];
                        break;
                    case "POSX":
                        X = int.Parse(properties[i + 1]);
                        break;
                    case "POSY":
                        Y = int.Parse(properties[i + 1]);
                        break;
                    case "CANBEPICKEDUP":
                        canBePickedUp = "TRUE" == properties[i + 1];
                        break;
                    case "ISTHEONE":
                        isTheOne = "TRUE" == properties[i + 1];
                        break;
                }
            }
            return this;
        }

    }

    public class DecoyItem: Items, ISerialization<DecoyItem>
    {
        public int Strength { get; set; }

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
    public class BinderRing : InventoryItem, ISerialization<BinderRing>
    {
        new public string Serialize()
        {
            string theItem = "";

            theItem = string.Format("RING?3,FOUND!{0},POSX!{1},POSY!{2},IMAGE!{3}",  Found.ToString().ToUpper(), X, Y, Image);

            return theItem;
        }

        //Take a string and return a DecoyItem object
        new public BinderRing Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));

            Position = new int[2];

            for (int i = 0; i < properties.Count; i++)
            {
                switch (properties[i])
                {
                    case "FOUND":
                        Found = "TRUE" == properties[i + 1];
                        break;
                    case "POSX":
                        X = int.Parse(properties[i + 1]);
                        break;
                    case "POSY":
                        Y = int.Parse(properties[i + 1]);
                        break;
                    case "IMAGE":
                        Image = properties[i + 1];
                        break;
                }
            }

            return this;
        }
    }
    
}
