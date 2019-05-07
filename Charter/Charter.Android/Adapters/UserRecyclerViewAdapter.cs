using System.Collections.ObjectModel;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Charter.Android.Models;
using Charter.Models;

namespace Charter.Android.Adapters
{
    public class UserRecyclerViewAdapter : ObservableCollectionRecyclerViewAdapter<User>
    {
        public UserRecyclerViewAdapter(Context context, ObservableCollection<User> users)
            : base(context, users)
        {

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new UserRecyclerViewHolder(inflater.Inflate(Resource.Layout.UserListItem, parent, false));
        }
    }
}