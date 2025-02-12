﻿using System;
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
using Windows.UI.Xaml.Media.Animation;
using KubApp_v0._1;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorMatchWrong : Page
    {
        private string highpass = "";
        public ColorMatchWrong()
        {
            this.InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            // go back to restarted game
            this.Frame.Navigate(typeof(ColorMatchGetReady), highpass);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // gets highscore from passed parameter and displays it
            var high = e.Parameter as string;
            this.highpass = high;
            textBlock2.Text = high;
        }

        private void quitGame_Click(object sender, RoutedEventArgs e)
        {
            // navigate back to mainpage
            this.Frame.Navigate(typeof(MainGameMain));
        }
    }
}
