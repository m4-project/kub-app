using KubApp_v0._1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using ZXing;
using ZXing.Common;
using ZXing.Mobile;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using KubApp;
using uPLibrary.Networking.M2Mqtt;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class newKub : Page
    {
        public string message;

        public string QRresult;

        public newKub()
        {
            this.InitializeComponent();
            ZXing.Net.Mobile.Forms.WindowsUniversal.ZXingScannerViewRenderer.Init();
            backButtonPressed();
        }

        private MobileBarcodeScanner _scanner;

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _scanner = new MobileBarcodeScanner(this.Dispatcher);
                _scanner.UseCustomOverlay = false;
                _scanner.TopText = "Hold camera up to QR code";
                _scanner.BottomText = "Camera will automatically scan QR code\r\n\rPress the 'Back' button to cancel";

                var result = await _scanner.Scan();
                ProcessScanResult(result);
            }
            catch (Exception error)
            {
               var errorMessage = new MessageDialog(error.ToString());
            }
        }

        private async void ProcessScanResult(ZXing.Result result)
        {
            QRresult = result.Text;
            string newMessage = string.Empty;
            newMessage = (result != null && !string.IsNullOrEmpty(result.Text)) ? "Found QR code: " + result.Text : "Scanning cancelled";
            var dialog = new MessageDialog(newMessage);
            await dialog.ShowAsync();
            MainPage.instance.AddNewKub(QRresult);
        }

        private void backButtonPressed()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                this.Frame.Navigate(typeof(MainPage));
            };
        }
    }
}
