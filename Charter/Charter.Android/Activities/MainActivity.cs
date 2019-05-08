using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using Charter.ViewModels;
using Charter.Android.Adapters;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using ViewStates = Android.Views.ViewStates;
using System.Linq;
using Android.Views;

namespace Charter.Android.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public MainViewModel MainViewModel { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MainViewModel = new MainViewModel();

            SetContentView(Resource.Layout.Main);

            SetupToolbar();

            // Set empty state and listen for updates
            var tvEmpty = FindViewById<TextView>(Resource.Id.TvEmpty);
            tvEmpty.Visibility = MainViewModel.Users.Any() ? ViewStates.Gone : ViewStates.Visible;
            MainViewModel.Users.CollectionChanged += (s, e) =>
            {
                tvEmpty.Visibility = MainViewModel.Users.Any() ? ViewStates.Gone : ViewStates.Visible;

                Toast.MakeText(this, e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add ? "User successfully added." : "User successfully removed.", ToastLength.Short).Show();
            };

            // Get the recycler view
            var recyclerView = FindViewById<RecyclerView>(Resource.Id.RvUsers);

            // Set the layout to linear
            recyclerView.SetLayoutManager(new LinearLayoutManager(ApplicationContext));

            // Create and set the adapter
            var userAdapter = new UserRecyclerViewAdapter(ApplicationContext, MainViewModel.Users);
            recyclerView.SetAdapter(userAdapter);
        }

        void SetupToolbar()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            
            SupportActionBar.Title = "Users";           
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.MiAdd:
                    StartActivity(typeof(AddUserActivity));
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

