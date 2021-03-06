﻿//--------------------------------------------------------------------------------------------
//File:   HighScoreHolder.cs
//Desc:   This class contains logic for the loading and saving high scores.
//---------------------------------------------------------------------------------------------

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
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    scoreList = new List<HighScore>();
                    highScoreText = null;
                    for (int i = 0; i < 10; i++)
                    {
                        string[] scoreArray = new string[2];
                        string scoreLine = reader.ReadLine();
                        if (scoreLine != null)
                        {
                            scoreArray = scoreLine.Split(new char[] { ' ' });
                            HighScore highScore = new HighScore(scoreArray[1], scoreArray[0]);
                            scoreList.Insert(0, highScore);
                            if (scoreList.Count() > 10)
                                scoreList.RemoveAt(10);
                        }

                    }
                    scoreList.Reverse();

                }
            }
            catch
            {
                Console.WriteLine("Could not load high scores");
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
                        string scoreLine = score.PlayerName + " " + score.CurrentScore;
                        writer.WriteLine(scoreLine);
                    }
                }
            }
        }

        public void AddHighScore(HighScore score)
        {
            scoreList.Insert(0, score);
            scoreList = scoreList.OrderBy(o => Convert.ToInt32(o.CurrentScore)).ToList();
            scoreList.Reverse();
            if (scoreList.Count() > 10)
                scoreList.RemoveAt(10);
            Save();
        }
    }
}
