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
            WriteScores();
        }

        //Calls HighScore.Load() and writes the high scores to the window.
        public void WriteScores()
        {         
            HighScoreHolder holder = new HighScoreHolder();
            holder.Load();
            HighScore score = new HighScore("300", "Jim");
            holder.AddHighScore(score);
            HighScore scoreTwo = new HighScore("200", "Bob");
            holder.AddHighScore(scoreTwo);
            holder.Save();
            holder.Load();
            txtScores.Text = holder.highScoreText;
        }
    }
}
