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
        bool isCheatOn = false;
        int difficulty = 1;
        bool isLoaded = false;
        public MainWindow()
        {
            InitializeComponent();
            lblDifficulty.Content = "1";
        }

        //Testing my GameWindow
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWin = new GameWindow(isCheatOn, difficulty);
            gameWin.Show();
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
            lblDifficulty.Content = Convert.ToString(difficulty);
            }
            isLoaded = true;
        }

        private void Instruction_Click(object sender, RoutedEventArgs e)
        {
            Instructions instructions = new Instructions();
            instructions.Show();
        }
    }
}
