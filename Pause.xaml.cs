using Binder.Environment;
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
        GameWindow window;
        public Pause(Game game, GameWindow gameWindow)
        {
            binderGame = game;
            InitializeComponent();
            SetRiddle();
            window = gameWindow;
        }

        public void SetRiddle()
        {
            if (binderGame.LevelNum == 1)
            {
                txtRiddle1.Text = "Soft and squishy, yellow and fat,";
                txtRiddle2.Text = "its sturdy frame never stays flat.";
                txtRiddle3.Text = "Metal bin, so hard and cold,";
                txtRiddle4.Text = "Spouting forth the taste of old.";
                if (binderGame.IsCheatOn == true)
                {
                    lblAnswer.Content = "The answer, my dear cheater, is the water fountain with the duck.";
                }
            }

            else if (binderGame.LevelNum == 2)
            {
                txtRiddle1.Text = "Spinning needle, round and round,";
                txtRiddle2.Text = "When it stops, your way is found.";
                txtRiddle3.Text = "Round the metal, up to the skies";
                txtRiddle4.Text = "A colored fabric on breezes flies";
                if (binderGame.IsCheatOn == true)
                {
                    lblAnswer.Content = "The answer, my dear cheater, is the flag with the compass.";
                }
            }

            else if (binderGame.LevelNum == 3)
            {
                txtRiddle1.Text = "Gold and silver, ringing true;";
                txtRiddle2.Text = "But only when the soft wind blew";
                txtRiddle3.Text = "Cork and color, needle and note";
                txtRiddle4.Text = "Find an event to float your boat.";
                if (binderGame.IsCheatOn == true)
                {
                    lblAnswer.Content = "The answer, my dear cheater, is the trumpet with the bulletin board.";
                }
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
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            window.Close();
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
            binderGame.isPauseScreenShown = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Game.isPaused = false;
            this.Close();
            binderGame.isPauseScreenShown = false;
        }
    }
}
