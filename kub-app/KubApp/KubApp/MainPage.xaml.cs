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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KubApp_v0._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //byte tempature;

        public MainPage()
        {
            this.InitializeComponent();
            //Tempature();
        }

        //public void tempatureKub(byte tempature)
        //{
        //    if()
        //    {

        //    }
        //    else
        //    {

        //    }
        //}

        //public void Tempature()
        //{
        //    tempature = 80;

        //    TempatureKub.Text = "Tempature Kub = " + tempature + " °C";

        //    if(tempature >= 60)
        //    {
        //        LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.Red);
        //    }
        //    else if(tempature < 60 && tempature > 30)
        //    {
        //        LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.Green);
        //    }
        //    else if(tempature < 30 && tempature > 0)
        //    {
        //        LayoutGrid.Background = new SolidColorBrush(Windows.UI.Colors.Blue);
        //    }
        //}
    }
}
