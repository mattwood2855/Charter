using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Charter.ViewModels;

using ImeAction = Android.Views.InputMethods.ImeAction;

namespace Charter.Android.Activities
{
    [Activity(Label = "AddUserActivity")]
    public class AddUserActivity : Activity
    {        
        public AddUserViewModel AddUserViewModel { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AddUserViewModel = new AddUserViewModel();

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddUser);

            var username = FindViewById<TextInputEditText>(Resource.Id.EtUsername);
            var password = FindViewById<TextInputEditText>(Resource.Id.EtPassword);
            var createButton = FindViewById<Button>(Resource.Id.BCreate);

            username.TextChanged += (s, e) =>
            {
                AddUserViewModel.Username = username.Text;
            };
            username.EditorAction += (s, e) =>
            {
                if (e.ActionId == ImeAction.Next)
                    password.RequestFocus();
            };
            
            password.TextChanged += (s, e) =>
            {
                AddUserViewModel.Password = password.Text;
            };
            password.EditorAction += (s, e) =>
            {
                if (e.ActionId == ImeAction.Done)
                    CreateUser();
            };

            createButton.Click += (s, e) => 
            {
                CreateUser();
            };
        }

        void CreateUser()
        {
            if (AddUserViewModel.TrySave())
                Finish();
        }
    }
}