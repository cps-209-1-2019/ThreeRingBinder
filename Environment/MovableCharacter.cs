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
        public const int changeNum = 69;

        public virtual void Move()
        {

        }
        public void Attack(Game game)
        {
            for (int i = 0; i < 4; i++)
            {
                foreach (WorldObject wObj in Game.Environ)
                {
                    if (wObj is AI)
                    {
                        if ((X + i * 40) < wObj.X && wObj.X < (X + (i * 40) + 40))
                        {

                        }
                    }
                }
            }
        }
        public bool IsNotWall(int changeInX, int changeInY, Building building)
        {
            foreach (Walls wall in Building.WallsCol)
                if (((wall.X + changeInX) < X && (wall.X + changeInX + wall.Width) > X))
                    if ((wall.Y + changeInY) < Y && (wall.Y + changeInY + wall.Length) > Y)
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
