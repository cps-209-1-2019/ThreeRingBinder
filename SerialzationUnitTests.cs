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
        public void Load_InitialTest_Success()
        {
            Game game = new Game(360);

            game.Load(System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "LoadTest.txt"));

            Assert.IsTrue(game.HighScore == 1500);
            Assert.IsTrue(game.Composure == 3);
            Assert.IsTrue(game.CurrScore == 200);
            Assert.IsTrue(game.Time == 400);
            Assert.IsTrue(game.IsCheatOn);
            Assert.IsTrue(game.NumItems == 3);
            Assert.IsTrue(game.Difficulty == 2);

            Assert.IsTrue(game.CurBuilding.Length == 7300);
            Assert.IsTrue(game.CurBuilding.Width == 2700);
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].Name == "BADGE");
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].Image == "badge.png");
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].Found == true);
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].X == 40);
            Assert.IsTrue(game.CurBuilding.Collection["BADGE"].Y == 50);
            Assert.IsTrue(game.CurBuilding.Collection["HAMMER"].Name == "HAMMER");
            Assert.IsTrue(game.CurBuilding.Collection["BOOK"].Name == "BOOK");

            Assert.IsTrue(game.Marcus.Name == "MARCUS");
            Assert.IsTrue(game.Marcus.Health == 3);
            Assert.IsTrue(game.Marcus.Damage == 1);
            Assert.IsTrue(game.Marcus.Speed == 30);
            Assert.IsTrue(game.Marcus.Inventory[0].Name == "BADGE");
            Assert.IsTrue(game.Marcus.Inventory[0].Image == "badge.png");
            Assert.IsTrue(game.Marcus.Inventory[0].Found == true);
            Assert.IsTrue(game.Marcus.Inventory[0].X == 40);
            Assert.IsTrue(game.Marcus.Inventory[0].Y == 50);
            Assert.IsTrue(game.Marcus.Inventory[1].Name == "BOOK");


        }

        [Test]
        public void Save_InitialTest_Unknown()
        {
            Game game = new Game(360) { CurrScore = 330, Time = 450, Composure = 3, HighScore = 1800, Difficulty = 3, IsCheatOn = false, NumItems = 1 };
            game.CurBuilding = new Environment.Building() { Length = 20, Width = 360, X = 60, Y = 90  };
            game.CurBuilding.Collection.Add("PIPE", new Environment.Items() { Name = "PIPE", Image = "pipe.png", Position = new int[] { 55, 80 }, Found = false});
            game.Marcus = new Environment.Player("MARCUS"){ Health = 3, Speed = 30, Damage = 2 };

            game.Save(System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "SaveTest.txt"));

            Game otherGame = new Game(360);

            otherGame.Load(System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "SaveTest.txt"));

            Assert.IsTrue(otherGame.HighScore == 1800);
            Assert.IsTrue(otherGame.Composure == 3);
            Assert.IsTrue(otherGame.CurrScore == 330);
            Assert.IsTrue(otherGame.Time == 450);
            Assert.IsTrue(otherGame.IsCheatOn == false);
        }
    }
}
