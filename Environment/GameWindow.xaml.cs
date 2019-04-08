﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Binder.Environment
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Game binderGame;
        Building building;

        public GameWindow(bool cheat, int difficulty)
        {
            //NameScope.SetNameScope(this, new NameScope());
            binderGame = new Game();
            binderGame.IsCheatOn = cheat;
            binderGame.Difficulty = difficulty;
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(CnvsGame_KeyDown);
            BuildWalls();
            building = binderGame.CurBuilding;
            //cnvsGame.DataContext = building;
        }


        private void CnvsGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                binderGame.Marcus.Move('n', binderGame);
            }
            else if (e.Key == Key.Down)
            {
                binderGame.Marcus.Move('s', binderGame);
            }
            else if (e.Key == Key.Left)
            {
                binderGame.Marcus.Move('w', binderGame);
            }
            else if (e.Key == Key.Right)
            {
                binderGame.Marcus.Move('e', binderGame);
            }
            else if (e.Key == Key.C)
            {
                binderGame.Marcus.Attack();
            }
            else if (e.Key == Key.X)
            {
                binderGame.Marcus.Enteract();
            }
            else if (e.Key == Key.Escape)
            {
                Game.isPaused = true;
                Pause pauseWindow = new Pause(binderGame);
                pauseWindow.Show();
            }
        }

        //Builds Walls with Blocks on GUI 
        public void BuildWalls()
        {
            foreach (Walls w in Building.WallsCol)
            {
                foreach (Block b in w.Blocks)
                {
                    Image img = new Image()
                    {
                        Source = new BitmapImage(new Uri("/Environment/blocks.png", UriKind.Relative))
                    };
                    Label block = new Label()
                    {
                        Content = img
                    };

                    block.DataContext = b;

                    block.SetBinding(Canvas.LeftProperty, "X");
                    block.SetBinding(Canvas.TopProperty, "Y");
                    


                    cnvsGame.Children.Add(block);
                }
            }
        }
    }
}