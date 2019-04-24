//--------------------------------------------------------------------------------------------
//File:   Walls.cs
//Desc:   This class describes the behavior of the walls in the Building.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public class Walls: WorldObject, ISerialization<Walls>
    {             
        public List<Block> Blocks { get; set; }              //Holds the blocks that make up the wall

        public Walls()
        {
        }

        //public constructor for the walls class
        public Walls(int width, int length, int[] pos)
        {
            Blocks = new List<Block>();
            Width = width;
            Length = length;
            X = pos[0];
            Y = pos[1];

            Build();                        
        }

        //Builds a wall with respect to the Length
        void Build()
        {
            int max = Math.Max(Length, Width);

            for(int i = 0; i <= max; i+=69)
            {
                if(max == Width)
                {
                    int[] pos = new int[] { X + i, Y};
                    Block b = new Block(pos);
                    Blocks.Add(b);
                }
                if(max == Length)
                {
                    int[] pos = new int[] { X, Y + i };
                    Block b = new Block(pos);
                    Blocks.Add(b);
                }
            }
        }

        public void UpdateBlocks(int x, int y)
        {
            foreach(Block b in Blocks)
            {
                b.X += x;
                b.Y += y;
            }
        }
        public string Serialize()
        {
            return string.Format("WALLS?3,X!{0},Y!{1},WIDTH!{2},LENGTH!{3}", X, Y, Width, Length);
        }

        public Walls Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));

            for (int i = 0; i < properties.Count; i += 2)
            {
                switch (properties[i])
                {
                    case "LENGTH":
                        Length = int.Parse(properties[i + 1]);
                        break;
                    case "X":
                        X = int.Parse(properties[i + 1]);
                        break;
                    case "Y":
                        Y = int.Parse(properties[i + 1]);
                        break;
                    case "WIDTH":
                        Width = int.Parse(properties[i + 1]);
                        break;
                    
                }
            }

            Build();

            return this;
        }
    }

    //Defines a class Block from which the walls will be built.
    public class Block : WorldObject
    {
        public Block(int[] pos)
        {
            X = pos[0];
            Y = pos[1];
        }
    }
}
