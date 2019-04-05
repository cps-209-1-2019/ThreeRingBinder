using NUnit.Framework;
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
        string filename = Path.Combine(TestContext.CurrentContext.TestDirectory, "scores.txt");
        public string highScoreText = "";
        public List<HighScore> scoreList = new List<HighScore>();

        public void Load()
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                scoreList = new List<HighScore>();
                for (int i = 0; i < 10; i++)
                {
                    string[] scoreArray = new string[2];
                    string scoreLine = reader.ReadLine();
                    if (scoreLine != null)
                    {
                        highScoreText += scoreLine + "\n";
                        scoreArray = scoreLine.Split(new char[] { ' ' });
                        HighScore highScore = new HighScore(scoreArray[1], scoreArray[0]);
                        scoreList.Insert(0, highScore);
                        if (scoreList.Count() > 9)
                            scoreList.RemoveAt(10);
                    }
                    
                }
            }
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(filename))
                {
                    foreach (HighScore score in scoreList)
                    {
                        if (score != null)
                        {
                            string scoreLine = score.PlayerName + "              " + score.CurrentScore;
                            writer.WriteLine(scoreLine);
                        }
                    }
                }
        }

        public void AddHighScore(HighScore score)
        {
            scoreList.Insert(0, score);
            if (scoreList.Count() > 9)
                scoreList.RemoveAt(10);
            scoreList = scoreList.OrderBy(o => o.CurrentScore).ToList();
            scoreList.Reverse();
        }
    }
}
