using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    class Player : MovableCharacter, ISerialization<Player>
    {
        public string Name { get; set; }
        public List<Items> Inventory { get; set; }
        public Player(string name)
        {
            Name = name;
        }
        public void Enteract()
        {

        }

        public override void Move(char direction, Game game)
        {
            if (direction == 'w')
            {
                if (IsNotWall())
                {
                    foreach (WorldObject thing in game.eviron)
                        thing.Position[0]++;
                }
            }
            else if (direction == 'n')
            {
                if (IsNotWall())
                {
                    foreach (WorldObject thing in game.eviron)
                        thing.Position[1]++;
                }
            }
            else if (direction == 'e')
            {
                if (IsNotWall())
                {
                    foreach (WorldObject thing in game.eviron)
                        thing.Position[0]--;
                }
            }
            else if (direction == 's')
            {
                if (IsNotWall())
                {
                    foreach (WorldObject thing in game.eviron)
                        thing.Position[1]--;
                }
            }
        }

        public bool IsNotWall()
        {
            return true;
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public Player Deserialize(string obj)
        {
            throw new NotImplementedException();
        }

    }
}
