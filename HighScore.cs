using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder
{
    class HighScore
    {
        public string CurrentScore { get; set; }
        public string PlayerName { get; set; }
        public HighScore(string score, string name)
        {
            CurrentScore = score;
            PlayerName = name;
        }        
    }
}
