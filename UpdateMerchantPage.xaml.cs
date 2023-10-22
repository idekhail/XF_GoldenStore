using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_GoldenStore.Model;

namespace XF_GoldenStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateMerchantPage : ContentPage
    {
        private Users user;
        public string CurrentImageBase64 { get; set; }

        public UpdateMerchantPage(Users user)
        {
            InitializeComponent();
            this.user = user;

            Username.Text = user.Username;
            Password.Text = user.Password;
            Email.Text = user.Email;
            Mobile.Text = user.Mobile;
            Zone.Text = user.Zone;
            City.Text = user.City;
            ShopName.Text = user.ShopName;

            img1.Source = ConvertStremToImage(user.Url1);

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
            if ((!string.IsNullOrEmpty(Username.Text)) && (!string.IsNullOrEmpty(Password.Text)))
            {
                user.Username = Username.Text;
                user.Password = Password.Text;
                user.Email = Email.Text;
                user.Mobile = Mobile.Text;
                user.Zone = Zone.Text;
                user.City = City.Text;
                user.ShopName = ShopName.Text;

                await App.DBSQLite.SaveUserAsync(user);
                await Navigation.PushAsync(new DisplayAllProductForUser(user));
            }
            else
                await DisplayAlert("Error", "Username is empty Or Username is already existe", "Ok");
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
                        CompressionQuality = 70
                    });

                    img1.Source = ImageSource.FromStream(() =>
                    {
                        var stream = photo.GetStream();
                        CurrentImageBase64 = GetBase64(photo.GetStream());
                        user.Url1 = CurrentImageBase64;
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
            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                array = memory.ToArray();
            }

            return Convert.ToBase64String(array);
        }
    }
}