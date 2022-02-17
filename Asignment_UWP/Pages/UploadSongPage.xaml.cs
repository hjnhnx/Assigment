using Asignment_UWP.Entity;
using Asignment_UWP.Service;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Asignment_UWP.Pages
{
    public sealed partial class UploadSongPage : Windows.UI.Xaml.Controls.Page
    {
        private static string publicIDCloudinary;
        private CloudinaryDotNet.Account accountCloudinary;
        private Cloudinary cloudinary;
        private SongService songService = new SongService();
        private int checkValid = 0;
        public UploadSongPage()
        {
            this.InitializeComponent();
            this.Loaded += CreateSongPage_Loaded;
        }

        private void CreateSongPage_Loaded(object sender, RoutedEventArgs e)
        {
            accountCloudinary = new CloudinaryDotNet.Account(
            "dn3bmj5ex",
            "344297185835677",
            "SanBwHJT4cGsaTibpYRpt0GzzmE"
            );
            cloudinary = new Cloudinary(accountCloudinary);
            cloudinary.Api.Secure = true;
        }

        private void checkValidate(string Name, string Description, string Singer, string Author, string Thumbnail, string Link)
        {
            if (string.IsNullOrEmpty(Name))
            {
                checkName.Visibility = Visibility.Visible;
            }
            else
            {
                checkName.Visibility = Visibility.Collapsed;
                checkValid++;
            }
            if (string.IsNullOrEmpty(Description))
            {
                checkDescription.Visibility = Visibility.Visible;
            }
            else
            {
                checkDescription.Visibility = Visibility.Collapsed;
                checkValid++;
            }
            if (string.IsNullOrEmpty(Singer))
            {
                checkSinger.Visibility = Visibility.Visible;
            }
            else
            {
                checkSinger.Visibility = Visibility.Collapsed;
                checkValid++;
            }
            if (string.IsNullOrEmpty(Author))
            {
                checkAuthor.Visibility = Visibility.Visible;
            }
            else
            {
                checkAuthor.Visibility = Visibility.Collapsed;
                checkValid++;
            }
            if (string.IsNullOrEmpty(Thumbnail))
            {
                checkThumbnail.Visibility = Visibility.Visible;
            }
            else
            {
                checkThumbnail.Visibility = Visibility.Collapsed;
                checkValid++;
            }
            if (string.IsNullOrEmpty(Link))
            {
                checkLink.Visibility = Visibility.Visible;
            }
            else
            {
                checkLink.Visibility = Visibility.Collapsed;
                checkValid++;
            }
        }

        private async void Button_CreateSong(object sender, RoutedEventArgs e)
        {
            checkValidate(name.Text, description.Text, singer.Text, author.Text, thumbnail.Text, link.Text);
            if (checkValid < 6)
            {
                return;
            }
            waitingRespone.Visibility = Visibility.Visible;
            var songCreate = new Song()
            {
                name = name.Text,
                description = description.Text,
                singer = singer.Text,
                author = author.Text,
                thumbnail = thumbnail.Text,
                link = link.Text,
            };

            bool result = await songService.CreateSong(songCreate);

            ContentDialog contentDialog = new ContentDialog();
            waitingRespone.Visibility = Visibility.Collapsed;
            if (result)
            {
                contentDialog.Title = "Success!";
                contentDialog.Content = "Create song success";
            }
            else
            {
                contentDialog.Title = "False";
                contentDialog.Content = "Create song failed";
            }
            contentDialog.CloseButtonText = "Done";
            await contentDialog.ShowAsync();
        }
        private async void Button_CreateThumbnail(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                IRandomAccessStream fileStream = await file.OpenReadAsync();
                await bitmapImage.SetSourceAsync(fileStream);
                imageThumbnail.Source = bitmapImage;
                RawUploadParams imageUploadParams = new RawUploadParams()
                {
                    File = new FileDescription(file.Name, await file.OpenStreamForReadAsync())
                };
                RawUploadResult result = await cloudinary.UploadAsync(imageUploadParams);
                publicIDCloudinary = result.PublicId;
                thumbnail.Text = result.Url.ToString();
                createThumbnail.Visibility = Visibility.Collapsed;
                deleteThumbnail.Visibility = Visibility.Visible;
            }
            else
            {
                Debug.WriteLine("Create image by song failed!");
            }
        }

        private async void Button_AddSong(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".mp3");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                await file.OpenReadAsync();
                RawUploadParams songUploadParams = new RawUploadParams()
                {
                    File = new FileDescription(file.Name, await file.OpenStreamForReadAsync())
                };
                Debug.WriteLine(songUploadParams);
                RawUploadResult result = await cloudinary.UploadAsync(songUploadParams);
                publicIDCloudinary = result.PublicId;
                link.Text = result.Url.ToString();
                createSong.Visibility = Visibility.Collapsed;
                deleteSong.Visibility = Visibility.Visible;
            }
            else
            {
                Debug.WriteLine("Add song failed!");
            }
        }

        private async void Button_DeleteThumbnail(object sender, RoutedEventArgs e)
        {
            List<string> listPublicIdCouldinary = new List<string>();
            listPublicIdCouldinary.Add(publicIDCloudinary);
            string[] arrayPublicIdCouldinary = listPublicIdCouldinary.ToArray();
            await cloudinary.DeleteResourcesAsync(arrayPublicIdCouldinary);
            deleteThumbnail.Visibility = Visibility.Collapsed;
            createThumbnail.Visibility = Visibility.Visible;
            imageThumbnail.Source = null;
            thumbnail.Text = "";
        }
        private async void Button_DeleteSong(object sender, RoutedEventArgs e)
        {
            List<string> listPublicIdCouldinary = new List<string>();
            listPublicIdCouldinary.Add(publicIDCloudinary);
            string[] arrayPublicIdCouldinary = listPublicIdCouldinary.ToArray();
            await cloudinary.DeleteResourcesAsync(arrayPublicIdCouldinary);
            deleteSong.Visibility = Visibility.Collapsed;
            createSong.Visibility = Visibility.Visible;
            imageThumbnail.Source = null;
            link.Text = "";
        }
    }
}
