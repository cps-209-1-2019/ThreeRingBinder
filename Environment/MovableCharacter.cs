using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    class MovableCharacter
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }

        public MovableCharacter(int health, int damage, int speed)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
        }

        public void Move
        {

        }
        public void Attack
        {

        }
    }
}
