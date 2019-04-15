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
        public GameOver(GameWindow window, bool isGameWon)
        {
            gameWindow = window;
            InitializeComponent();
            if (isGameWon)
                lblResults.Content = "You Win!";
            else
                lblResults.Content = "You lost";
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow title = new MainWindow();
            title.Show();
            this.Close();
            gameWindow.Close();
        }
    }
}
