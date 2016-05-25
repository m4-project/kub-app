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
using System.Threading;
using Windows.UI;
using Windows.UI.Popups;

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
        private int highScore = 0;

        public SolidColorBrush fillColor { get; set; }

        public ColorMatchGame()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Executed on opening the page, wait for user input ( close button ) to start game
            var dialog = new MessageDialog("Press the corect color! \r\n\r Ready? Press oke!");
            await dialog.ShowAsync();
            randomcolor();
            DispatcherTimerSetup();
        }

        public void DispatcherTimerSetup()
        {
            // initializes dispatcherTimer
            this.dispatcherTimer.Tick += DispatcherTimer_Tick;
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            this.dispatcherTimer.Start();
        }

        private async void DispatcherTimer_Tick(object sender, object e)
        {
            // Executed when timer runs out
            var dialog = new MessageDialog("To slow bitch! \r\n\r HighScore: " + highScore);
            await dialog.ShowAsync();
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

        // Code below is for buttons, if clicked correctly, add 1 point to score
        // If clicked incorrectly, display message with highscore


        private async void buttonblue_Click(object sender, RoutedEventArgs e)
        {
            colorPressed = colorList[0];
            if (this.colorNow == colorPressed)
            {
                currentScore = currentScore + 1;
                highScore = currentScore;
                scoreTextBlock.Text = currentScore.ToString();
            }
            else
            {
                highScore = currentScore;
                currentScore = 0;
                scoreTextBlock.Text = currentScore.ToString();
                var dialog = new MessageDialog("Wrong!\r\n\rHighScore: " + highScore);
                await dialog.ShowAsync();
            }
            highscoreTextBlock.Text = highScore.ToString();
            dispatcherTimer.Start();
            randomcolor();
        }

        private async void buttonred_Click(object sender, RoutedEventArgs e)
        {
            colorPressed = colorList[1];
            if (this.colorNow == colorPressed)
            {
                currentScore = currentScore + 1;
                highScore = currentScore;
                scoreTextBlock.Text = currentScore.ToString();
            }
            else
            {
                highScore = currentScore;
                currentScore = 0;
                scoreTextBlock.Text = currentScore.ToString();
                var dialog = new MessageDialog("Wrong!\r\n\rHighScore: " + highScore);
                await dialog.ShowAsync();
            }
            highscoreTextBlock.Text = highScore.ToString();
            dispatcherTimer.Start();
            randomcolor();
        }

        private async void buttongreen_Click(object sender, RoutedEventArgs e)
        {
            colorPressed = colorList[3];
            if (this.colorNow == colorPressed)
            {
                currentScore = currentScore + 1;
                highScore = currentScore;
                scoreTextBlock.Text = currentScore.ToString();
            }
            else
            {
                highScore = currentScore;
                currentScore = 0;
                scoreTextBlock.Text = currentScore.ToString();
                var dialog = new MessageDialog("Wrong!\r\n\rHighScore: " + highScore);
                await dialog.ShowAsync();
            }
            highscoreTextBlock.Text = highScore.ToString();
            dispatcherTimer.Start();
            randomcolor();
        }

        private async void buttonyellow_Click(object sender, RoutedEventArgs e)
        {
            colorPressed = colorList[2];
            if (this.colorNow == colorPressed)
            {
                currentScore = currentScore + 1;
                highScore = currentScore;
                scoreTextBlock.Text = currentScore.ToString();
            }
            else
            {
                highScore = currentScore;
                currentScore = 0;
                scoreTextBlock.Text = currentScore.ToString();
                var dialog = new MessageDialog("Wrong!\r\n\rHighScore: " + highScore);
                await dialog.ShowAsync();
            }
            highscoreTextBlock.Text = highScore.ToString();
            dispatcherTimer.Start();
            randomcolor();
        }

    }
}
