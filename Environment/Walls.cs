//--------------------------------------------------------------------------------------------
//File:   Walls.cs
//Desc:   This class describes the behavior of the walls in the Building.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    //Added public accessibility - Day
    public class Walls : ISerialization<Walls>
    {
        public int Width { get; set; }
        public int Length { get; set; }
        public int[] Position { get; set; }

        public Walls(int width, int length, int[] pos)
        {
            Width = width;
            Length = length;
            Position = pos;
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public Walls Deserialize(string obj)
        {
            throw new NotImplementedException();
        }
    }
}
