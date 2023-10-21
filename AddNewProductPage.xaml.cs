using Plugin.Media;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_GoldenStore.Model;

namespace XF_GoldenStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewProductPage : ContentPage
    {
        private Users user;
        private Products product = new Products();       
        public string CurrentImageBase64 { get; set; }

        public AddNewProductPage(Users user)
        {
            InitializeComponent();
            this.user = user;
           
            Cancel.Clicked += (s, e) => Navigation.PushAsync(new DisplayAllProductForUser(user));
        }

        private void BtnTakePhoto1_Clicked(object sender, EventArgs e)
        {
            TakePhoto(img1, product.Url1);            
        }
        private void BtnTakePhoto2_Clicked(object sender, EventArgs e)
        {
            TakePhoto(img2, product.Url2);
        }
        private void BtnTakePhoto3_Clicked(object sender, EventArgs e)
        {
            TakePhoto(img3, product.Url3);
        }
        private void BtnTakePhoto4_Clicked(object sender, EventArgs e)
        {
            TakePhoto(img4, product.Url4);
        }
        private async void TakePhoto(Image img, string url)
        {
            try
            {
                var current = Plugin.Media.CrossMedia.Current;
                if (current.IsCameraAvailable && current.IsTakePhotoSupported)
                {
                    var photo = await current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        CompressionQuality = 75
                    });

                    img.Source = ImageSource.FromStream(() =>
                    {
                        var stream = photo.GetStream();
                        CurrentImageBase64 = GetBase64(photo.GetStream());
                        url = CurrentImageBase64;
                        BtnSaveProduct.IsVisible = true;
                        photo.Dispose();
                        return stream;
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Take Photo", ex.Message, "Ok");
            }
        }
        private string GetBase64(Stream stream)
        {
            byte[] array;
            using(MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                array = memory.ToArray();
            }

            return Convert.ToBase64String(array);
        }
        private async void BtnSaveProduct_Clicked(object sender, EventArgs e)
        {
            try
            {
                product.ProductName = ProductName.Text;
                product.Mobile = user.Mobile;
                await App.DBSQLite.SaveProductAsync(product);
                
                await Navigation.PushAsync(new DisplayAllProductForUser(user));
               
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Save Product", ex.Message, "Ok");
            }
        }
        private Stream GetStream(string base64)
        {
            byte[] array = Convert.FromBase64String(base64);
            return new MemoryStream(array);
        }
    }    
}