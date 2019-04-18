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
        TextBlock[] textBlocksNames;
        TextBlock[] textBlocksScores;
        public HighScoreList()
        {

            InitializeComponent();
            textBlocksNames = new TextBlock[10] { txtNameOne, txtNameTwo, txtNameThree, txtNameFour, txtNameFive, txtNameSix, txtNameSeven, txtNameEight, txtNameNine, txtNameTen };
            textBlocksScores = new TextBlock[10] { txtScoreOne, txtScoreTwo, txtScoreThree, txtScoreFour, txtScoreFive, txtScoreSix, txtScoreSeven, txtScoreEight, txtScoreNine, txtScoreTen };
            WriteScores();

        }

        //Calls HighScore.Load() and writes the high scores to the window.
        public void WriteScores()
        {         
            HighScoreHolder holder = new HighScoreHolder();
            holder.Load();
            //holder.AddHighScore(new HighScore("3", "r"));
            for (int i = 0; i < holder.scoreList.Count(); i++)
            {
                string colon = ":     ";
                if (i == 9)
                {
                    colon = ":   ";
                }
                textBlocksNames[i].Text = ( i + 1 )+ colon + holder.scoreList[i].PlayerName;
                textBlocksScores[i].Text = holder.scoreList[i].CurrentScore;
            }
        }
    }
}
