﻿//--------------------------------------------------------------------------------------------
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
        public int Orientation { get; set; }

        public Walls(int width, int length, int[] pos)
        {
            Width = width;
            Length = length;
            Position = pos;
        }

        //Builds a wall with respect to the Length
        public void Build()
        {
            
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
