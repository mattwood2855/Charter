using System.Collections.ObjectModel;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Charter.Android.Extensions;
using Charter.Android.Models;
using Charter.Extensions;
using Charter.Models;

namespace Charter.Android.Adapters
{
    public class UserRecyclerViewAdapter : ObservableCollectionRecyclerViewAdapter<User>
    {
        public UserRecyclerViewAdapter(Context context, ObservableCollection<User> users)
            : base(context, users)
        {
            items = users;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            // Get the item
            var user = items[position];

            // Sanity check
            if(!(holder is UserRecyclerViewHolder userViewHolder)) return;

            // Set the avatar
            if (user.Image != null)
                userViewHolder.Avatar.SetImageBitmap(user.Image.ToBitmap());
            else
                userViewHolder.Avatar.SetImageResource(Resource.Drawable.baseline_person_black_48dp);

            // Set the username
            userViewHolder.Username.Text = user.Username;

            // Set the password
            userViewHolder.Phone.Text = user.Phone.FormatPhoneNumber();

            userViewHolder.DeleteIcon.Click += (s, e) =>
            {
                CharterApp.Instance.Storage.DeleteUser(user);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Create the user list item view holder
            var viewHolder = inflater.Inflate(Resource.Layout.UserListItem, parent, false);
            return new UserRecyclerViewHolder(viewHolder);
        }
    }
}