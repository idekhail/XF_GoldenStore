using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_GoldenStore.Model;

namespace XF_GoldenStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProductPage : ContentPage
    {
        private Users user;
        private Products product;
        public UpdateProductPage(Products product)
        {
            InitializeComponent();
            this.product = product;
            this.product = product;
            
            ProductName.Text = product.ProductName;
            Mobile.Text = user.Zone;

            img1.Source = ConvertStremToImage(product.Url1); 
            img2.Source = ConvertStremToImage(product.Url2);
            img3.Source = ConvertStremToImage(product.Url3);
            img4.Source = ConvertStremToImage(product.Url4);

            Back.Clicked += (s, e) =>  Navigation.PushAsync(new DisplayAllProductForUser(user));

        }
       

        public ImageSource ConvertStremToImage(string value)
        {
            string base64Image = (string)value;

            if (base64Image == null)
                return null;

            // Convert base64Image from string to byte-array
            var imageBytes = System.Convert.FromBase64String(base64Image);

            // Return a new ImageSource
            return ImageSource.FromStream(() => { return new MemoryStream(imageBytes); });
        }
        private async void Update_Clicked(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(ProductName.Text)) && (!string.IsNullOrEmpty(Mobile.Text)))
            {
                product.ProductName = ProductName.Text;
                product.Mobile = Mobile.Text;
               
                await App.DBSQLite.SaveUserAsync(user);
                await App.DBSQLite.SaveProductAsync(product);

                await DisplayAlert("Done", "Users @ Products are added", "Ok");
                await Navigation.PushAsync(new DisplayAllProductForUser(user));
            }
            else
                await DisplayAlert("Error", "Username is empty Or Username is already existe", "Ok");
        }    
    }
}