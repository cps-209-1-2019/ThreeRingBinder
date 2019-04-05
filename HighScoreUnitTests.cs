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
            HighScore score = new HighScore("300", "Bob");
            HighScoreHolder testHolder = new HighScoreHolder();
            testHolder.AddHighScore(score);
            HighScore scoreTwo = new HighScore("400", "Joe");
            testHolder.AddHighScore(scoreTwo);
            Assert.IsTrue(testHolder.scoreList[0].CurrentScore == "400");
            Assert.IsTrue(testHolder.scoreList[1].PlayerName == "Bob");
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
