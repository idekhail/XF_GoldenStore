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
    public partial class ShowMerchantPage : ContentPage
    {
        private Users user;

        public ShowMerchantPage(Users user)
        {
            InitializeComponent();
            this.user = user;
            ShopName.Text = user.ShopName;

            Logout.Clicked += (s, e) => Navigation.PopToRootAsync();
            try
            {
                LoadImage();
            }
            catch(Exception ex) {
                DisplayAlert("Error Take Photo", ex.Message, "Ok");
            }
        }

        public async void LoadImage()
        {
            try
            {
                var productList = await App.DBSQLite.GetAllProductForUserAsync(user.Mobile);
                List<ImgUrl> myImage;

                var img1 = new Image();
                if (productList != null)
                {
                    foreach (var p in productList)
                    {
                        img1.Source = ImageSource.FromStream(() =>
                         {
                             return GetStream(p.Url1);
                         });
                        myImage = new List<ImgUrl>
                        {
                            new ImgUrl{Id=1,Img=img1},
                        };

                    listView.ItemsSource = myImage;

                    }
                }

                //  imgUrl.Img = new Image {Source="img1.jpg" } ;

                 

                
                //if (productList != null)
                //    foreach (var p in productList)
                //    {
                //        img1.Source = ImageSource.FromStream(() =>
                //        {
                //            return GetStream(p.Url1);
                //        });

                //    //img1.Source = p.Url1;
                //}
            }
            catch (Exception ex)
            {
               await  DisplayAlert("Error Take Photo", ex.Message, "Ok");
            }
        }

        //===== Add Image  ==============


        //public Task<int> SaveImageAsync(ImgUrl imgUrl)
        //{
        //    if (imgUrl.Id != 0)
        //    {
        //        // Update an existing User.
        //        return db.UpdateAsync(imgUrl);
        //    }
        //    else
        //    {
        //        // Save a new User.
        //        return db.InsertAsync(imgUrl);
        //    }

        //}
        //public Task<List<ImgUrl>> GetAllImageAsync()
        //{
        //    return db.Table<ImgUrl>().ToListAsync();
        //}
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

        private async void BtnGallary_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DisplayGalleryPage());
        }

     
        private async void AddNewProduct_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new AddNewProductPage(user));

        }
    }
}