using Asignment_UWP.Service;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Asignment_UWP.Pages
{
    public sealed partial class ProfilePage : Page
    {
        private UserService service = new UserService();
        public ProfilePage()
        {
            this.InitializeComponent();
            this.Loaded += Profile_Loaded;
        }

        private async void Profile_Loaded(object sender, RoutedEventArgs e)
        {
            var account = await service.GetProfile();
            if (account == null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Login required";
                contentDialog.Content = $"Please login to continue!";
                contentDialog.PrimaryButtonText = "Got it!";
                await contentDialog.ShowAsync();
                Frame.Navigate(typeof(Pages.LoginPage));
            }
            else
            {
                avatar.Source = new BitmapImage(new Uri(account.avatar));
                email.Text = account.email;
                fullName.Text = $"{account.firstName} {account.lastName}";
                address.Text = account.address;
                birthday.Text = account.birthday;
            }
        }
    }
}
