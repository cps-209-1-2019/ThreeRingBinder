using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace Binder
{
    [TestFixture]
    class SerialzationUnitTests
    {
        [Test]
        public void Load_InitialTest_Unknown()
        {
            Game game = new Game();

            game.Load("C:\\Users\\zdd73\\OneDrive - Bob Jones University\\Freshman\\Spring\\CpS 209\\Project\\ThreeRingBinder\\LoadTest.txt");

            Assert.IsTrue(game.HighScore == 550);
            Assert.IsTrue(game.Composure == 2);
            Assert.IsTrue(game.CurrScore == 200);
            Assert.IsTrue(game.Time == 500);
        }

        [Test]
        public void Save_InitialTest_Unknown()
        {
            Game game = new Game() { CurrScore = 330, Time = 450, Composure = 3, HighScore = 1800 };

            game.Save("C:\\Users\\zdd73\\OneDrive - Bob Jones University\\Freshman\\Spring\\CpS 209\\Project\\ThreeRingBinder\\SaveTest.txt");

            Game otherGame = new Game();

            otherGame.Load("C:\\Users\\zdd73\\OneDrive - Bob Jones University\\Freshman\\Spring\\CpS 209\\Project\\ThreeRingBinder\\SaveTest.txt");

            Assert.IsTrue(otherGame.HighScore == 1800);
            Assert.IsTrue(otherGame.Composure == 3);
            Assert.IsTrue(otherGame.CurrScore == 330);
            Assert.IsTrue(otherGame.Time == 450);
        }



    }
}
