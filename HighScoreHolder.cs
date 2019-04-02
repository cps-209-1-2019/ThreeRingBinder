using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder
{
    class HighScoreHolder
    {
        public string highScoreText = "";
        public List<HighScore> scoreList = new List<HighScore>();

        public void Load()
        {
            using (FileStream stream = new FileStream("scores.txt", FileMode.Create))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        string[] scoreArray = new string[2];
                        string scoreLine = reader.ReadLine();
                        if (scoreLine != null)
                        {
                            highScoreText += scoreLine + "\n";
                        }
                        scoreArray = scoreLine.Split(new char[] { ' ' });
                        HighScore highScore = new HighScore(scoreArray[1], scoreArray[0]);
                        scoreList.Insert(0, highScore);
                        scoreList.RemoveAt(-1);
                    }
                }
            }
        }

        public void Save()
        {
            using (FileStream stream = new FileStream("scores.txt", FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    foreach (HighScore score in scoreList)
                    {
                        if (score != null)
                        {
                            string scoreLine = score.PlayerName + " " + score.CurrentScore;
                            writer.WriteLine(scoreLine);
                        }
                    }
                }
            }
            
        }

        public void AddHighScore(HighScore score)
        {
            scoreList.Insert(0, score);
            scoreList.RemoveAt(-1);
        }
    }
}
