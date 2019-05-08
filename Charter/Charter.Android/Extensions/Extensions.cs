using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Charter.Android.Extensions
{
    public static class Extensions
    {
        public static Bitmap ToBitmap(this byte[] image)
        {
            return BitmapFactory.DecodeByteArray(image, 0, image.Length);
        }
    }
}