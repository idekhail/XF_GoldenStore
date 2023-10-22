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

        public DisplayAllProductForUser(int id)
        {
            InitializeComponent();

            AddNewProduct.IsVisible = false;
            UpdateUser.IsVisible = false;
            Logout.IsVisible = false;

            LoadImage(id);
            LoadSelectedItem();                       
        }
        public DisplayAllProductForUser(Users user)
        {
            InitializeComponent();
            this.user = user;
            AddNewProduct.IsVisible = true;
            UpdateUser.IsVisible = true;
            Logout.IsVisible = true;
            ShopName.Text = user.ShopName;

            Logout.Clicked     += (s, e) => Navigation.PopToRootAsync();
            UpdateUser.Clicked += (s, e) => Navigation.PushAsync(new UpdateMerchantPage(user));
            AddNewProduct.Clicked += (s, e) => Navigation.PushAsync(new AddNewProductPage(user));

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
                foreach(var p in productList)
                {
                    img1.Source = p.Url1;
                }
                 
            }
            catch (Exception ex)
            {
               await  DisplayAlert("Error Take Photo", ex.Message, "Ok");
            }
        }
        public async void LoadImage(int id)
        {
            try
            {
                this.user = await App.DBSQLite.GetUserAsync(id);
                ShopName.Text = user.ShopName;
                var productList = await App.DBSQLite.GetAllProductForUserAsync(user.Mobile);
                if (productList != null)
                    listView.ItemsSource = productList;
                foreach (var p in productList)
                {
                    img1.Source = p.Url1;
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Take Photo", ex.Message, "Ok");
            }
        }
        private Stream GetStream(string base64)
        {
            byte[] array = Convert.FromBase64String(base64);
            return new MemoryStream(array);
        }       
         
       
    }
}