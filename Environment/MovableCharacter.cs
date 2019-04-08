using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public abstract class MovableCharacter : WorldObject
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public const int changeNum = 24;

        public virtual void Move()
        {
            
        }
        public void Attack()
        {

        }
    }
}
