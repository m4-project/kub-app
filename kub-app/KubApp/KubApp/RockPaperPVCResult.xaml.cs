using KubApp_v0._1;
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

namespace KubApp
{
    /// <summary>
    /// Displays result of player versus computer game, passes roundscore back to RockPaperPVC.xaml
    /// </summary>
    public sealed partial class RockPaperPVCResult : Page
    {
        // declare variables used in class
        private int PlayerInt = 0;
        private int PcInt = 0;
        public int roundscore = 0;
        public int highscore = 0;
        public List<string> passlist = new List<string>();

        public RockPaperPVCResult()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // method used for displaying proper image for player chosen var
            // rock, paper or scicor.
            List<string> passedlist = new List<string>();
            passedlist = e.Parameter as List<string>;
            this.roundscore = Int32.Parse(passedlist[1]);
            this.highscore = Int32.Parse(passedlist[2]);
            if (passedlist[0] == "rock")
            {
                imageSteen.Opacity = 100;
                PlayerInt = 1;
            }
            else if (passedlist[0] == "paper")
            {
                imagePapier.Opacity = 100;
                PlayerInt = 2;
            }
            else if (passedlist[0] == "scicor")
            {
                imageSchaar.Opacity = 100;
                PlayerInt = 3;
            }
            else
            {
                this.Frame.Navigate(typeof(RockPaperMain), null);
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // executed on page loaded
            // pick a random number 1, 2 or 3 which represents rock paper or scicor.
            Random rnd = new Random();
            int PCpick = rnd.Next(1, 4);
            switch (PCpick)
            {
                // switch case for the random number, display corresponding image (rock paper or scicor)
                case 1:
                    imageSteenPC.Opacity = 100;
                    PcInt = 1;
                    break;
                case 2:
                    imagePapierPC.Opacity = 100;
                    PcInt = 2;
                    break;
                case 3:
                    imageSchaarPC.Opacity = 100;
                    PcInt = 3;
                    break;
                default:
                    break;
            }

            // following if else statement is used for displaying correct result message, 
            // and add correct amount of points for win , draw of lose
            if (PlayerInt == PcInt)
            {
                // tie
                result.Text = "Tie!";
                this.roundscore = roundscore + 1;
                this.highscore = roundscore;

            }
            else if (PlayerInt == 1 && PcInt == 3)
            {
                // win
                result.Text = "You Win!";
                this.roundscore = roundscore + 3;
            }
            else if (PlayerInt == 2 && PcInt == 1)
            {
                // win
                result.Text = "You Win!";
                this.roundscore = roundscore + 3;
            }
            else if (PlayerInt == 3 && PcInt == 2)
            {
                // win
                result.Text = "You Win!";
                this.roundscore = roundscore + 3;
            }
            else
            {
                // lose
                result.Text = "You Lost!";
                this.highscore = roundscore;
                this.roundscore = 0;

            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // code for back button,
            // passes roundscore and highscore to rockpaperPVC.xaml

                passlist.Add(roundscore.ToString());
                passlist.Add(highscore.ToString());
                this.Frame.Navigate(typeof(RockPaperPVC), passlist);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            // navigate back to main page
            this.Frame.Navigate(typeof(MainGameMain));
        }
    }
}
