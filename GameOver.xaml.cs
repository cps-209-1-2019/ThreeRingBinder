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

namespace Binder.Environment
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        GameWindow gameWindow;
        public GameOver(GameWindow window, bool isGameWon, int points)
        {
            gameWindow = window;
            InitializeComponent();
            if (isGameWon)
            {
                lblResults.Content = "You Win!";
                Image img = new Image()
                {
                    Source = new BitmapImage(new Uri("/Sprites/completeBinder.png", UriKind.Relative))
                };
                Label label = new Label()
                {
                    Content = img
                };
                label.Width = 60;
                Canvas.SetLeft(label, 195);
                Canvas.SetTop(label, 205);
                CnvsGameOver.Children.Add(label);
            }
            else
                lblResults.Content = "You lost";
            Game.isPaused = true;
            lblPoints.Content = points + " Points!";
            CheckHighScore(points);
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow title = new MainWindow();
            title.Show();
            this.Close();
            gameWindow.Close();
        }
        private void CheckHighScore(int score)
        {
            HighScoreHolder holder = new HighScoreHolder();
            holder.Load();
            int index = holder.scoreList.Count() - 1;
            if ((index != -1) && score > Convert.ToInt32(holder.scoreList[index].CurrentScore))
            {
                NewHighScore newScore = new NewHighScore(score);
                newScore.Show();
                newScore.Topmost = true;
            }
        }

        private void BtnEndGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            gameWindow.Close();
        }
    }
}
