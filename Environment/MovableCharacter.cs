using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public abstract class MovableCharacter : WorldObject
    {
        public int howLongUp;
        public int howLongDown;
        public int howLongRight;
        public int howLongLeft;
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }

        public virtual void Move()
        {

        }
        public bool IsNotWall(Building building, int changeInX, int changeInY)
        {
            foreach (Walls wall in building.WallsCol)
            {
                if (((wall.X + changeInX < X) && ((wall.X + wall.Width + changeInX) > X)) || (((wall.X + changeInX) < (X + Width)) && ((wall.X + wall.Width + changeInX) > (X + Width))))
                    if (((wall.Y + changeInY < Y) && ((wall.Y + wall.Length + changeInY) > Y)) || ((changeInY + wall.Y < (Y + Length)) && ((changeInY + wall.Y + wall.Length) > (Y + Length))))
                        return false;
                else if (((X < wall.X + changeInX) && ((X + Width) > wall.X + changeInX)) || ((X < wall.X + wall.Width + changeInX) && ((X + Width) > wall.X + wall.Width + changeInX)))
                    if (((Y < wall.Y + changeInY) && ((Y + Length) > wall.Y + changeInY)) || ((Y < wall.Y + wall.Length + changeInY) && ((Y + Length) > wall.Y + wall.Length + changeInY)))
                        return false;
            }

            return true;
        }
    }
}
