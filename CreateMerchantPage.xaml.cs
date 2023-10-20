using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_GoldenStore.Model;

namespace XF_GoldenStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateMerchantPage : ContentPage
    {
        public CreateMerchantPage()
        {
            InitializeComponent();

            Cancel.Clicked += (s, e) => Navigation.PopAsync();
        }


        private async void Create_Clicked(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(Username.Text)) && (!string.IsNullOrEmpty(Password.Text)))
            {
                var user = new Users()
                {
                    Username = Username.Text,
                    Password = Password.Text,
                    Email = Email.Text,
                    Mobile = Mobile.Text,
                    Zone = Zone.Text,
                    City = City.Text,
                    ShopName = ShopName.Text,
                };
                await App.DBSQLite.SaveUserAsync(user);
                await DisplayAlert("Done", "Username is added", "Ok");
                await Navigation.PopAsync();
            }
            else
                await DisplayAlert("Error", "Username is empty Or Username is already existe", "Ok");
        }

    }
    
}