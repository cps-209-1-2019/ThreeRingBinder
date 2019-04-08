using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public class AI : MovableCharacter, ISerialization<AI> 
    {

        public AI(int health, int damage, int speed)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
        }
        public void Patrol()
        {
            Random rand = new Random();
            int direc = rand.Next(4);
            if (direc == 0)
            {
                //west
                Position[0] -= changeNum;
            }
            else if (direc == 1)
            {
                //east
                Position[0] += changeNum;
            }
            else if (direc == 2)
            {
                //north
                Position[1] -= changeNum;
            }
            else if (direc == 3)
            {
                //south
                Position[1] += changeNum;
            }
        }

        public void Chase(Player player)
        {
            if (player.Position[0] < Position[0])
                Position[0]--;
            else if (player.Position[0] > Position[0])
                Position[0]++;
            if (player.Position[1] < Position[1])
                Position[1]--;
            else if (player.Position[1] > Position[1])
                Position[1]++;
        }

        public void Move(Player player)
        {
            if ((75 * 75) >= (((Position[0] -  player.Position[0]) * (Position[0] - player.Position[0] )) + ((Position[1] - player.Position[1]) * (Position[1] - player.Position[1]))))
            {
                Chase(player);
            }
            else
            {
                Patrol();
            }
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public AI Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));

            for (int i = 0; i < properties.Count; i += 2)
            {
                switch (properties[i])
                {
                    case "HEALTH":
                        Health = int.Parse(properties[i + 1]);
                        break;
                    case "DAMAGE":
                        Damage = int.Parse(properties[i + 1]);
                        break;
                    case "SPEED":
                        Speed = int.Parse(properties[i + 1]);
                        break;
                }
            }

            return this;
        }
    }
}
