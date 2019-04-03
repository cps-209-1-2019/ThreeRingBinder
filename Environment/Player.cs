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

        public override void Move(string direction, Game game)
        {
            if (direction == 'w')
            {
                if (IsNotWall())
                {
                    game.StartPoint[0]++;

                }
            }
            else if (direction == 'n')
            {
                if (IsNotWall())
                {
                    game.StartPoint[1]++;
                }
            }
            else if (direction == 'e')
            {
                if (IsNotWall())
                {
                    game.StartPoint[0]--;
                }
            }
            else if (direction == 's')
            {
                if (IsNotWall())
                {
                    game.StartPoint[1]--;
                }
            }
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
