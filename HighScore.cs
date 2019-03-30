using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder
{
    class HighScore
    {
        public int currentScore;
        public string playerName;
        public HighScore(int score, string name)
        {
            currentScore = score;
            playerName = name;
        }

        //Loads text from high score text file; returns 1 if successful
        public static int Load()
        {

        }

        //Adds new name and score to `text`
        public void AddScore(string text)
        {

        }

        //Writes `text` to the high score text file
        public void Write(string text)
        {

        }
    }
}
