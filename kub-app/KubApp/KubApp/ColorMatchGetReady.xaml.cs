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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorMatchGetReady : Page
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int timevar = 3;
        private string highpass = "";

        public ColorMatchGetReady()
        {
            this.InitializeComponent();
        }

        public void Countdown()
        {
            // initializes dispatcherTimer
            this.dispatcherTimer.Tick += DispatcherTimer_Tick;
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this.dispatcherTimer.Start();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Countdown();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            // executed when timer runs out
            if (this.timevar > 1)
            {
                timevar--;
                textBlock.Text = string.Format("{1}", timevar / 60, timevar % 60);
            }
            else
            {
                dispatcherTimer.Stop();
                this.Frame.Navigate(typeof(ColorMatchGame), highpass);
            }

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // when navigated to this page, gets var from passed parameter, highscore
            var high = e.Parameter as string;
            this.highpass = high;
        }
    }
}
