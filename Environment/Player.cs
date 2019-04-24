//--------------------------------------------------------------------------------------------
//File:   Player.cs
//Desc:   This class contains logic for the Player.
//---------------------------------------------------------------------------------------------

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
        public string Direction { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        
        public const int changeNum = 5;
        public Player(string name)
        {
            Inventory = new List<InventoryItem>();
            Name = name;
            X = 654;
            Y = 448;
            Length = 138;
            Width = 134;
            Damage = 1;
            Health = 3;
        }
        public void ChangeYFrames(int changeInY)
        {
            if (changeInY > 0)
            {
                howLongUp = 0;
                howLongRight = 0;
                howLongLeft = 0;
                Direction = "down";
                if (howLongDown >= 0 && howLongDown < 10)
                {
                    PictureName = "/Sprites/MarcusFront.png";
                }
                else if (howLongDown >= 10 && howLongDown < 20)
                {
                    PictureName = "/Sprites/MarcusFront1.png";
                }
                else if (howLongDown >= 20 && howLongDown < 30)
                {
                    PictureName = "/Sprites/MarcusFront.png";
                }
                else if (howLongDown >= 30 && howLongDown < 40)
                {
                    PictureName = "/Sprites/MarcusFront2.png";
                }
                else
                {
                    howLongDown = 0;
                }
                howLongDown++;
            }
            else if (changeInY < 0)
            {
                howLongDown = 0;
                howLongRight = 0;
                howLongLeft = 0;
                Direction = "up";
                if (howLongUp >= 0 && howLongUp < 10)
                {
                    PictureName = "/Sprites/MarcusBack.png";
                }
                else if (howLongUp >= 10 && howLongUp < 20)
                {
                    PictureName = "/Sprites/MarcusBack1.png";
                }
                else if (howLongUp >= 20 && howLongUp < 30)
                {
                    PictureName = "/Sprites/MarcusBack.png";
                }
                else if (howLongUp >= 30 && howLongUp < 40)
                {
                    PictureName = "/Sprites/MarcusBack2.png";
                }
                else
                {
                    howLongUp = 0;
                }
                howLongUp++;
            }
        }
        public void ChangeXFrames(int changeInX)
        {
            if (changeInX > 0)
            {
                howLongDown = 0;
                howLongUp = 0;
                howLongLeft = 0;
                Direction = "right";
                if (howLongRight >= 0 && howLongRight < 10)
                {
                    PictureName = "/Sprites/MarcusRight.png";
                }
                else if (howLongRight >= 10 && howLongRight < 20)
                {
                    PictureName = "/Sprites/MarcusRight1.png";
                }
                else if (howLongRight >= 20 && howLongRight < 30)
                {
                    PictureName = "/Sprites/MarcusRight.png";
                }
                else if (howLongRight >= 30 && howLongRight < 40)
                {
                    PictureName = "/Sprites/MarcusRight2.png";
                }
                else
                {
                    howLongRight = 0;
                }
                howLongRight++;
            }
            else if (changeInX < 0)
            {
                howLongDown = 0;
                howLongUp = 0;
                howLongRight = 0;
                Direction = "left";
                if (howLongLeft >= 0 && howLongLeft < 10)
                {
                    PictureName = "/Sprites/MarcusLeft.png";
                }
                else if (howLongLeft >= 10 && howLongLeft < 20)
                {
                    PictureName = "/Sprites/MarcusLeft1.png";
                }
                else if (howLongLeft >= 20 && howLongLeft < 30)
                {
                    PictureName = "/Sprites/MarcusLeft.png";
                }
                else if (howLongLeft >= 30 && howLongLeft < 40)
                {
                    PictureName = "/Sprites/MarcusLeft2.png";
                }
                else
                {
                    howLongLeft = 0;
                }
                howLongLeft++;
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
                PickUp(game, itemToPickUp);
            }
        }

        //Sets found to true to say that the player has picked up item.
        public void PickUp(Game binderGame, InventoryItem item)
        {
            if (item.canBePickedUp)
            {
                if(item.Name == "drSchaub")
                {
                    item.PictureName = "/Sprites/ignominy.png";
                    Health -= 1;
                }
                if(item.Name == "drMcGee")
                {
                    item.PictureName = "/Sprites/composureTie.png";
                    Health += 1;
                }
                binderGame.Marcus.Inventory.Insert(0, item);
                if (binderGame.Marcus.Inventory.Count > 4)
                {
                    binderGame.Marcus.Inventory.RemoveAt(4);
                }
                item.Found = true;
            }
        }

        public void Move(char direction, Game game) //Removed override keyword for buildability
        {
            if (Game.isPaused != true)
            {
                if (direction == 'w')
                {
                    if (IsNotWall(game.CurBuilding, changeNum, 0))
                    {
                        foreach (WorldObject thing in Game.Environ)
                        {
                            thing.X += changeNum;
                            if (thing is Walls)
                            {
                                Walls wall = (Walls)thing;
                                wall.UpdateBlocks(changeNum, 0);
                            }
                        }
                    }
                    ChangeXFrames(-changeNum);
                }
                else if (direction == 'n')
                {
                    if (IsNotWall(game.CurBuilding, 0, changeNum))
                    {
                        foreach (WorldObject thing in Game.Environ)
                        {
                            thing.Y += changeNum;
                            if (thing is Walls)
                            {
                                Walls wall = (Walls)thing;
                                wall.UpdateBlocks(0, changeNum);
                            }
                        }
                    }
                    ChangeYFrames(-changeNum);
                }
                else if (direction == 'e')
                {
                    if (IsNotWall(game.CurBuilding, -changeNum, 0))
                    {
                        foreach (WorldObject thing in Game.Environ)
                        {
                            thing.X -= changeNum;
                            if (thing is Walls)
                            {
                                Walls wall = (Walls)thing;
                                wall.UpdateBlocks(-changeNum, 0);
                            }
                        }
                    }
                    ChangeXFrames(changeNum);
                }
                else if (direction == 's')
                {
                    if (IsNotWall(game.CurBuilding, 0, -changeNum))
                    {
                        foreach (WorldObject thing in Game.Environ)
                        {
                            thing.Y = thing.Y - changeNum;
                            if (thing is Walls)
                            {
                                Walls wall = (Walls)thing;
                                wall.UpdateBlocks(0, -changeNum);
                            }
                        }
                    }
                    ChangeYFrames(changeNum);
                }
            }
        }


        //Turns the Player object into a string
        public string Serialize()
        {
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
        
        //Fills a Player object with data from a string
        public Player Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));
            Inventory = new List<InventoryItem>();

            for (int i = 1; i < properties.Count; i += 1)
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
                        string itemString = string.Format("{0}?{1},{2}!{3},{4}!{5},{6}!{7},{8}!{9},{10}!{11},{12}!{13},{14}!{15}", properties[i], properties[i + 1], properties[i + 2], properties[i + 3], properties[i + 4], properties[i + 5], properties[i + 6], properties[i + 7], properties[i + 8], properties[i + 9], properties[i + 10], properties[i + 11], properties[i + 12], properties[i + 13], properties[i + 14], properties[i + 15]);
                        Inventory.Add(item.Deserialize(itemString));
                        i = i + 15;
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
        public bool Destroy { get; set; }
        public string Direction { get; set; }
        public Airplane(Player player)
        {
            Stage = 0;
            Damage = player.Damage;
            X = player.X + 30;
            Y = player.Y + 40;
            Direction = player.Direction;
            if (player.Direction == "up")
            { 
                PictureName = "/Sprites/paperAirplaneUp.jpg";
            }
            else if (player.Direction == "down")
            {
                PictureName = "/Sprites/paperAirplaneDown.jpg";
            }
            else if (player.Direction == "left")
            {
                PictureName = "/Sprites/paperAirplaneLeft.jpg";
            }
            else if (player.Direction == "right")
            {
                PictureName = "/Sprites/paperAirplaneRight.jpg";
            }
        }
        public void Update()
        {
            if (Direction == "up")
            {
                Y -= 5;
                Hit(0, -5);
            }
            if (Direction == "down")
            {
                Y += 5;
                Hit(0, 5);
            }
            if (Direction == "right")
            {
                X += 5;
                Hit(5, 0);
            }
            if (Direction == "left")
            {
                X -= 5;
                Hit(-5, 0);
            }
            Stage++;
            if (Stage > 58)
            {
                Destroy = true;
            }
        }
        public void Hit(int changeInX, int changeInY)
        {
            foreach (WorldObject thing in Game.Environ)
            {
                if (thing is Walls || thing is AI)
                {
                    if ((thing.X < (X + changeInX)) && ((thing.X + thing.Width) > (X + changeInX)) || ((thing.X < (X + changeInX + 30)) && ((thing.X + thing.Width) > (X + changeInX + 30))))
                    {
                        if (((thing.Y < (Y + changeInY)) && ((thing.Y + thing.Length) > (Y + changeInY))) || ((thing.Y < (Y + changeInY + Length - 20)) && ((thing.Y + thing.Length) > (Y + changeInY + Length - 20))))
                        {
                            Destroy = true;
                            if (thing is AI)
                            {
                                AI ai = (AI)thing;
                                ai.Health -= Damage;
                            }
                        }
                    }
                }
            }
        }
    }
}
