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
    /// Interaction logic for NewHighScore.xaml
    /// </summary>
    public partial class NewHighScore : Window
    {
        string newHighScore;
        public NewHighScore(int score)
        {
            InitializeComponent();
            newHighScore = Convert.ToString(score);
            lblScore.Content = Convert.ToString(newHighScore);
        }

        //Submits player name and instantiates HighScore
        private void BtnSubmitName_Click(object sender, RoutedEventArgs e)
        {
            string[] nameArray = new string[10];
            nameArray = txtPlayerName.Text.Split(new char[] { ' ' } );
            HighScore newScore = new HighScore(newHighScore, nameArray[0]);
            HighScoreHolder newHolder = new HighScoreHolder();
            newHolder.AddHighScore(newScore);
            newHolder.Save();
        }
    }
}
