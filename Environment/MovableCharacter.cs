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
        public bool IsNotWall(int changeInX, int changeInY, Building building)
        {
            foreach (Walls wall in Building.WallsCol)
                if ((wall.X + changeInX) < X && (wall.X + changeInX + wall.Width) > X)
                    if ((wall.Y + changeInY) < Y && (wall.Y + changeInY + wall.Length) > Y)
                        return false;
            return true;
        }
    }
}
