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
        public bool IsNotWall(Building building, int changeInX, int changeInY)
        {
            foreach (Walls wall in building.WallsCol)
                if (((wall.X + changeInX < X) && ((wall.X + wall.Width + changeInX) > X)) || (((wall.X + changeInX) < (X + Width)) && ((wall.X + wall.Width + changeInX) > (X + Width))))
                    if (((wall.Y + changeInY < Y) && ((wall.Y + wall.Length + changeInY) > Y)) || ((changeInY + wall.Y < (Y + Length)) && ((changeInY + wall.Y + wall.Length) > (Y + Length))))
                        if (((X < wall.X + changeInX) && ((X + Width) > wall.X + changeInX)) || ((X < wall.X + wall.Width + changeInX) && ((X + Width) > wall.X + wall.Width + changeInX)))
                            if (((Y < wall.Y + changeInY) && ((Y + Length) > wall.Y + changeInY)) || ((Y < wall.Y + wall.Length + changeInY) && ((Y + Length) > wall.Y + wall.Length + changeInY)))
                                return false;
            return true;
        }
        //public bool AIIsNotWall(Building building, int changeInX, int changeInY)
        //{
        //    foreach (Walls walls in building.WallsCol)
        //    {
        //        if ((X + changeInX > walls.X) && (X + changeInX < walls.X + walls.Width))
        //            if ((Y + changeInY > walls.Y) && (Y + changeInY < walls.Y + walls.Length))
        //                return false;
        //    }
        //    return true;
        //}
    }
}
