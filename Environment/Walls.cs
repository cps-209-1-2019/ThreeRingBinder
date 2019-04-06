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
    //Added public accessibility - Day
    public class Walls: WorldObject, ISerialization<Walls>
    {
        private int[] posW;
        public int Width { get; set; }                      //Contains the thickness of the wall
        public int Length { get; set; }                     //The number of blocks the wall will contain              
        public int Orientation { get; set; }                //1 = Horizontal, 2 = Vertical 
        public List<Block> Blocks { get; set; }              //Holds the blocks that make up the wall

        public override int[] Position
        {
            get
            {
                return posW;
            }
            set
            {
                int x = 0;
                int y = 0;

                if (posW != null)
                {
                    x = posW[0];
                    y = posW[1];
                }                

                posW = value;
                SetProperty("Position");
                X = posW[0];
                Y = posW[1];

                foreach(Block b in Blocks)
                {
                    if(x != posW[0])
                    {
                        int xChanged = posW[0] - x;
                        b.X += xChanged;
                    }
                    if(y != posW[1])
                    {
                        int yChanged = posW[1] - y;
                        b.Y += yChanged;
                    }
                }
            }
        }

        //public constructor for the walls class
        public Walls(int width, int length, int[] pos)
        {
            Blocks = new List<Block>();
            Width = width;
            Length = length;

            X = pos[0];
            Y = pos[1];

            Position = pos;

            Build();

                        
        }

        //Builds a wall with respect to the Length
        void Build()
        {
            int max = Math.Max(Length, Width);

            for(int i = 0; i < max; i+=24)
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

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public Walls Deserialize(string obj)
        {
            throw new NotImplementedException();
        }
    }

    //Defines a class Block from which the walls will be built.
    public class Block : WorldObject
    {

        public Block(int[] pos)
        {
            Position = new int[2];
            Position[0] = pos[0];
            Position[1] = pos[1];

            X = pos[0];
            Y = pos[1];
        }
        //Detects whether the player is close or not
        public void Detect()
        {

        }
    }
}
