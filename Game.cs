using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binder.Environment;

namespace Binder
{
    class Game
    {
        public int CurrScore { get; set; }          //Keeps track of the current score as player plays
        public int HighScore { get; set; }          //Keeps track of the High Score so far
        public int Composure { get; set; }          //Keeps track of the health of the Player
        public int Time { get; set; }               //Keeps track of the amount of time remaining
        public int NumItems { get; set; }           //Keeps track of the number of items in players inventory
    }
}
