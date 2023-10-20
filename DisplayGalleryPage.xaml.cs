using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF_GoldenStore
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayGalleryPage : CarouselPage
    {
        public DisplayGalleryPage()
        {
            InitializeComponent();

        }
    }
}