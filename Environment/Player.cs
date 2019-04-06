using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public class Player : MovableCharacter, ISerialization<Player>
    {
        public string Name { get; set; }
        public List<Items> Inventory { get; set; }
        public Player(string name)
        {
            Name = name;
            X = 720;
            Y = 450;
        }
        public void Enteract()
        {

        }

        public void Move(char direction, Game game) //Removed override keyword for buildability
        {
            
            if (direction == 'w')
            {
                if (IsNotWall(1, 0, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Environ)
                    {
                        thing.X += 50;
                    }
                    foreach (Walls wall in Building.WallsCol)
                    {
                        wall.ChangeBlocks('X', 50);
                    }
                }
            }
            else if (direction == 'n')
            {
                if (IsNotWall(0, 1, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Environ)
                    {
                        thing.Y += 50;
                    }
                    foreach (Walls wall in Building.WallsCol)
                    {
                        wall.ChangeBlocks('Y', 50);
                    }
                }
            }
            else if (direction == 'e')
            {
                if (IsNotWall(-1, 0, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Environ)
                    {
                        thing.X -= 50;
                    }
                    foreach (Walls wall in Building.WallsCol)
                    {
                        wall.ChangeBlocks('X', -50);
                    }
                }
            }
            else if (direction == 's')
            {
                if (IsNotWall(0, -1, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Environ) {
                        thing.Y = thing.Y - 50;
                    }
                    foreach (Walls wall in Building.WallsCol)
                    {
                        wall.ChangeBlocks('Y', -50);
                    }
                }
            }
        }

        public bool IsNotWall(int changeInX, int changeInY, Building building)
        {
            int testCoordX = X + changeInX;
            int testCoordY = Y + changeInY;
            foreach (Walls wall in Building.WallsCol)
            {
                foreach (Block block in wall.Blocks)
                {
                    if (((block.Y >= testCoordY) && (block.Y + 24 <= testCoordY)) || ((block.X >= testCoordX) && (block.X + 24 <= testCoordX)))
                    {
                        return false;
                    }
                }
                //if ( (wall.Position[0] + changeInX ) < Position[0] && (wall.Position[0] + changeInX + wall.Width) > Position[0])
                   // if ( (wall.Position[1] + changeInY) < Position[1] && (wall.Position[1] + changeInY + wall.Length) > Position[1])
                     //   return false; 
            }
            return true;
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public Player Deserialize(string obj)
        {
            throw new NotImplementedException();
        }

    }
}
