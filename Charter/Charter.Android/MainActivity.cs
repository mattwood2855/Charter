using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using Charter.ViewModels;
using Charter.Android.Adapters;

namespace Charter.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public MainViewModel MainViewModel { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = "Users";
            SetContentView(Resource.Layout.activity_main);

            // Get the recycler view
            var list = FindViewById<RecyclerView>(Resource.Id.RvUsers);

            // Create and set the adapter
            var userAdapter = new UserRecyclerViewAdapter(ApplicationContext, MainViewModel.Users);
            list.SetAdapter(userAdapter);

            
        }
    }
}