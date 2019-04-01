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
        public MainWindow()
        {
            InitializeComponent();
        }

        //Testing my GameWindow
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWin = new GameWindow(isCheatOn, difficulty);
            gameWin.Show();
        }

        private void BtnCheat_Click(object sender, RoutedEventArgs e)
        {
            isCheatOn = true;
        }

        private void SldrDifficulty_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            difficulty = Convert.ToInt32(sldrDifficulty.Value);
            lblDifficulty.Content = Convert.ToInt32(difficulty);
        }
    }
}
