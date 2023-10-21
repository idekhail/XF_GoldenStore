using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF_GoldenStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginMerchantPage : ContentPage
    {
        public LoginMerchantPage()
        {
            InitializeComponent();
            Create.Clicked += (s, e) => Navigation.PushAsync(new CreateMerchantPage());
            Home.Clicked += (s, e) => Navigation.PushAsync(new MainGoldenStorePage());
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(Username.Text))
            {
                var user = await App.DBSQLite.LoginAsync(Username.Text, Password.Text);
                if (user != null)
                {

                    await Navigation.PushAsync(new DisplayAllProductForUser(user));

                }
                else
                    await DisplayAlert("Error", "Username is error", "Ok");
            }
            else
                await DisplayAlert("Error", "Username is empty", "Ok");


        }
    }
}