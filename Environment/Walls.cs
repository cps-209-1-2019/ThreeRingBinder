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
    public class Walls: WorldObject, ISerialization<Walls> 
    {
        public int Width { get; set; }                      //Contains the thickness of the wall
        public int Length { get; set; }                     //The number of blocks the wall will contain
        //public int[] Position { get; set; }                 //Takes two arguments an x and y coordinate respectively
        public int Orientation { get; set; }                //1 = Horizontal, 2 = Vertical 
        public List<Block> Blocks { get; set; }              //Holds the blocks that make up the wall

        //public constructor for the walls class
        public Walls(int width, int length, int[] pos)
        {
            Width = width;
            Length = length;
            Position = pos;
        }

        //Builds a wall with respect to the Length
        public void Build()
        {
            for(int i = 0; i < Length; i++)
            {
                if(Orientation == 1)
                {
                    int[] pos = new int[] { Position[0] + i, Position[1] };
                    Block b = new Block(Width, 5, pos);
                    Blocks.Add(b);
                }
                if(Orientation == 2)
                {
                    int[] pos = new int[] { Position[0], Position[1] + i };
                    Block b = new Block(Width, 5, pos);
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
    public class Block: Walls
    {
        public  Block(int width, int length, int[] pos): base(width, length, pos)
        {           
        }

        //Detects whether the player is close or not
        public void Detect()
        {

        }
    }
}
