using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_GoldenStore.Model;

namespace XF_GoldenStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateMerchantPage : ContentPage
    {
        private Users user;
        private Products product;
        public UpdateMerchantPage(Users user, Products product)
        {
            InitializeComponent();
            this.user = user;
            this.product = product;

            Username.Text = user.Username;
            Password.Text = user.Password;
            Email.Text = user.Email;
            Mobile.Text = user.Zone;
            Zone.Text = user.City;
            ShopName.Text = user.ShopName;
            ProductName.Text = product.ProductName;

            img1.Source = product.Url1;
            //img2.Source = product.Url[1];
            //img3.Source = product.Url[2];
            //img4.Source = product.Url[3];

            Back.Clicked += (s, e) =>  Navigation.PushAsync(new ShowMerchantPage(user));

        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(Username.Text)) && (!string.IsNullOrEmpty(Password.Text)))
            {
                user.Username = Username.Text;
                user.Password = Password.Text;
                user.Email = Email.Text;
                user.Mobile = Mobile.Text;
                user.Zone = Zone.Text;
                user.City = City.Text;
                user.ShopName = ShopName.Text;

                product.ProductName = ProductName.Text;

                await App.DBSQLite.SaveUserAsync(user);
                await App.DBSQLite.SaveProductAsync(product);

                await DisplayAlert("Done", "Users @ Products are added", "Ok");
                await Navigation.PushAsync(new ShowMerchantPage(user));
            }
            else
                await DisplayAlert("Error", "Username is empty Or Username is already existe", "Ok");
        }    
    }
}