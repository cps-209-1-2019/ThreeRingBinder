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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Binder.Environment;

namespace Binder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double startTime = 0;
        bool isCheatOn = false;
        int difficulty = 1;
        bool isLoaded = false;
        public MainWindow()
        {
            InitializeComponent();
            lblDifficulty.Content = "Underclassman";
        }

        //Testing my GameWindow
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWin = new GameWindow(isCheatOn, difficulty, startTime);
            gameWin.Show();
            this.Close();
        }

        private void BtnCheat_Click(object sender, RoutedEventArgs e)
        {
            if (isCheatOn)
                isCheatOn = false;
            else
                isCheatOn = true;
        }

        private void SldrDifficulty_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isLoaded) {
                difficulty = Convert.ToInt32(sldrDifficulty.Value);
                if (difficulty == 1)
                    {
                        startTime = 360;
                        lblDifficulty.Content = "Underclassman";
                    }
                else if (difficulty == 2)
                {
                    startTime = 280;
                    lblDifficulty.Content = "Upperclassman";
                }
                else if (difficulty == 3)
                {
                    startTime = 150;
                    lblDifficulty.Content = "Grad Student";
                }
            }
            isLoaded = true;
        }

        private void Instruction_Click(object sender, RoutedEventArgs e)
        {
            Instructions instructions = new Instructions();
            instructions.Show();
        }

        private void BtnHighScores_Click(object sender, RoutedEventArgs e)
        {
            HighScoreList highScoreList = new HighScoreList();
            highScoreList.Show();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWin = new GameWindow(isCheatOn, difficulty, startTime);
            try
            {
                gameWin.binderGame.Load("gameFile.txt");
            }
            catch
            {
                
            }
            gameWin.Show();
            this.Close();
        }
    }
}
