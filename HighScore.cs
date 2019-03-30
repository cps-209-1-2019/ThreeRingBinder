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
        public int playerName;
        public HighScore(int score, int name)
        {
            currentScore = score;
            playerName = name;
        }

        //Loads text from high score text file
        public void Load()
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
