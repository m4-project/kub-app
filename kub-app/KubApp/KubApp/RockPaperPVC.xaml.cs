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
            string pointstoadd = e.Parameter as string;
            if (pointstoadd == null)
            {
                currentscore = 0;
                currenthighscore = 0;
            }
            else
            {
                if (pointstoadd == "0")
                {
                    currenthighscore = currentscore;
                    highscore.Text = currenthighscore.ToString();
                    score.Text = "0";
                    currentscore = 0;
                    
                }
                else
                {
                    currentscore = currentscore + Int32.Parse(pointstoadd);
                    currenthighscore = currentscore;
                    score.Text = currentscore.ToString();
                    highscore.Text = currentscore.ToString();
                }
            }
        }

        private void rock_Click(object sender, RoutedEventArgs e)
        {
            passlist.Clear();
            passlist.Add(rockpass);
            passlist.Add(currentscore.ToString());
            this.Frame.Navigate(typeof(RockPaperPVCResult), passlist);
        }

        private void paper_Click(object sender, RoutedEventArgs e)
        {
            passlist.Clear();
            passlist.Add(paperpass);
            passlist.Add(currentscore.ToString());
            this.Frame.Navigate(typeof(RockPaperPVCResult), passlist);
        }

        private void scicor_Click(object sender, RoutedEventArgs e)
        {
            passlist.Clear();
            passlist.Add(scicorpass);
            passlist.Add(currentscore.ToString());
            this.Frame.Navigate(typeof(RockPaperPVCResult), passlist);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            string highscore = currentscore.ToString();
            this.Frame.Navigate(typeof(RockPaperPVC), highscore);
        }
    }
}
