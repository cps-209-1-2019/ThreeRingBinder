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

        }

        public void Chase(Player player)
        {
            
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
            throw new NotImplementedException();
        }
    }
}
