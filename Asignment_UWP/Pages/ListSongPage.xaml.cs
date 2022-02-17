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

namespace Asignment_UWP.Pages
{
    public sealed partial class ListSongPage : Page
    {
        private SongService songService = new SongService();
        public ListSongPage()
        {
            this.Loaded += ListSong_LoadedAsync;
            this.InitializeComponent();
        }
        private async void ListSong_LoadedAsync(object sender, RoutedEventArgs e)
        {
            var listSong = await songService.GetLatestSong();
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
