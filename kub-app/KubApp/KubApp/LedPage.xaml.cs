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
using Windows.UI.Xaml.Media.Imaging;
using ColorPicker;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LedPage : Page
    {
        public LedPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Linkt naar main page
        /// </summary>
        private void LinkMainPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }

        /// <summary>
        /// Verander kleur naar geselecteerde kleur in de colorpicker
        /// </summary>
        private void colorChange()
        {
            SolidColorBrush brush = colorp.SelectedColor;
            currentColor.Fill = brush;

            string hexColor = brush.Color.ToString();
            textBox.Text = hexColor;

            string hexColorSub = hexColor.Substring(3);
            int R = int.Parse(hexColorSub.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(hexColorSub.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(hexColorSub.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            textBox1.Text = R.ToString();
            textBox2.Text = G.ToString();
            textBox3.Text = B.ToString();
        }

        /// <summary>
        /// Roept colorChanged() met kleur aan op basis van plaats van muis
        /// </summary>
        private void colorp_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            colorChange();
        }

        private void colorp_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            colorChange();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
