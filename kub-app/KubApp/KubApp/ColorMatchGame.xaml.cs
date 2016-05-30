using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
using WinRTXamlToolkit;
using WinRTXamlToolkit.AwaitableUI;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorMatchGame : Page
    {
        // DispatchTimer Variables
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DateTimeOffset startTime;
        private DateTimeOffset lastTime;
        // Random color ( blue, red, green, yellow ) Variables
        private List<Color> colorList = new List<Color>();
        private Color colorNow;
        private Color colorPressed;
        private Color blue = Color.FromArgb(255, 0, 0, 255);
        private Color red = Color.FromArgb(255, 255, 0, 0);
        private Color yellow = Color.FromArgb(255, 255, 255, 41);
        private Color green = Color.FromArgb(255, 41, 255, 41);
        // Track Score Variables
        private int currentScore = 0;
        public int highScore = 0;
        // Timevar for countdown
        private int timevar = 3;

        public SolidColorBrush fillColor { get; set; }

        public ColorMatchGame()
        {
            this.InitializeComponent();
        }
      
        private  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Executed when page is loaded
            randomcolor();
            DispatcherTimerSetup();
        }

        public void DispatcherTimerSetup()
        {
            // initializes dispatcherTimer
            this.dispatcherTimer.Tick += DispatcherTimer_Tick;
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this.dispatcherTimer.Start();
        }


        private void DispatcherTimer_Tick(object sender, object e)
        {
            // Executed when timer runs out
            if (this.timevar == 3)
            {
                timevar--;
                bar.Value = 100;
            }
            else if (this.timevar == 2)
            {
                timevar--;
                bar.Value = 66;
            }
            else if (this.timevar == 1)
            {
                timevar--;
                bar.Value = 33;
            }
            else
            {
                bar.Value = 0;
                dispatcherTimer.Stop();
                string hs = this.highScore.ToString();
                this.Frame.Navigate(typeof(ColorMatchSlow), hs);
            }
            
        }

        private void randomcolor()
        {
            // put colors in list
            colorList.Add(blue);
            colorList.Add(red);
            colorList.Add(yellow);
            colorList.Add(green);
            // pick random color from list
            Random rnd = new Random();
            int r = rnd.Next(colorList.Count);
            Color rndColor = colorList[r];
            // fill rect with random color from list
            currentColor.Fill = new SolidColorBrush(rndColor);
            this.colorNow = rndColor;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var high = e.Parameter as string;
            if (high == null)
            {
                this.highScore = 0;
            }
            else
            {
                this.highScore = Int32.Parse(high);
                highscoreTextBlock.Text = highScore.ToString();
            }
        }

        // Code below is for buttons, if clicked correctly, add 1 point to score
        // and reset countdown bar to 100%
        // If clicked incorrectly, display message with highscore


        private void buttonblue_Click(object sender, RoutedEventArgs e)
        {
            colorPressed = colorList[0];
            if (this.colorNow == colorPressed)
            {
                currentScore = currentScore + 1;
                highScore = currentScore;
                scoreTextBlock.Text = currentScore.ToString();
                bar.Value = 100;
                timevar = 3;
                dispatcherTimer.Start();
            }
            else
            {
                highScore = currentScore;
                currentScore = 0;
                scoreTextBlock.Text = currentScore.ToString();
                string hs = highScore.ToString();
                this.Frame.Navigate(typeof(ColorMatchWrong), hs);
                dispatcherTimer.Stop();
            }
            highscoreTextBlock.Text = highScore.ToString();
            randomcolor();
        }

        private void buttonred_Click(object sender, RoutedEventArgs e)
        {
            colorPressed = colorList[1];
            if (this.colorNow == colorPressed)
            {
                currentScore = currentScore + 1;
                highScore = currentScore;
                scoreTextBlock.Text = currentScore.ToString();
                bar.Value = 100;
                timevar = 3;
                dispatcherTimer.Start();
            }
            else
            {
                highScore = currentScore;
                currentScore = 0;
                scoreTextBlock.Text = currentScore.ToString();
                string hs = highScore.ToString();
                this.Frame.Navigate(typeof(ColorMatchWrong), hs);
                dispatcherTimer.Stop();
            }
            highscoreTextBlock.Text = highScore.ToString();
            randomcolor();
        }

        private void buttongreen_Click(object sender, RoutedEventArgs e)
        {
            colorPressed = colorList[3];
            if (this.colorNow == colorPressed)
            {
                currentScore = currentScore + 1;
                highScore = currentScore;
                bar.Value = 100;
                timevar = 3;
                scoreTextBlock.Text = currentScore.ToString();
                dispatcherTimer.Start();
            }
            else
            {
                highScore = currentScore;
                currentScore = 0;
                scoreTextBlock.Text = currentScore.ToString();
                string hs = highScore.ToString();
                this.Frame.Navigate(typeof(ColorMatchWrong), hs);
                dispatcherTimer.Stop();
            }
            highscoreTextBlock.Text = highScore.ToString();
            randomcolor();
        }

        private void buttonyellow_Click(object sender, RoutedEventArgs e)
        {
            colorPressed = colorList[2];
            if (this.colorNow == colorPressed)
            {
                currentScore = currentScore + 1;
                highScore = currentScore;
                scoreTextBlock.Text = currentScore.ToString();
                timevar = 3;
                bar.Value = 100;
                dispatcherTimer.Start();
            }
            else
            {
                highScore = currentScore;
                currentScore = 0;
                scoreTextBlock.Text = currentScore.ToString();
                string hs = highScore.ToString();
                this.Frame.Navigate(typeof(ColorMatchWrong), hs);
                dispatcherTimer.Stop();
            }
            highscoreTextBlock.Text = highScore.ToString();
            randomcolor();
        }

    }
}
