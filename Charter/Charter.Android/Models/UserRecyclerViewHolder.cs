using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Charter.Android.Models
{
    public class UserRecyclerViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Avatar { get; private set; }
        public TextView Password { get; private set; }
        public TextView Username { get; private set; }

        public UserRecyclerViewHolder(View itemView) 
            : base(itemView)
        {
            Avatar = itemView.FindViewById<ImageView>(Resource.Id.IvAvatar);
            Password = itemView.FindViewById<TextView>(Resource.Id.TvPassword);
            Username = itemView.FindViewById<TextView>(Resource.Id.TvUsername);

            //itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }
}