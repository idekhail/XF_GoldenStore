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
    public partial class DisplayOneProductInGalleryPage : CarouselPage
    {
        private Products product;

        public DisplayOneProductInGalleryPage(Products product)
        {
            InitializeComponent();
            this.product = product;
            
            img1.Source = ImageSource.FromStream(() =>
            {
                var stream = GetStream(product.Url1);
                return stream;
            });

            img2.Source = ImageSource.FromStream(() =>
            {
                var stream = GetStream(product.Url2);
                return stream;
            });

            img3.Source = ImageSource.FromStream(() =>
            {
                var stream = GetStream(product.Url3);
                return stream;
            });

            img4.Source = ImageSource.FromStream(() =>
            {
                var stream = GetStream(product.Url4);
                return stream;
            });
        }

        public Stream GetStream(string base64)
        {
            byte[] array = Convert.FromBase64String(base64);
            return new MemoryStream(array);
        }

    }
}