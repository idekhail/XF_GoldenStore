using System;
using System.Collections.Generic;
using System.Text;


using SQLite;

using Xamarin.Forms;

namespace XF_GoldenStore.Model
{
    public class ImgUrl
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Image Img { get; set; }
    }
}
