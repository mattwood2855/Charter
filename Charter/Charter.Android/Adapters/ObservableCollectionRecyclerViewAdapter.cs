using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Charter.Android.Adapters
{
    public abstract class ObservableCollectionRecyclerViewAdapter<T> : RecyclerView.Adapter
    {
        protected readonly Context context;
        protected readonly LayoutInflater inflater;
        protected ObservableCollection<T> items;

        protected ObservableCollectionRecyclerViewAdapter(Context context, ObservableCollection<T> items)
        {
            this.context = context;
            this.items = items;

            inflater = LayoutInflater.From(context);
            items.CollectionChanged += OnCollectionChanged;
        }

        public override int ItemCount => items?.Count ?? 0;

        public abstract override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position);

        public abstract override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType);

        protected void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    NotifyItemInserted(e.NewStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    NotifyItemRemoved(e.OldStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    NotifyItemChanged(e.OldStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    NotifyItemMoved(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
            }
        }
    }
}