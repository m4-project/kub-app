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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MindGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Medium : Page
    {
        private int time = 4;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Random rnd = new Random();
        int randomNummer;
        int randomNummer1;
        int score = 0;
        int lives = 3;
        private int highscore = 0;

        public Medium()
        {
            this.InitializeComponent();
        }


        private void DispatcherTimer_Tick(object sender, object e)
        {
            if (time > 0)
            {
                time--;
                textBlock1.Text = string.Format("{1}", time / 60, time % 60);
            }
            else
            {
                //dispatcherTimer.Stop();
                textBlock1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                //Generate new colours
                randomNummer = rnd.Next(5); //random number 0 to 4

                if (randomNummer == 0) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                else if (randomNummer == 1) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 0));
                else if (randomNummer == 2) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 255));
                else if (randomNummer == 3) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 0));
                else if (randomNummer == 4) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));

                //Generate new text
                randomNummer1 = rnd.Next(5); //random number 0 to 4

                if (randomNummer1 == 0) randomColor.Content = "Red";
                else if (randomNummer1 == 1) randomColor.Content = "Blue";
                else if (randomNummer1 == 2) randomColor.Content = "Green";
                else if (randomNummer1 == 3) randomColor.Content = "Yellow";
                else if (randomNummer1 == 4) randomColor.Content = "Black";
            }
        }

        private void Startbtn_Click(object sender, RoutedEventArgs e)
        {
            button.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button2.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button3.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button4.Visibility = Windows.UI.Xaml.Visibility.Visible;

            Startbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Stopbtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            timerStart();
        }

        private void timerStart()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void Stopbtn_Click(object sender, RoutedEventArgs e)
        {
            button.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button3.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            Stopbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Startbtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            dispatcherTimer.Stop();
            this.Frame.Navigate(typeof(EndPage), highscore.ToString());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 255, 0, 0")
            {
                score += 10;
                Score.Text = score.ToString();
                if (this.score > this.highscore)
                {
                    this.highscore = score;
                }
            }
            else
            {
                score -= 10;
                lives--;
                if (lives == 0)
                {
                    dispatcherTimer.Stop();
                    this.Frame.Navigate(typeof(EndPage), highscore.ToString());
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 0, 255, 0")
            {
                score += 10;
                Score.Text = score.ToString();
                if (this.score > this.highscore)
                {
                    this.highscore = score;
                }
            }
            else
            {
                score -= 10;
                lives--;
                if (lives == 0)
                {
                    dispatcherTimer.Stop();
                    this.Frame.Navigate(typeof(EndPage), highscore.ToString());
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 0, 0, 255")
            {
                score += 10;
                Score.Text = score.ToString();
                if (this.score > this.highscore)
                {
                    this.highscore = score;
                }
            }
            else
            {
                score -= 10;
                lives--;
                if (lives == 0)
                {
                    dispatcherTimer.Stop();
                    this.Frame.Navigate(typeof(EndPage), highscore.ToString());
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 255, 255, 0")
            {
                score += 10;
                Score.Text = score.ToString();
                if (this.score > this.highscore)
                {
                    this.highscore = score;
                }
            }
            else
            {
                score -= 10;
                lives--;
                if (lives == 0)
                {
                    dispatcherTimer.Stop();
                    this.Frame.Navigate(typeof(EndPage), highscore.ToString());
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 0, 0, 0")
            {
                score += 10;
                Score.Text = score.ToString();
                if (this.score > this.highscore)
                {
                    this.highscore = score;
                }
            }
            else
            {
                score -= 10;
                lives--;
                if (lives == 0)
                {
                    dispatcherTimer.Stop();
                    this.Frame.Navigate(typeof(EndPage), highscore.ToString());
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }
    }
}
