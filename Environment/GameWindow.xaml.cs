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
            Debug.WriteLine(Canvas.GetLeft(imgBl) + " " + Canvas.GetTop(imgBl));
            Canvas.SetLeft(imgBl, Canvas.GetLeft(imgBl) - 50);
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
                Pause pauseWindow = new Pause(binderGame);
                pauseWindow.Show();
            }
        }
    }
}
