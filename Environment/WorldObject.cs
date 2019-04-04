using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public class WorldObject
    {
        public int[] Position { get; set; }
        public string PictureName { get; set; }

        public void Pos(int x, int y)
        {
            Position[0] = x;
            Position[1] = y;
        }
    }
}
