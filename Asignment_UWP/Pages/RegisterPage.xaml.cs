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

namespace Asignment_UWP.Pages
{
    public sealed partial class RegisterPage : Page
    {
        private int checkGender;
        private string dateChanged;
        private int check = 0;
        public RegisterPage()
        {
            this.InitializeComponent();
        }
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            var check = sender as RadioButton;
            switch (check.Content)
            {
                case "Male":
                    checkGender = 1;
                    break;
                case "Fermale":
                    checkGender = 2;
                    break;
                case "Other":
                    checkGender = 3;
                    break;
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog();
            var service = new UserService();
            Validate(firstName.Text, lastName.Text, email.Text, address.Text, phone.Text, password.Password);
            if (check != 0)
            {
                return;
            }
            var user = new User()
            {
                firstName = firstName.Text,
                lastName = lastName.Text,
                email = email.Text,
                phone = phone.Text,
                password = password.Password,
                address = address.Text,
                gender = checkGender,
                avatar = avatar.Text,
                birthday = dateChanged,
            };
            var result = await service.Register(user);
            if (result)
            {
                dialog.Title = "Success";
                dialog.Content = "Register Success";
                dialog.CloseButtonText = "Close";
                await dialog.ShowAsync();
                this.Frame.Navigate(typeof(Pages.LoginPage));
            }
            else
            {
                dialog.Title = "Error";
                dialog.Content = "Register Failed";
                dialog.CloseButtonText = "Close";
                await dialog.ShowAsync();
            }
        }
        private void birthday_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var date = sender;
            dateChanged = date.Date.Value.ToString("yyyy-MM-dd");
        }
        private void Validate(string Fname, string Lname, string Email, string Address, string Phone, string Password)
        {
            if (string.IsNullOrEmpty(Fname))
            {
                checkFirstName.Text = "FirstName is required";
                check++;
            }
            if (string.IsNullOrEmpty(Lname))
            {
                checkLastName.Text = "LastName is required";
                check++;
            }
            if (string.IsNullOrEmpty(Email))
            {
                checkEmail.Text = "Email is required";
                check++;
            }
            if (string.IsNullOrEmpty(Address))
            {
                checkAddress.Text = "Address is required";
                check++;
            }
            if (string.IsNullOrEmpty(Phone))
            {
                checkPhone.Text = "Phone is required";
                check++;
            }
            if (string.IsNullOrEmpty(Password))
            {
                checkPassword.Text = "Password is required";
                check++;
            }
        }
        private void Handle_Login(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.LoginPage));
        }

   
    }
}
