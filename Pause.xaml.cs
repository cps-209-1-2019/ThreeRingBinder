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

namespace Binder
{
    /// <summary>
    /// Interaction logic for Pause.xaml
    /// </summary>
    public partial class Pause : Window
    {
        Game binderGame;
        public Pause(Game game)
        {
            binderGame = game;
            InitializeComponent();
            SetRiddle(game);

        }

        public void SetRiddle(Game game)
        {
            if (game.LevelNum == 1)
            txtRiddle1.Text = "Soft and squishy, yellow and fat,";
            txtRiddle2.Text = "its sturdy frame never stays flat.";
            txtRiddle3.Text = "Metal bin, so hard and cold,";
            txtRiddle4.Text = "Spouting forth the taste of old.";
            if (game.IsCheatOn == true)
            {
                lblAnswer.Content = "The answer, my dear cheater, is the water fountain";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            lblSaved.Content = "Saved!";
            binderGame.Save("gameFile.txt");
        }

        private void BtnSaveQuit_Click(object sender, RoutedEventArgs e)
        {
            binderGame.Save("gameFile.txt");
            Application.Current.Shutdown();
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            Help helpWindow = new Help();
            helpWindow.Show();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.Show();
        }

        private void BtnResume_Click(object sender, RoutedEventArgs e)
        {
            Game.isPaused = false;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Game.isPaused = false;
            this.Close();
        }
    }
}
