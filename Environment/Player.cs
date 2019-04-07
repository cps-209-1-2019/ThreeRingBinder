﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public class Player : MovableCharacter, ISerialization<Player>
    {
        public const int changeNum = 24;
        public string Name { get; set; }
        public List<Items> Inventory { get; set; }
        public Player(string name)
        {
            Name = name;
            X = 720;
            Y = 450;
        }
        public void Enteract()
        {

        }

        public void Move(char direction, Game game) //Removed override keyword for buildability
        {
            
            if (direction == 'w')
            {
                if (IsNotWall(changeNum, 0, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Environ)
                    {
                        thing.X += changeNum;
                    }
                    foreach (Walls wall in Building.WallsCol)
                    {
                        wall.ChangeBlocks('X', changeNum);
                    }
                }
            }
            else if (direction == 'n')
            {
                if (IsNotWall(0, changeNum, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Environ)
                    {
                        thing.Y += changeNum;
                    }
                    foreach (Walls wall in Building.WallsCol)
                    {
                        wall.ChangeBlocks('Y', changeNum);
                    }
                }
            }
            else if (direction == 'e')
            {
                if (IsNotWall(changeNum * -1, 0, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Environ)
                    {
                        thing.X -= changeNum;
                    }
                    foreach (Walls wall in Building.WallsCol)
                    {
                        wall.ChangeBlocks('X', -changeNum);
                    }
                }
            }
            else if (direction == 's')
            {
                if (IsNotWall(0, changeNum * -1, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Environ) {
                        thing.Y = thing.Y - changeNum;
                    }
                    foreach (Walls wall in Building.WallsCol)
                    {
                        wall.ChangeBlocks('Y', -changeNum);
                    }
                }
            }
        }

        public bool IsNotWall(int changeInX, int changeInY, Building building)
        {
            foreach (Walls wall in Building.WallsCol)
            if ((wall.X + changeInX) < X && (wall.X + changeInX + wall.Width) > X)
                if ((wall.Y + changeInY) < Y && (wall.Y + changeInY + wall.Length) > Y)
                    return false;
        return true;
    }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public Player Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));
            Inventory = new List<Items>();

            for (int i = 0; i < properties.Count; i += 2)
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
}
