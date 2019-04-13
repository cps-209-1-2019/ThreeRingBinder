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
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;

namespace Binder.Environment
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Game binderGame;
        Building building;
        DispatcherTimer timer;
        bool isRingShown = false;

        public GameWindow(bool cheat, int difficulty, double startTime)
        {
            //NameScope.SetNameScope(this, new NameScope());
            binderGame = new Game(startTime);
            binderGame.IsCheatOn = cheat;
            binderGame.Difficulty = difficulty;

            InitializeComponent();

            this.KeyDown += new KeyEventHandler(CnvsGame_KeyDown);
            BuildWalls();
            BindItems();
            building = binderGame.CurBuilding;
            //cnvsGame.DataContext = building;
            MakeAI(binderGame);
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += Timer_Tick;
            timer.Start();

            DispatcherTimer timerTwo = new DispatcherTimer();
            timerTwo.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timerTwo.Tick += TimerTwo_Tick;
            timerTwo.Start();

            binderGame.Marcus.PictureName = "/Sprites/MarcusFront.png";
            imgBl.DataContext = binderGame.Marcus.PictureName;

            //SetObjectBinding(binderGame.Marcus.PictureName, binderGame.Marcus);
        }

        private void TimerTwo_Tick(object sender, EventArgs e)
        {
            imgBl.Source = new BitmapImage(new Uri(binderGame.Marcus.PictureName, UriKind.Relative));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((binderGame.isRingFound == true) && (isRingShown == false))
            {
                MessageBox.Show("You Found the Ring!");
                Label label = SetObjectBinding("/Sprites/binderRingSilver.png", binderGame.ring);
                label.Width = 30;
                label.Height = 30;
                isRingShown = true;
            }
            foreach (WorldObject wObj in Game.Environ)
            {
                if (wObj is AI)
                {
                    AI ai = (AI)wObj;
                    ai.Move(binderGame);
                    RemoveLabel(ai);
                    Label label = SetObjectBinding(ai.PictureName, ai);
                    label.Width = 120;
                    label.Height = 120;
                }
                else if (wObj is InventoryItem)
                {
                    InventoryItem item = (InventoryItem)wObj;
                    if (item.Found == true)
                    {
                        RemoveLabel(item);
                    Rectangle rectangle = null;
                    foreach (InventoryItem thing in Game.itemsHeld)
                    {
                        rectangle = GetRectangle(thing);
                        FillRectangle(rectangle, thing);
                    }
                    break;
                    }                       
                }
            }
        }

        public void RemoveLabel(object item)
        {
            foreach (object thing in cnvsGame.Children)
            {
                if (thing is Label)
                {
                    Label label = (Label)thing;
                    if (label.DataContext == item)
                    {
                        cnvsGame.Children.Remove(label);
                        break;
                    }
                }
            }
        }

        private void FillRectangle(Rectangle rectangle, InventoryItem item)
        {
            ImageBrush img = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(item.Image, UriKind.Relative))
                
            };

            if ((Game.itemsHeld.Count() > binderGame.currentItem) && (item == Game.itemsHeld[binderGame.currentItem]))
            {
                rectangle.Stroke = Brushes.Chartreuse;
            }
            else
            {
                rectangle.Stroke = Brushes.DarkBlue;
            }
            rectangle.Fill = img;
            Game.Environ.Remove(item);
        }

        private void CnvsGame_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Up)
            {
                binderGame.Marcus.Move('n', binderGame);
            }
            else if (e.Key == Key.Down)
            {
                binderGame.Marcus.Move('s', binderGame);
            }
            else if (e.Key == Key.Left)
            {
                binderGame.Marcus.Move('w', binderGame);
            }
            else if (e.Key == Key.Right)
            {
                binderGame.Marcus.Move('e', binderGame);
            }
            else if (e.Key == Key.C)
            {
                binderGame.Marcus.Attack(binderGame);
            }
            else if (e.Key == Key.X)
            {
                binderGame.Marcus.Enteract();
            }
            else if (e.Key == Key.Z)
            {
                if (Game.itemsHeld.Count() >= binderGame.currentItem)
                    Game.itemsHeld[binderGame.currentItem].Use(binderGame);
            }
            else if (e.Key == Key.Escape)
            {
                Game.isPaused = true;
                Pause pauseWindow = new Pause(binderGame);
                pauseWindow.Show();
            }
            else if (e.Key == Key.D1)
            {
                ResetRectangles(rectItemOne, 0);
            }
            else if (e.Key == Key.D2)
            {
                ResetRectangles(rectItemTwo, 1);
            }
            else if (e.Key == Key.D3)
            {
                ResetRectangles(rectItemThree, 2);
            }
            else if (e.Key == Key.D4)
            {
                ResetRectangles(rectItemFour, 3);
            }

        }

        public void ResetRectangles(Rectangle firstRectangle, int num)
        {
            int oldCurrentItem = binderGame.currentItem;
            binderGame.currentItem = num;
            FillRectangle(firstRectangle, Game.itemsHeld[num]);
            if (Game.itemsHeld.Count() > oldCurrentItem)
            {
                Rectangle rectangle = GetRectangle(Game.itemsHeld[oldCurrentItem]);
                FillRectangle(rectangle, Game.itemsHeld[oldCurrentItem]);
            }
        }

        public void BindItems()
        {
            foreach (object thing in Game.Environ)
            {
                if (thing is InventoryItem)
                {
                    SetObjectBinding("/Sprites/schaubJacket.png", thing);
                }
            }
        }

        //Builds Walls with Blocks on GUI 
        public void BuildWalls()
        {
            foreach (Walls w in Building.WallsCol)
            {
                foreach (Block b in w.Blocks)
                {
                    SetObjectBinding("/Environment/blocks.png", b);
                }
            }
        }

        public void MakeAI(Game game)
        {
            AI ai = new AI(10, 300000, 20);
            ai.X = 750;
            ai.Y = 400;
            Game.Environ.Add(ai);
            ai.PictureName = "/Sprites/PsiZetaFront.png";
            Label label = SetObjectBinding(ai.PictureName, ai);
            label.Width = 120;
            label.Height = 120;
        }
        public Label SetObjectBinding(string uri, object b)
        {
            Image img = new Image()
            {
                Source = new BitmapImage(new Uri(uri, UriKind.Relative))

            };
            Label block = new Label()
            {
                Content = img
            };

            block.DataContext = b;

            block.SetBinding(Canvas.LeftProperty, "X");
            block.SetBinding(Canvas.TopProperty, "Y");



            cnvsGame.Children.Add(block);
            return block;
        }
        public Rectangle GetRectangle(InventoryItem thing)
        {
            Rectangle rectangle = null;
            if (thing == Game.itemsHeld[0])
                rectangle = rectItemOne;
            else if (Game.itemsHeld.Count() >= 2)
            {
                if (thing == Game.itemsHeld[1])
                    rectangle = rectItemTwo;
                else if (Game.itemsHeld.Count() >= 3)
                {
                    if (thing == Game.itemsHeld[2])
                        rectangle = rectItemThree;
                    else if (Game.itemsHeld.Count() >= 4)
                    {
                        if (thing == Game.itemsHeld[3])
                            rectangle = rectItemFour;
                    }
                }
            }
            return rectangle;
        }
    }
}