using Asignment_UWP.Entity;
using Asignment_UWP.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
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
    public sealed partial class MySongPage : Page
    {
        private SongService songService = new SongService();
        public MySongPage()
        {
            this.InitializeComponent();
            this.Loaded += MySongPage_Loaded;
        }

        private async void MySongPage_Loaded(object sender, RoutedEventArgs e)
        {
            var listSong = await songService.GetMySong();
            Debug.WriteLine(listSong);
            MyListView.ItemsSource = listSong;
        }

        private void MyListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedItem = (Song)MyListView.SelectedItem;
            MyMediaPlayer.Source = MediaSource.CreateFromUri(new Uri(selectedItem.link));
        }
    }
}
