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
    public sealed partial class Easy : Page
    {
        private int time = 4;                                               //Dit is een default waarde van de countdown timer.
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();    //Dit initialiseert een timer.
        Random rnd = new Random();                                          //Dit is een variabel voor de random methode.

        int randomNummer;                                                   //Dit is een variabel voor de DispatcherTimer_Tick methode.
        int randomNummer1;                                                  

        int livesEasy = 3;                                                  //Dit is een default waarde van het aantal levens.

        public int scoreEasy { get; set; }                                  //Hiermee wordt de score naar de "EndPage" doorgegeven.
        
        public int highscore = 0;                                           //Dit is een default waarde van "Highscore".

        public Easy()
        {
            this.InitializeComponent();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            if (time > 0)
            {
                time--;
                textBlock1.Text = string.Format("{1}", time / 60, time % 60); //Hiermee kunt u alleen de aantal seconden tijdens een countdown zien.
            }
            else
            {
                textBlock1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                //Hiermee genereert u nieuwe achtergrondkleur
                randomNummer = rnd.Next(4); //random nummer tussen 0 to 4

                if (randomNummer == 0) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                else if (randomNummer == 1) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 0));
                else if (randomNummer == 2) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 255));
                else if (randomNummer == 3) randomColor.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 0));

                //Hiermee genereert u de button text
                randomNummer1 = rnd.Next(4); //random number 0 to 4

                if (randomNummer1 == 0) randomColor.Content = "Red";
                else if (randomNummer1 == 1) randomColor.Content = "Blue";
                else if (randomNummer1 == 2) randomColor.Content = "Green";
                else if (randomNummer1 == 3) randomColor.Content = "Yellow";
            }
        }

        private void Startbtn_Click(object sender, RoutedEventArgs e)
        {
            //Met deze methode maakt u de button weer zichtbaar.
            BtnRed.Visibility = Windows.UI.Xaml.Visibility.Visible;
            BtnGreen.Visibility = Windows.UI.Xaml.Visibility.Visible;
            BtnBlue.Visibility = Windows.UI.Xaml.Visibility.Visible;
            BtnYellow.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //Met deze methode maakt u de startbutton onzichtbaar.
            Startbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            //Met deze methode maakt u de stopbutton zichtbaar.
            Stopbtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            TimerStart();
        }

        private void Stopbtn_Click(object sender, RoutedEventArgs e)
        {
            BtnRed.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            BtnGreen.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            BtnBlue.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            BtnYellow.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            Stopbtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Startbtn.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //Met deze methode kunt u naar de "EndPage" navigeren.
            this.Frame.Navigate(typeof(EndPage), highscore.ToString());
        }

        /// <summary>
        /// Met deze methode activeer u de timer.
        /// </summary>
        private void TimerStart()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Met behulp van deze vier button click method hieronder kunt u de button kleur met de string waarde vergelijken.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRed_Click(object sender, RoutedEventArgs e)
        {
            //Hiermee krijgt u de achtergrondkleur van de button.
            SolidColorBrush brush = (SolidColorBrush)randomColor.Background;

            //Hiermee verandert u de integer waarde naar een string.
            string hexColor = brush.Color.ToString();

            string substringHexColor = hexColor.Substring(3);

            int R = int.Parse(substringHexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(substringHexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(substringHexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            string colorsCombined = "255, " + R.ToString() + ", " + G.ToString() + ", " + B.ToString();

            if (colorsCombined == "255, 255, 0, 0") //Hiermee vergelijkt u de string waarde.
            {
                this.scoreEasy += 10;               //Wanneer de waarde "255, 255, 0, 0" gelijk aan de string waarde "colorsCombined" krijgt u + 10 punten bij de score.
                Score.Text = scoreEasy.ToString();  //Hier wordt de score naar textblock overgeplaats.
                if(this.scoreEasy > this.highscore) //Hiermee vergelijkt u of de huidige score groter is dan de highscore.
                {
                    this.highscore = scoreEasy; //zo ja? wordt de huidige score uw nieuwe highscore.
                }
            }
            else
            {
                this.scoreEasy -= 10; // Wanneer de waarde "255, 255, 0, 0" niet gelijk aan de string waarde "colorsCombined" krijgt u - 10 punten bij de score.
                livesEasy--; //Naast - 10 punten neemt uw levens ook met -1 af.
                if (livesEasy == 0) 
                {
                    dispatcherTimer.Stop(); //Wanneer de integer waarde van uw levens gelijk is aan 0 
                    this.Frame.Navigate(typeof(EndPage), scoreEasy.ToString());
                }
                Lives.Text = livesEasy.ToString();
                Score.Text = scoreEasy.ToString();    
            }
        }

        private void BtnGreen_Click(object sender, RoutedEventArgs e)
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
                this.scoreEasy += 10;
                Score.Text = scoreEasy.ToString();
                if (this.scoreEasy > this.highscore)
                {
                    this.highscore = scoreEasy;
                }
            }
            else
            {
                this.scoreEasy -= 10;
                livesEasy--;
                if (livesEasy == 0)
                {
                    dispatcherTimer.Stop();
                    this.Frame.Navigate(typeof(EndPage), this.scoreEasy.ToString());
                }
                Lives.Text = livesEasy.ToString();
                Score.Text = scoreEasy.ToString();
            }
        }

        private void BtnBlue_Click(object sender, RoutedEventArgs e)
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
                this.scoreEasy += 10;
                Score.Text = scoreEasy.ToString();
                if (this.scoreEasy > this.highscore)
                {
                    this.highscore = scoreEasy;
                }
            }
            else
            {
                this.scoreEasy -= 10;
                livesEasy--;
                if (livesEasy == 0)
                {
                    dispatcherTimer.Stop();
                    this.Frame.Navigate(typeof(EndPage), this.scoreEasy.ToString());
                }
                Lives.Text = livesEasy.ToString();
                Score.Text = scoreEasy.ToString();
            }
        }

        private void BtnYellow_Click(object sender, RoutedEventArgs e)
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
                this.scoreEasy += 10;
                Score.Text = scoreEasy.ToString();
                if (this.scoreEasy > this.highscore)
                {
                    this.highscore = scoreEasy;
                }
            }
            else
            {
                this.scoreEasy -= 10;
                livesEasy--;
                if (livesEasy == 0)
                {
                    dispatcherTimer.Stop();
                    this.Frame.Navigate(typeof(EndPage), this.scoreEasy.ToString());
                }
                Lives.Text = livesEasy.ToString();
                Score.Text = scoreEasy.ToString();
            }
        }
    }
}
