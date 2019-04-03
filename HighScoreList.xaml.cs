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
    /// Interaction logic for HighScores.xaml
    /// </summary>
    public partial class HighScoreList : Window
    {
        public HighScoreList()
        {
            InitializeComponent();
        }

        //Calls HighScore.Load() and writes the high scores to the window.
        public void WriteScores()
        {
            HighScoreHolder holder = new HighScoreHolder();
            holder.Load();
            txtScores.Text = holder.highScoreText;
        }
    }
}
