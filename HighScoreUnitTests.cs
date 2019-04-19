using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder
{
    [TestFixture]
    class HighScoreUnitTests
    {
        [Test]
        public void HighScoreHolder_ValidScore_Success()
        {
            HighScore score = new HighScore("400", "Bob");
            HighScoreHolder testHolder = new HighScoreHolder();
            testHolder.AddHighScore(score);
            HighScore scoreTwo = new HighScore("300", "Joe");
            testHolder.AddHighScore(scoreTwo);
            Assert.IsTrue(testHolder.scoreList[0].CurrentScore == "400");
            Assert.IsTrue(testHolder.scoreList[1].PlayerName == "Joe");
        }

        [Test]
        public void HighScoreHolder_MultipleValidScores_Success()
        {
            HighScore score = new HighScore("450", "Dave");
            HighScoreHolder testHolder = new HighScoreHolder();
            testHolder.AddHighScore(score);
            HighScore scoreTwo = new HighScore("300", "Joe");
            testHolder.AddHighScore(scoreTwo);
            HighScore scoreThree = new HighScore("1050", "Jim");
            testHolder.AddHighScore(scoreThree);
            Assert.IsTrue(testHolder.scoreList[0].CurrentScore == "1050");
            Assert.IsTrue(testHolder.scoreList[1].PlayerName == "Dave");
            Assert.IsTrue(testHolder.scoreList[2].CurrentScore == "300");
        }

        [Test]
        public void HighScoreHolder_Valid_LoadWriteSuccess()
        {
            HighScore score = new HighScore("600", "Joe");
            HighScoreHolder testHolder = new HighScoreHolder();
            testHolder.AddHighScore(score);
            testHolder.Save();
            HighScoreHolder testHolderTwo = new HighScoreHolder();
            testHolderTwo.Load();
            Assert.IsTrue(testHolderTwo.scoreList[0].PlayerName == "Joe");
        }
    }
}
