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
using System.IO;

namespace Binder.Environment
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Image imgMarcus;
        bool isGameOver = false;
        public Game binderGame;
        bool isCheatOn = false;
        bool isPressed = false;

        Building building;
        DispatcherTimer timer;
        DispatcherTimer timerUp;
        DispatcherTimer timerDown;
        DispatcherTimer timerLeft;
        DispatcherTimer timerRight;
        DispatcherTimer LimitTimer;
        //DispatcherTimer MusicTimer;
        bool isRingShown = false;

        TextBlock Time;
        TextBlock Level;
        TextBlock Score;
        TextBlock ScoreLbl;

        Rectangle rectLifeOne;
        Rectangle rectLifeTwo;
        Rectangle rectLifeThree;
        Rectangle rectItemOne;
        Rectangle rectItemTwo;
        Rectangle rectItemThree;
        Rectangle rectItemFour;

        MediaPlayer BMusic;

        public GameWindow(bool cheat, int difficulty, double startTime, bool doLoad)
        {
            InitializeComponent();

            isCheatOn = cheat;
            Game.Difficulty = difficulty;
            binderGame = new Game(startTime, 1);
            cnvsGame.DataContext = binderGame;
            binderGame.IsCheatOn = cheat;
            LoadGame();

            Play();
        }

        public GameWindow()
        {
            InitializeComponent();
            
            binderGame = new Game();
            binderGame.Load("gameFile.txt");
            LoadGame();
        }

        //Checks if the time is up
        private void LimitTimer_Tick(object sender, EventArgs e)
        {            
            binderGame.DecrTime();
            if (binderGame.Time == 0)
            {
                LimitTimer.Stop();
                binderGame.Play("timeUp.wav");
                int score = binderGame.CalculateScores(true);
                GameOver endGame = new GameOver(this, false, score);
                endGame.Show();
                timer.Stop();
            }
        }

        //Moves marcus up
        private void UpTimer_Tick(object sender, EventArgs e)
        {
            binderGame.Marcus.Move('n', binderGame);
        }

        //Moves marcus down
        private void DownTimer_Tick(object sender, EventArgs e)
        {
            binderGame.Marcus.Move('s', binderGame);
        }

        //Moves marcus left
        private void LeftTimer_Tick(object sender, EventArgs e)
        {
            binderGame.Marcus.Move('w', binderGame);
        }

        //Moves marcus right
        private void RightTimer_Tick(object sender, EventArgs e)
        {
            binderGame.Marcus.Move('e', binderGame);
        }

        //Loads a new game screen
        private void LoadGame()
        {
            //NameScope.SetNameScope(this, new NameScope());

            building = binderGame.CurBuilding;

            MakeLevelFloors(binderGame.LevelNum);

            MakeMarcus();
            MakeAI();
            this.KeyDown += new KeyEventHandler(CnvsGame_KeyDown);
            this.KeyUp += new KeyEventHandler(CnvsGame_KeyUp);
            BuildWalls();
            BindItems();
            
            cnvsGame.DataContext = building;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();

            //The timers that allow the player to move smoothly
            timerUp = new DispatcherTimer();
            timerUp.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timerUp.Tick += UpTimer_Tick;
            timerDown = new DispatcherTimer();
            timerDown.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timerDown.Tick += DownTimer_Tick;
            timerLeft = new DispatcherTimer();
            timerLeft.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timerLeft.Tick += LeftTimer_Tick;
            timerRight = new DispatcherTimer();
            timerRight.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timerRight.Tick += RightTimer_Tick;

            DispatcherTimer timerTwo = new DispatcherTimer();
            timerTwo.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timerTwo.Tick += TimerTwo_Tick;
            timerTwo.Start();

            binderGame.Marcus.PictureName = "/Sprites/MarcusFront.png";
            imgMarcus.DataContext = binderGame.Marcus.PictureName;

            DisplayGameData();

            LimitTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 1)
            };

            LimitTimer.Tick += LimitTimer_Tick;
            LimitTimer.Start();

            //MusicTimer = new DispatcherTimer()
            //{
            //    Interval = new TimeSpan(0, 0, 0, 1),
            //};
            //MusicTimer.Tick += MusicTimer_Tick;
            //MusicTimer.Start();

            LoadRectangles();

            string dir = Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "");
            FillLivesRectangle(rectLifeOne, dir + "/Sprites/composureTie.png");
            FillLivesRectangle(rectLifeTwo, dir + "/Sprites/composureTie.png");
            FillLivesRectangle(rectLifeThree, dir + "/Sprites/composureTie.png");

            int whichRect = 1;
            foreach (InventoryItem i in binderGame.Marcus.Inventory)
            {
                switch (whichRect)
                {
                    case 1:
                        FillInventoryRectangle(rectItemOne, i);
                        break;
                    case 2:
                        FillInventoryRectangle(rectItemTwo, i);
                        break;
                    case 3:
                        FillInventoryRectangle(rectItemThree, i);
                        break;
                    case 4:
                        FillInventoryRectangle(rectItemFour, i);
                        break;                       
                }
                whichRect++;
            }            
        }

        //private void MusicTimer_Tick(object sender, EventArgs e)
        //{
        //    if(isGameOver == false)
        //    {
        //        binderGame.Play("GamePlay.wav");
        //    }
        //}

        //Sets new image for Marcus
        private void TimerTwo_Tick(object sender, EventArgs e)
        {
            imgMarcus.Source = new BitmapImage(new Uri(binderGame.Marcus.PictureName, UriKind.Relative));
        }

        //Checks for updates in model
        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((binderGame.isRingFound == true) && (isRingShown == false))
            {
                StartNewLevel();
            }
            foreach (WorldObject wObj in Game.Environ)
            {
                if (wObj is AI)
                {
                    bool shouldBreak = MoveAI(wObj);
                    if (shouldBreak)
                        break;
                }
                else if (wObj is InventoryItem)
                {
                    InventoryItem item = (InventoryItem)wObj;
                    if (item.Found == true)
                    {
                        binderGame.Play("inventory.wav");
                        RemoveLabel(item);
                        Rectangle rectangle = null;
                        foreach (InventoryItem thing in binderGame.Marcus.Inventory)
                        {
                            rectangle = GetRectangle(thing);
                            FillInventoryRectangle(rectangle, thing);
                        }
                        break;
                    }                       
                }
                else if (wObj is Airplane)
                {
                    Airplane plane = (Airplane)wObj;
                    plane.Update();
                    
                    if (plane.Destroy == true)
                    {
                        RemoveLabel(plane);
                    }
                }
                CheckHealth();
            }
        }

        //Reloads the health label
        private void CheckHealth()
        {
            if (binderGame.Marcus.Health < 3)
            {
                rectLifeThree.Fill = null;
                if (binderGame.Marcus.Health < 2)
                {
                    rectLifeTwo.Fill = null;
                    if (binderGame.Marcus.Health < 1)
                    {
                        rectLifeOne.Fill = null;
                    }
                    if ((binderGame.Marcus.Health == 0) && (isGameOver == false))
                    {
                        int score = binderGame.CalculateScores(true);
                        GameOver endGame = new GameOver(this, false, score);
                        endGame.Show();
                        StopTimers();

                        binderGame.Play("defeated.wav");
                        isGameOver = true;
                    }
                }
            }
        }

        //Stops all timers
        private void StopTimers()
        {
            //MusicTimer.Stop();
            timer.Stop();
            timerDown.Stop();
            timerUp.Stop();
            timerLeft.Stop();
            timerRight.Stop();
            LimitTimer.Stop();
        }

        //Reloads a new level
        private void StartNewLevel()
        {
            int level = binderGame.LevelNum + 1;
            if ((level == 4) && (isGameOver == false))
            {
                int points = binderGame.CalculateScores(true);
                binderGame.Play("GameWon.wav");
                GameOver gameOver = new GameOver(this, true, points);
                gameOver.Show();
                isGameOver = true;
            }
            else if (isGameOver == false)
            {
                int saveScore = binderGame.CurrScore;
                MessageBox.Show("You Found the Ring!");
                binderGame.Play("trumpets.wav");
                Label label = SetObjectBinding("/Sprites/binderRingSilver.png", binderGame.ring);
                label.Width = 30;
                label.Height = 30;
                isRingShown = true;
                cnvsGame.Children.Clear();
                StopTimers();
                binderGame = new Game(180, level);
                binderGame.CurrScore = saveScore;
                binderGame.IsCheatOn = isCheatOn;
                MakeLevelFloors(level);
                LoadGame();
                isRingShown = false;
            }
        }

        //Moves the AI corresponding to `wObj`
        private bool MoveAI(object wObj)
        {

            AI ai = (AI)wObj;
            if (ai.Health <= 0)
            {
                RemoveLabel(ai);
                binderGame.Play("No.wav");
                Game.Environ.Remove(ai);
                binderGame.PsiZetaShamed++;
                binderGame.CalculateScores(false);
                return true;
            }
            ai.Move(binderGame);
            RemoveLabel(ai);
            Label label = SetObjectBinding(ai.PictureName, ai);
            if (ai.PictureName.Contains("Whip"))
            {
                binderGame.Play("whiplash.wav");
                label.Width = 180;
                label.Height = 180;
            }
            else
            {
                label.Width = 120;
                label.Height = 120;
            }
            return false;
        }

        //Puts game information on the screen
        void DisplayGameData()
        {
            double mid = this.Width / 2;

            List<TextBlock> gameData = new List<TextBlock>();
            Time = new TextBlock()
            {
                FontSize = 50,
                Foreground = Brushes.Yellow,
                FontFamily = new FontFamily("Algerian"),
            };
            
            cnvsGame.Children.Add(Time);
            Time.DataContext = binderGame;
            Time.SetBinding(TextBlock.TextProperty, "TimeLeft");
            Canvas.SetLeft(Time, 30);
            Canvas.SetTop(Time, 15);


            //Implement Level Progression, and use abstraction for how the text on the screen will look.
            Level = new TextBlock()
            {
                MinWidth = 200
            };
           
            cnvsGame.Children.Add(Level);
            Level.SetBinding(TextBlock.TextProperty, "CurrLevel");
            Canvas.SetLeft(Level, mid - Level.MinWidth);
            Canvas.SetTop(Level, 10);

            //Set Scores in the middle
            Score = new TextBlock();           
            cnvsGame.Children.Add(Score);
            Score.SetBinding(TextBlock.TextProperty, "CurrScore");
            Canvas.SetLeft(Score, mid);
            Canvas.SetTop(Score, 55);

            ScoreLbl = new TextBlock()
            {
                Text = "Score: "
            };
            cnvsGame.Children.Add(ScoreLbl);
            Canvas.SetLeft(ScoreLbl, mid - 150);
            Canvas.SetTop(ScoreLbl, 55);

            gameData.AddRange(new TextBlock[] { Score, Level, ScoreLbl });
            
            foreach(TextBlock t in gameData)
            {
                t.FontSize = 30;
                t.Foreground = Brushes.Yellow;
                t.FontFamily = new FontFamily("Algerian");
                t.DataContext = binderGame;
            }
        }

        //Puts new rectangles on the canvas
        private void LoadRectangles()
        {
            Rectangle[] rectList = new Rectangle[4] { rectItemOne, rectItemTwo, rectItemThree, rectItemFour };
            double rectWid = 60;
            double rectHeight = 2 * rectWid;
            double rectTop = 20;
            double rectSpace = rectWid / 3;
            double rectMargin = 30;

            rectLifeOne = new Rectangle() { Width = rectWid, Height = rectHeight };
            rectLifeTwo = new Rectangle() { Width = rectWid, Height = rectHeight };
            rectLifeThree = new Rectangle() { Width = rectWid, Height = rectHeight };

            Canvas.SetRight(rectLifeOne, rectMargin);
            Canvas.SetTop(rectLifeOne, rectTop + 10);
            cnvsGame.Children.Add(rectLifeOne);
            Canvas.SetRight(rectLifeTwo, rectMargin + rectWid + rectSpace);
            Canvas.SetTop(rectLifeTwo, rectTop + 10);
            cnvsGame.Children.Add(rectLifeTwo);
            Canvas.SetRight(rectLifeThree, rectMargin + 2 * rectWid + 2 * rectSpace);
            Canvas.SetTop(rectLifeThree, rectTop + 10);
            cnvsGame.Children.Add(rectLifeThree);

            rectWid = 90;
            rectHeight = 60;

            for (int i = 0; i < 4; i++)
            {
                rectList[i] = new Rectangle() { Width = rectWid, Height = rectHeight };
                cnvsGame.Children.Add(rectList[i]);
                Canvas.SetBottom(rectList[i], rectTop);
                Canvas.SetLeft(rectList[i], rectMargin);
                rectMargin += 100;
                
            }
            rectItemOne = rectList[0];
            rectItemTwo = rectList[1];
            rectItemThree = rectList[2];
            rectItemFour = rectList[3];
        }

        //Sets image binding to Marcus
        private void MakeMarcus()
        {
            imgMarcus = new Image();
            imgMarcus.Source = new BitmapImage(new Uri("../Sprites/MarcusFront.png", UriKind.Relative));
            imgMarcus.Height = 138;
            imgMarcus.Width = 134;
            cnvsGame.Children.Add(imgMarcus);
            Canvas.SetTop(imgMarcus, 448);
            Canvas.SetLeft(imgMarcus, 654);
        }

        //Removes the label corresponding to `item`
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

        //Fills `rectangle` with `image`
        private void FillLivesRectangle(Rectangle rectangle, string image)
        {
            ImageBrush img = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(image, UriKind.Relative))
            };
            rectangle.Fill = img;
        }

        //Fills `rectangle` with the image of `item`
        private void FillInventoryRectangle(Rectangle rectangle, InventoryItem item)
        {
            ImageBrush img = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(Directory.GetCurrentDirectory().Replace("\\bin\\Debug","") + item.Image, UriKind.Relative))                
            };

            if ((binderGame.Marcus.Inventory.Count() > binderGame.currentItem) && (item == binderGame.Marcus.Inventory[binderGame.currentItem]))
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

        //Stops the timer when a KeyUp event is fired
        private void CnvsGame_KeyUp(object sender, KeyEventArgs e)
        {
            isPressed = false;
            if (e.Key == Key.Up)
                timerUp.Stop();
            if (e.Key == Key.Down)
                timerDown.Stop();
            if (e.Key == Key.Left)
                timerLeft.Stop();
            if (e.Key == Key.Right)
                timerRight.Stop();
        }

        //Determines action based on the button pushed
        private void CnvsGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (isPressed == false)
            {
                if (e.Key == Key.Up)
                {
                    timerUp.Start();
                }
                else if (e.Key == Key.Down)
                {
                    timerDown.Start();
                }
                else if (e.Key == Key.Left)
                {
                    timerLeft.Start();
                }
                else if (e.Key == Key.Right)
                {
                    timerRight.Start();
                }
                else if (e.Key == Key.C)
                {
                    binderGame.Play("paperHit.wav");
                    Airplane airplane = new Airplane(binderGame.Marcus);
                    Game.Environ.Add(airplane);
                    try
                    {
                        Label label = SetObjectBinding(airplane.PictureName, airplane);
                        label.Width = 30;
                        label.Height = 30;
                    }
                    catch
                    {
                        isPressed = true;
                    }
                }
                else if (e.Key == Key.X)
                {
                    binderGame.Marcus.Enteract(binderGame);
                    //binderGame.Play("inventory.wav");
                }
                else if (e.Key == Key.Z)
                {
                    if (binderGame.Marcus.Inventory.Count() > binderGame.currentItem)
                        binderGame.Marcus.Inventory[binderGame.currentItem].Use(binderGame);
                }
                else if (e.Key == Key.Escape)
                {
                    if (binderGame.isPauseScreenShown == false)
                    {
                        Game.isPaused = true;
                        LimitTimer.Stop();
                        Pause pauseWindow = new Pause(binderGame, this);
                        pauseWindow.Show();
                        binderGame.isPauseScreenShown = true;
                    }
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
                isPressed = true;
            }
    
        }

        //Resets the filling of the `num`th rectangle `firstRectangle`
        public void ResetRectangles(Rectangle firstRectangle, int num)
        {
            int oldCurrentItem = binderGame.currentItem;
            binderGame.currentItem = num;
            FillInventoryRectangle(firstRectangle, binderGame.Marcus.Inventory[num]);
            if (binderGame.Marcus.Inventory.Count() > oldCurrentItem)
            {
                Rectangle rectangle = GetRectangle(binderGame.Marcus.Inventory[oldCurrentItem]);
                FillInventoryRectangle(rectangle, binderGame.Marcus.Inventory[oldCurrentItem]);
            }
        }

        //Binds a label to an InventoryItem
        public void BindItems()
        {
            foreach (object thing in Game.Environ)
            {
                if (thing is InventoryItem)
                {
                    InventoryItem item = (InventoryItem)thing;
                    try
                    {
                        SetObjectBinding(item.Image, thing);
                    }
                    catch
                    {
                        Debug.WriteLine("Could not load item");
                    }
                }
            }
        }

        //Builds Walls with Blocks on GUI 
        public void BuildWalls()
        {
            foreach (WorldObject w in Game.Environ)
            {
                if (w is Walls)
                {
                    foreach (Block b in (w as Walls).Blocks)
                    {
                        SetObjectBinding("/Environment/blocks.png", b);
                    }
                }
            }
        }

        //Sets label binding for each AI in the model
        public void MakeAI()
        {
            foreach (WorldObject wObj in Game.Environ)
            {
                if (wObj is AI)
                {
                    AI ai = (AI)wObj;
                    Label label = SetObjectBinding(ai.PictureName, ai);
                    label.Width = 120;
                    label.Height = 120;
                }
            }
        }

        //Sets label binding to a model object `b` with image `uri`; returns the label
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

        //Finds and returns the rectangle that corresponds to the position of `thing`
        public Rectangle GetRectangle(InventoryItem thing)
        {
            Rectangle rectangle = null;
            if (thing == binderGame.Marcus.Inventory[0])
                rectangle = rectItemOne;
            else if (binderGame.Marcus.Inventory.Count() >= 2)
            {
                if (thing == binderGame.Marcus.Inventory[1])
                    rectangle = rectItemTwo;
                else if (binderGame.Marcus.Inventory.Count() >= 3)
                {
                    if (thing == binderGame.Marcus.Inventory[2])
                        rectangle = rectItemThree;
                    else if (binderGame.Marcus.Inventory.Count() >= 4)
                    {
                        if (thing == binderGame.Marcus.Inventory[3])
                        {
                            rectangle = rectItemFour;                           
                        }
                        else if (thing == binderGame.Marcus.Inventory[4])
                        {

                        }
                    }
                }
            }
            return rectangle;
        }

        //Sets binding for the floor depending on the level
        private void MakeLevelFloors(int level)
        {
            string[] stringList = new string[3] { "/Environment/floor5.png", "/Environment/floor5.png", "/Environment/floor5.png" };
            
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(stringList[level - 1], UriKind.Relative));
            image.Height = 1250;
            image.Width = 1966;
            cnvsGame.Children.Add(image);
            Canvas.SetTop(image, -19);
            Canvas.SetLeft(image, -18);
        }

        private void Play()
        {
            BMusic = new MediaPlayer();
            BMusic.Open(new System.Uri(@"pack://application:,,,/Resources/GamePlay.wav"));
            BMusic.Play();
        }
    }
}