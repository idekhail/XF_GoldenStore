using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_GoldenStore.Model;

namespace XF_GoldenStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayAllProductForUser : ContentPage
    {
        private Users user;

        public DisplayAllProductForUser(Users user)
        {
            InitializeComponent();
            this.user = user;
            ShopName.Text = user.ShopName;

            Logout.Clicked += (s, e) => Navigation.PopToRootAsync();

            LoadImage();
            LoadSelectedItem();
        }

        public void LoadSelectedItem()
        {
            listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var product = (Products)e.SelectedItem;

                // now you can reference item.Name, item.Location, etc
                Navigation.PushAsync(new DisplayOneProductInGalleryPage(product));
            };
        }

        public async void LoadImage()
        {
            try
            {
                var productList = await App.DBSQLite.GetAllProductForUserAsync(user.Mobile);                
                if (productList != null)
                    listView.ItemsSource = productList;
                 
            }
            catch (Exception ex)
            {
               await  DisplayAlert("Error Take Photo", ex.Message, "Ok");
            }
        }
       
        private Stream GetStream(string base64)
        {
            byte[] array = Convert.FromBase64String(base64);
            return new MemoryStream(array);
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            try
            {
                var product = await App.DBSQLite.GetProductAsync(user.Mobile);
                await Navigation.PushAsync(new UpdateMerchantPage(user, product));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }

        }
         
        private async void AddNewProduct_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new AddNewProductPage(user));

        }
    }
}