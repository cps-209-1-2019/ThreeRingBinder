﻿using System;
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

            Assert.IsTrue(game.HighScore == 1500);
            Assert.IsTrue(game.Composure == 3);
            Assert.IsTrue(game.CurrScore == 200);
            Assert.IsTrue(game.Time == 400);
            Assert.IsTrue(game.IsCheatOn);
            Assert.IsTrue(game.NumItems == 3);
            Assert.IsTrue(game.CurBuilding.Length == 7300);
            Assert.IsTrue(game.CurBuilding.Width == 2700);
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].Name == "BADGE");
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].Image == "badge.png");
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].Found == true);
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].Position[0] == 40);
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].Position[1] == 50);
            Assert.IsTrue(game.CurBuilding.Collection["HAMMER"].Name == "HAMMER");
            Assert.IsTrue(game.CurBuilding.Collection["BOOK"].Name == "BOOK");
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
