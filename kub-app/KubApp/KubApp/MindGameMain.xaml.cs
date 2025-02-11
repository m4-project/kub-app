﻿using KubApp_v0._1;
using MindGame;
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
    public sealed partial class MindGameMain : Page
    {
        public MindGameMain()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Deze methode verwijst u naar de andere pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            //Deze methode verwijst u naar de "Easy" pagina.
            this.Frame.Navigate(typeof(Easy));
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Medium));
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Hard));
        }

        private void Instruction_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Instruction));
        }

        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainGameMain));
        }
    }
}
