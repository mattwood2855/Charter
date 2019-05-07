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
using Charter.Models;
using Charter.ViewModels;

using ImeAction = Android.Views.InputMethods.ImeAction;

namespace Charter.Android.Activities
{
    [Activity(Label = "AddUserActivity")]
    public class AddUserActivity : Activity
    {
        Button createButton;
        TextInputEditText password;
        TextInputEditText username;
        public AddUserViewModel AddUserViewModel { get; private set; }

        void BuildInteractions()
        {
            username = FindViewById<TextInputEditText>(Resource.Id.EtUsername);
            password = FindViewById<TextInputEditText>(Resource.Id.EtPassword);
            createButton = FindViewById<Button>(Resource.Id.BCreate);

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AddUserViewModel = new AddUserViewModel();

            SetContentView(Resource.Layout.AddUser);

            BuildInteractions();
        }

        void CreateUser()
        {
            try
            {
                AddUserViewModel.TrySave();
                Finish();
            }
            catch(PasswordException ex)
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Password Error");
                var message = string.Join(System.Environment.NewLine + System.Environment.NewLine, ex.Violations);
                alert.SetMessage(message);
                alert.SetPositiveButton("Ok", (s,e)=> 
                {
                    password.RequestFocus();
                    password.SelectAll();
                });
                var dialog = alert.Create();
                dialog.Show();
            }
            catch(Exception ex)
            {
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Error");
                alert.SetMessage("There was a problem creating the user.");
                var dialog = alert.Create();
                dialog.Show();
            }
                
        }
    }
}