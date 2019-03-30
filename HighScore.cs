using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder
{
    class HighScore: ISerialization<HighScore>
    {
        public int currentScore;
        public string playerName;
        public HighScore(int score, string name)
        {
            currentScore = score;
            playerName = name;
        }

        //Turn the object into a string to be put in a text file
        public string Serialize()
        {
            throw new NotImplementedException();
        }


        //Takes a string representing the 
        public HighScore Deserialize(string obj)
        {
            throw new NotImplementedException();
        }
        

        
    }
}
