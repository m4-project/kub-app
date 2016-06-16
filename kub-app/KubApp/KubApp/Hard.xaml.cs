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
    public sealed partial class Hard : Page
    {
        private int time = 4;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Random rnd = new Random();
        int randomNummer;
        int randomNummer1;
        int score = 0;
        int lives = 3;
        private int highscore = 0;

        public Hard()
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

                Random rnd = new Random();

                //Generate new colours
                randomNummer = rnd.Next(9); //random number 0 to 10

                if (randomNummer == 0) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                else if (randomNummer == 1) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 0));
                else if (randomNummer == 2) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 255));
                else if (randomNummer == 3) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 0));
                else if (randomNummer == 4) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
                else if (randomNummer == 5) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 128, 128, 128));
                else if (randomNummer == 6) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 75, 0, 130));
                else if (randomNummer == 7) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 127, 80));
                else if (randomNummer == 8) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 69, 0));

                //Generate new colours
                randomNummer1 = rnd.Next(9); //random number 0 to 10

                if (randomNummer1 == 0) randomColor.Content = "Red";
                if (randomNummer1 == 1) randomColor.Content = "Blue";
                if (randomNummer1 == 2) randomColor.Content = "Green";
                if (randomNummer1 == 3) randomColor.Content = "Yellow";
                if (randomNummer1 == 4) randomColor.Content = "Black";
                if (randomNummer1 == 5) randomColor.Content = "Gray";
                if (randomNummer1 == 6) randomColor.Content = "Indigo";
                if (randomNummer1 == 7) randomColor.Content = "Coral";
                if (randomNummer1 == 8) randomColor.Content = "OrangeRed";
            }
        }

        private void timerStart()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
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
                    this.Frame.Navigate(typeof(EndPage), highscore);
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
                    this.Frame.Navigate(typeof(EndPage), highscore);
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
                    this.Frame.Navigate(typeof(EndPage), highscore);
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
                    this.Frame.Navigate(typeof(EndPage),highscore);
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
                    this.Frame.Navigate(typeof(EndPage), highscore);
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 128, 128, 128")
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
                    this.Frame.Navigate(typeof(EndPage), highscore);
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 75, 0, 130")
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
                    this.Frame.Navigate(typeof(EndPage), highscore);
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 255, 127, 80")
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
                    this.Frame.Navigate(typeof(EndPage), highscore);
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 255, 69, 0")
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
                    this.Frame.Navigate(typeof(EndPage), highscore);
                }
                Lives.Text = lives.ToString();
                Score.Text = score.ToString();
            }
        }

        private void Startbtn_Click(object sender, RoutedEventArgs e)
        {
            button.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button2.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button3.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button4.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button5.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button6.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button7.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button9.Visibility = Windows.UI.Xaml.Visibility.Visible;

            Startbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Stopbtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            timerStart();
        }

        private void Stopbtn_Click(object sender, RoutedEventArgs e)
        {
            button.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button3.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button5.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button6.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button7.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button9.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            Stopbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Startbtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            dispatcherTimer.Stop();
            this.Frame.Navigate(typeof(EndPage), highscore);
        }
    }
}
