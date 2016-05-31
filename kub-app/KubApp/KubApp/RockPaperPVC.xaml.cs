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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RockPaperPVC : Page
    {
        private static string rockpass = "rock";
        private static string paperpass = "paper";
        private static string scicorpass = "scicor";
        public int currentscore = 0;
        public int currenthighscore = 0;
        public List<string> passlist = new List<string>();

        public RockPaperPVC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<string> passedlist = new List<string>();
            passedlist = e.Parameter as List<string>;
            if (passedlist == null)
            {
                currenthighscore = 0;
                currentscore = 0;
            }
            else
            {
                int returnedscore = Int32.Parse(passedlist[0]);
                int returnedhighscore = Int32.Parse(passedlist[1]);

                if (returnedscore < returnedhighscore)
                {
                    currenthighscore = returnedhighscore;
                    currentscore = returnedscore;
                    highscore.Text = returnedhighscore.ToString();
                    score.Text = currentscore.ToString();
                }
                else
                {
                    currentscore = returnedscore;
                    currenthighscore = returnedscore;
                    score.Text = currentscore.ToString();
                    highscore.Text = currenthighscore.ToString();
                }
            }
        }

        private void rock_Click(object sender, RoutedEventArgs e)
        {
            passlist.Clear();
            passlist.Add(rockpass);
            passlist.Add(currentscore.ToString());
            passlist.Add(currenthighscore.ToString());
            this.Frame.Navigate(typeof(RockPaperPVCResult), passlist);
        }

        private void paper_Click(object sender, RoutedEventArgs e)
        {
            passlist.Clear();
            passlist.Add(paperpass);
            passlist.Add(currentscore.ToString());
            passlist.Add(currenthighscore.ToString());
            this.Frame.Navigate(typeof(RockPaperPVCResult), passlist);
        }

        private void scicor_Click(object sender, RoutedEventArgs e)
        {
            passlist.Clear();
            passlist.Add(scicorpass);
            passlist.Add(currentscore.ToString());
            passlist.Add(currenthighscore.ToString());
            this.Frame.Navigate(typeof(RockPaperPVCResult), passlist);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            string highscore = currentscore.ToString();
            this.Frame.Navigate(typeof(RockPaperPVC), highscore);
        }
    }
}
