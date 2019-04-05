using System;
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
        public GameWindow(bool cheat, int difficulty)
        {
            binderGame = new Game();
            binderGame.IsCheatOn = cheat;
            binderGame.Difficulty = difficulty;
            InitializeComponent();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TranslateTransform transform = new TranslateTransform(50, 20);
            imgBl.RenderTransform = transform;

            BuildWalls();
            //Canvas.SetLeft(imgBl, Canvas.GetLeft(imgBl) - 50);
            //cnvsGame.Children.Remove(btnStart);
            
        }

        private void CnvsGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                //binderGame.Marcus.Move('n', binderGame);
                Canvas.SetTop(imgBl, Canvas.GetTop(imgBl) - 50);
            }
            else if (e.Key == Key.Down)
            {
                //binderGame.Marcus.Move('s', binderGame);
                Canvas.SetTop(imgBl, Canvas.GetTop(imgBl) + 50);
            }
            else if (e.Key == Key.Left)
            {
                //binderGame.Marcus.Move('w', binderGame);
                ////Point p = new Point(50, 0);
                ////imgBl.RenderTransform.Transform(p);
                Canvas.SetLeft(imgBl, Canvas.GetLeft(imgBl) - 50);
            }
            else if (e.Key == Key.Right)
            {
                //binderGame.Marcus.Move('e', binderGame);
                Canvas.SetLeft(imgBl, Canvas.GetLeft(imgBl) + 50);
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

            //Debug.WriteLine(Canvas.GetLeft(imgBl) + " " + Canvas.GetTop(imgBl));
            //Debug.WriteLine(imgBl.RenderTransform.Value);
        }

        //Builds Walls with Blocks on GUI 
        public void BuildWalls()
        {
            //List<int[]> coords(params not yet needed)
            int[] c = new int[] { 200, 50 };
            Block b = new Block(24, 24, c);

            Image img = new Image()
            {
                Source = new BitmapImage(new Uri("/Environment/blocks.png", UriKind.Relative))
            };
            Label block = new Label()
            {
                Content = img
            };
            block.DataContext = b;
            block.SetBinding(render, "Position");

            cnvsGame.Children.Add(block);
            Canvas.SetTop(block, 0);
            Canvas.SetLeft(block, 0);
        }
    }
}
