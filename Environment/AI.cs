using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Binder.Environment
{

    public class AI : MovableCharacter, ISerialization<AI> 
    {
        int horizCount = 0;
        int vertCount = 0;
        string horizDirection = "west";
        string vertDirection = "north";
        bool patrolVertically = false;
        public AI(int health, int damage, int speed)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
        }

        public void PatrolVert(Game game)
        {
            vertCount++;
            if (vertDirection == "north")
            {
                if (AIIsNotWall(0, -changeNum))
                {
                    Y -= changeNum;
                }
                else
                {
                    Y += changeNum;
                    horizDirection = "south";
                }
            }
            else if (horizDirection == "south")
            {
                if (AIIsNotWall(0, changeNum))
                {
                    Y += changeNum;
                }
                else
                {
                    Y -= changeNum;
                    horizDirection = "north";
                }
            }
            if (vertCount > 30)
            {
                patrolVertically = false;
                vertCount = 0;
            }
        }
        public void PatrolHoriz(Game game)
        {
            horizCount++;
            if (horizDirection  == "west")
            {
                if  (AIIsNotWall(-changeNum, 0))
                {
                    X -= changeNum;
                }
                else
                {
                    X += changeNum;
                    horizDirection = "east";
                }
            }
            else if (horizDirection == "east")
            {
                if (AIIsNotWall(changeNum, 0))
                {
                    X += changeNum;
                }
                else
                {
                    X -= changeNum;
                    horizDirection = "west";
                }
            }
            if (horizCount > 25)
            {
                patrolVertically = true;
                horizCount = 0;
            }
        }
        public void Patrol(Building building)
        {
            int direc = 0;
            vertCount++;
            if (vertCount > 10)
            {
                Random rand = new Random();
                direc = rand.Next(4);
            }
            if (direc == 0)
            {
                //west
                if (AIIsNotWall(-changeNum / 2, 0))
                    X -= changeNum;
                else
                {
                    X += changeNum;
                }
            }
            else if (direc == 1)
            {
                //east
                if (AIIsNotWall((changeNum / 2), 0))
                    X += changeNum;
                else
                {
                    X -= changeNum;
                }
            }
            else if (direc == 2)
            {
                //north
                if (AIIsNotWall(0, (-changeNum / 2)))
                    Y -= changeNum;
                else
                {
                    Y += changeNum;
                }
            }
            else if (direc == 3)
            {
                //south
                if (AIIsNotWall(0, (changeNum / 2)))
                    Y += changeNum / 2;
                else
                {
                    Y -= changeNum / 2;
                }
            }
        }

        public void Chase(Game game)
        {
            if (game.Marcus.X < X)
            {
                if (IsNotWall((-changeNum / 2), 0))
                    X -= changeNum / 2;
            }
            else if (game.Marcus.X > X)
            {
                if (IsNotWall((changeNum / 2), 0))
                    X += changeNum / 2;
            }
            if (game.Marcus.Y < Y)
            {
                if (IsNotWall(0, (-changeNum / 2)))
                    Y -= changeNum / 2;
            }
            else if (game.Marcus.Y > Y)
            {
                if (IsNotWall(0, (changeNum / 2)))
                    Y += changeNum / 2;
            }
        }

        public void Move(Game game)
        {
            if (Game.isPaused != true)
            {
                if ((200 * 200) >= (((X - game.Marcus.X) * (X - game.Marcus.X)) + ((Y - game.Marcus.Y) * (Y - game.Marcus.Y))))
                {
                    Chase(game);
                }
                else
                {
                    //if (patrolVertically)
                    //    PatrolVert(game);
                    //else
                    //{
                    //    PatrolHoriz(game);
                    //} 
                    Patrol(game.CurBuilding);
                }
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
