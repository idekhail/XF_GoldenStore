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
    public partial class LoadMerchantPage : ContentPage
    {
        private Users user = new Users();
        private Products product;

        public LoadMerchantPage()
        {

            InitializeComponent();
            
            LoadUsers();
            LoadSelectedItem();            
        }


        public void LoadSelectedItem()
        {
            listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                this.user = (Users)e.SelectedItem;

                // now you can reference item.Name, item.Location, etc
                Navigation.PushAsync(new DisplayAllProductForUser(user.Id));

               // DisplayAlert("ItemSelected", user.Mobile, "Ok");
            };
        }



        public async void LoadUsers()
        {
            try
            {
                var myList = await App.DBSQLite.GetAllUsersAsync();
                if (myList != null)
                    listView.ItemsSource = myList;
                
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

        private async void Update_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new UpdateMerchantPage(this.user));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }

        }

        private async void BtnGallary_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayOneProductInGalleryPage(product));
        }


        private async void AddNewProduct_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewProductPage(user));

        }
    }
}