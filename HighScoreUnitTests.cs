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
            Assert.IsTrue(testHolder.scoreList[0].CurrentScore == "300");
        }

        [Test]
        public void HighScoreHolder_Valid_LoadWriteSuccess()
        {
            HighScore score = new HighScore("600", "Joe");
            HighScoreHolder testHolder = new HighScoreHolder();
            testHolder.AddHighScore(score);
            testHolder.Save();
            testHolder.Load();
            Assert.IsTrue(testHolder.scoreList[0].PlayerName == "Joe");
        }
    }
}
