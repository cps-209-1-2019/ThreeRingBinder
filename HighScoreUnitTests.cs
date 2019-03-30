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
        public void HighScore_ValidScoreName_Success()
        {
            HighScore newScore = new HighScore(300, "zach");
            Assert.IsTrue(newScore.currentScore == 300);
            Assert.IsTrue(newScore.playerName == "zach");
        }

        [Test]
        public void HighScore_AlsoValid_LoadSuccess()
        {
            HighScore newScore = new HighScore(1450, "melchi");
            Assert.IsTrue(HighScore.Load() == 1);
        }
    }
}
