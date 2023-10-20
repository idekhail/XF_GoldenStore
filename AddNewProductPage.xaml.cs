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
           
            Cancel.Clicked += (s, e) => Navigation.PushAsync(new ShowMerchantPage(user));
        }

        private async void BtnTakePhoto_Clicked(object sender, EventArgs e)
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
                    
                    img1.Source = ImageSource.FromStream(() =>
                    {
                        var stream = photo.GetStream();
                        CurrentImageBase64 = GetBase64(photo.GetStream());
                        product.Url1 = CurrentImageBase64;
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
                
                await Navigation.PushAsync(new ShowMerchantPage(user));
               
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