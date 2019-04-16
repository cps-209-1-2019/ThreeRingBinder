using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public abstract class MovableCharacter : WorldObject
    {
        public int front = 0;
        public int back = 0;
        public int right = 0;
        public int left = 0;
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }

        public virtual void Move()
        {

        }
        public void Attack()
        {
            //for (int i = 1; i < 4; i++)
            //{
            //    foreach (WorldObject wObj in Game.Environ)
            //    {
            //        if ((X + i * changeNum) < wObj.X && wObj.X < (X + (i * changeNum) + changeNum))
            //        {
            //            if (wObj is AI)
            //            {
            //                AI ai = (AI)wObj;
            //                ai.Health -= Damage;
            //                break;
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}
        }
        public bool IsNotWall(int changeInX, int changeInY)
        {
            foreach (Walls wall in Building.WallsCol)
                if ((wall.X < (X + changeInX)) && ((wall.X + wall.Width) > (X + changeInX)))// || ((wall.X < (X + changeInX + 30)) && ((wall.X + wall.Width) > (X + changeInX + 30))))
                    if (((wall.Y < (Y + changeInY)) && ((wall.Y + wall.Length) > (Y + changeInY))) || ((wall.Y < (Y + changeInY + Length - 20)) && ((wall.Y + wall.Length) > (Y + changeInY + Length - 20))))
                        return false;
            return true;
        }
        public bool AIIsNotWall(int changeInX, int changeInY)
        {
            foreach (Walls walls in Building.WallsCol)
            {
                if ((X + changeInX > walls.X) && (X + changeInX < walls.X + walls.Width))
                    if ((Y + changeInY > walls.Y) && (Y + changeInY < walls.Y + walls.Length))
                        return false;
            }
            return true;
        }
    }
}
