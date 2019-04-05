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
                    foreach (WorldObject thing in game.Eviron)
                        thing.Position[0]++;
                }
            }
            else if (direction == 'n')
            {
                if (IsNotWall(0, 1, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Eviron)
                        thing.Position[1]++;
                }
            }
            else if (direction == 'e')
            {
                if (IsNotWall(-1, 0, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Eviron)
                        thing.Position[0]--;
                }
            }
            else if (direction == 's')
            {
                if (IsNotWall(0, -1, game.CurBuilding))
                {
                    foreach (WorldObject thing in game.Eviron)
                        thing.Position[1]--;
                }
            }
        }

        public bool IsNotWall(int changeInX, int changeInY, Building building)
        {
            foreach (Walls wall in building.WallsCol)
            {
                if ( (wall.Position[0] + changeInX ) < Position[0] && (wall.Position[0] + changeInX + wall.Width) > Position[0])
                    if ( (wall.Position[1] + changeInY) < Position[1] && (wall.Position[1] + changeInY + wall.Length) > Position[1])
                        return false; 
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
