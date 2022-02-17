using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Asignment_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Navigation : Page
    {
        public Navigation()
        {
            this.InitializeComponent();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = sender.SelectedItem as NavigationViewItem;
            Debug.WriteLine(item);
            switch (item.Tag)
            {
                case "latesSong":
                    MainContent.Navigate(typeof(Pages.ListSongPage));
                    break;
                case "profile":
                    MainContent.Navigate(typeof(Pages.ProfilePage));
                    break;
                case "createSong":
                    MainContent.Navigate(typeof(Pages.UploadSongPage));
                    break;
                case "mySong":
                    MainContent.Navigate(typeof(Pages.MySongPage));
                    break;
            }
        }
    }
}
