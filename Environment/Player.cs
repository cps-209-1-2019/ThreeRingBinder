using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public class Player : MovableCharacter, ISerialization<Player>
    {
        public string Name { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public Player(string name)
        {
            Inventory = new List<InventoryItem>();
            Name = name;
            X = 654;
            Y = 448;
            Length = 138;
            Width = 134;
            
        }
        public void ChangeXFrames(int changeInX)
        {
            if (changeInX > 0)
            {
                if (front == 0)
                {
                    PictureName = "/Sprites/MarcusFront.png";
                    front = 1;
                }
                else if (front == 1)
                {
                    PictureName = "/Sprites/MarcusFront1.png";
                    front = 2;
                }
                else if (front == 2)
                {
                    PictureName = "/Sprites/MarcusFront.png";
                    front = 3;
                }
                else
                {
                    PictureName = "/Sprites/MarcusFront2.png";
                    front = 0;
                }
            }
            else if (changeInX < 0)
            {
                if (back == 0)
                {
                    PictureName = "/Sprites/MarcusBack.png";
                    back = 1;
                }
                else if (back == 1)
                {
                    PictureName = "/Sprites/MarcusBack1.png";
                    back = 2;
                }
                else if (back == 20)
                {
                    PictureName = "/Sprites/MarcusBack.png";
                    back = 3;
                }
                else
                {
                    PictureName = "/Sprites/MarcusBack2.png";
                    back = 0;
                }
            }
        }
        public void ChangeYFrames(int changeInX)
        {
            if (changeInX > 0)
            {
                if (right == 0)
                {
                    PictureName = "/Sprites/MarcusRight.png";
                    right = 1;
                }
                else if (right == 1)
                {
                    PictureName = "/Sprites/MarcusRight1.png";
                    right = 2;
                }
                else if (right == 2)
                {
                    PictureName = "/Sprites/MarcusRight.png";
                    right = 3;
                }
                else
                {
                    PictureName = "/Sprites/MarcusRight2.png";
                    right = 0;
                }
            }
            else if (changeInX < 0)
            {
                if (left == 0)
                {
                    PictureName = "/Sprites/MarcusLeft.png";
                    left = 1;
                }
                else if (left == 1)
                {
                    PictureName = "/Sprites/MarcusLeft1.png";
                    left = 2;
                }
                else if (left == 20)
                {
                    PictureName = "/Sprites/MarcusLeft.png";
                    left = 3;
                }
                else
                {
                    PictureName = "/Sprites/MarcusLeft2.png";
                    left = 0;
                }
            }
        }
        public void Enteract(Game game)
        {
            InventoryItem itemToPickUp = null;
            foreach (WorldObject thing in Game.Environ) {
                int distanceNum = 100;
                if (thing is InventoryItem)
                {
                    InventoryItem item = (InventoryItem)thing;
                    if ((distanceNum * distanceNum) >= (((X - item.X) * (X - item.X)) + ((Y - item.Y) * (Y - item.Y))))
                    {
                        itemToPickUp = item;
                        distanceNum = ((X - item.X) * (X - item.X)) + ((Y - item.Y) * (Y - item.Y));
                    }
                }
            }
            if (itemToPickUp != null)
            {
                itemToPickUp.PickUp(game);
            }
        }

        public void Move(char direction, Game game) //Removed override keyword for buildability
        {
            if (Game.isPaused != true)
            {
                if (direction == 'w')
                {
                    if (IsNotWall(changeNum, 0))
                    {
                        foreach (WorldObject thing in Game.Environ)
                        {
                            thing.X += changeNum;
                        }
                    }
                    ChangeYFrames(-changeNum);
                }
                else if (direction == 'n')
                {
                    if (IsNotWall(0, changeNum))
                    {
                        foreach (WorldObject thing in Game.Environ)
                        {
                            thing.Y += changeNum;
                        }
                    }
                    ChangeXFrames(-changeNum);
                }
                else if (direction == 'e')
                {
                    if (IsNotWall(changeNum * -1, 0))
                    {
                        foreach (WorldObject thing in Game.Environ)
                        {
                            thing.X -= changeNum;
                        }
                    }
                    ChangeYFrames(changeNum);
                }
                else if (direction == 's')
                {
                    if (IsNotWall(0, changeNum * -1))
                    {
                        foreach (WorldObject thing in Game.Environ)
                        {
                            thing.Y = thing.Y - changeNum;
                        }
                    }
                    ChangeXFrames(changeNum);
                }
            }
        }



        public string Serialize()
        {
            Inventory = new List<InventoryItem>();
            string thePlayer = "";
            string theInventory = "";
            foreach(Items items in Inventory)
            {
                InventoryItem inv = items as InventoryItem;
                theInventory += inv.Serialize() + ";";
            }


            thePlayer = string.Format("PLAYER?5,NAME!{0},HEALTH!{1},DAMAGE!{2},SPEED!{3},POSX!{4},POSY!{5},INVENTORY#{6}!{7}", Name, Health, Damage, Speed, X, Y, Inventory.Count, theInventory);

            return thePlayer;
        }

        public Player Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));
            Inventory = new List<InventoryItem>();

            for (int i = 1; i < properties.Count; i += 2)
            {
                switch (properties[i])
                {
                    case "NAME":
                        Name = properties[i + 1];
                        break;
                    case "HEALTH":
                        Health = int.Parse(properties[i + 1]);
                        break;
                    case "DAMAGE":
                        Damage = int.Parse(properties[i + 1]);
                        break;
                    case "SPEED":
                        Speed = int.Parse(properties[i + 1]);
                        break;
                    case "POSX":
                        X = int.Parse(properties[i + 1]);
                        break;
                    case "POSY":
                        Y = int.Parse(properties[i + 1]);
                        break;
                    case "INVENTORYITEM":
                        InventoryItem item = new InventoryItem();
                        string itemString = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9},{10}!{11}", properties[i], properties[i + 1], properties[i + 2], properties[i + 3], properties[i + 4], properties[i + 5], properties[i + 6], properties[i + 7], properties[i + 8], properties[i + 9], properties[i + 10], properties[i + 11]);
                        Inventory.Add(item.Deserialize(itemString));
                        i = i + 10;
                        break;
                }
            }

            return this;
        }

    }

    public class Airplane : WorldObject
    {
        public int Damage { get; set; }
        public int Stage { get; set; }
        public void Update()
        {
            Stage++;
            if (Stage > 4)
            {
                Destroy();
            }
            foreach (WorldObject wObj in Game.Environ)
            {

            }

        }
        public bool isHit()
        {
            foreach (WorldObject thing in Game.Environ)
                if (thing.X > (X + Width) || (thing.X + thing.Width) < X)
                    if (thing.Y < (Y + Length) || (thing.Y + thing.Length) > Y)
                        return true;
            return false;
        }
        public void Destroy()
        {

        }
    }
}
