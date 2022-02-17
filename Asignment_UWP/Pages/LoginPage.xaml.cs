using Asignment_UWP.Entity;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Asignment_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private UserService service = new UserService();
        private int checkValid = 0;
        public LoginPage()
        {
            this.InitializeComponent();
        }
        private async void Handle_Login(object sender, RoutedEventArgs e)
        {
            checkValidate(email.Text, password.Password.ToString());
            if (checkValid < 2)
            {
                return;
            }
            var account = new Account()
            {
                email = email.Text,
                password = password.Password.ToString()
            };

            var credential = await service.Login(account);
            if (credential == null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Login failed";
                contentDialog.Content = "Please try again later!";
                contentDialog.PrimaryButtonText = "Close";
                await contentDialog.ShowAsync();
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog();
                this.Frame.Navigate(typeof(Pages.Navigation));
            }
        }
        private void checkValidate(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email))
            {
                checkEmail.Visibility = Visibility.Visible;
            }
            else
            {
                checkEmail.Visibility = Visibility.Collapsed;
                checkValid++;
            }
            if (string.IsNullOrEmpty(Password))
            {
                checkPassword.Visibility = Visibility.Visible;
            }
            else
            {
                checkPassword.Visibility = Visibility.Collapsed;
                checkValid++;
            }
        }

        private void Handle_Register(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.RegisterPage));
        }
    }
}
