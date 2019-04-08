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
                X -= changeNum;
            }
            else if (direc == 1)
            {
                //east
                X += changeNum;
            }
            else if (direc == 2)
            {
                //north
                Y -= changeNum;
            }
            else if (direc == 3)
            {
                //south
                Y += changeNum;
            }
        }

        public void Chase(Game game)
        {
            if (game.Marcus.X < X)
                if(IsNotWall((-changeNum / 2), 0, game.CurBuilding))
                    X -= changeNum / 2;
            else if (game.Marcus.X > X)
                if (IsNotWall((changeNum / 2), 0, game.CurBuilding))
                    X += changeNum / 2;
            if (game.Marcus.Y < Y)
                if (IsNotWall((-changeNum / 2), 0, game.CurBuilding))
                    Y -= changeNum / 2;
            else if (game.Marcus.Y > Y)
                if (IsNotWall((changeNum / 2), 0, game.CurBuilding))
                    Y += changeNum / 2;
        }

        public void Move(Game game)
        {
            if ((75 * 75) >= (((X - game.Marcus.X) * (X - game.Marcus.X )) + ((Y - game.Marcus.Y) * (Y - game.Marcus.Y))))
            {
                Chase(game);
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
