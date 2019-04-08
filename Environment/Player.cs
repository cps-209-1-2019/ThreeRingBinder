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
                }
            }
            else if (direction == 's')
            {
                if (IsNotWall(0, changeNum * -1, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Environ) {
                        thing.Y = thing.Y - changeNum;
                    }
                }
            }
        }



        public string Serialize()
        {
            Inventory = new List<Items>();
            string thePlayer = "";
            string theInventory = "";
            foreach(Items items in Inventory)
            {
                InventoryItem inv = items as InventoryItem;
                theInventory += inv.Serialize() + ";";
            }


            thePlayer = string.Format("PLAYER?5,NAME!{0},HEALTH!{1},DAMAGE!{2},SPEED!{3},INVENTORY#{4}!{5}", Name, Health, Damage, Speed, Inventory.Count, theInventory);

            return thePlayer;
        }

        public Player Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));
            Inventory = new List<Items>();

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
