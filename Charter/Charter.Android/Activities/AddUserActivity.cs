using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Charter.Android.Extensions;
using Charter.Models;
using Charter.ViewModels;
using Java.IO;
using Refractored.Controls;
using ImeAction = Android.Views.InputMethods.ImeAction;

namespace Charter.Android.Activities
{
    [Activity(Label = "AddUserActivity")]
    public class AddUserActivity : Activity
    {
        CircleImageView avatar;
        Button createButton;
        TextInputEditText password;
        TextInputEditText phone;
        TextInputEditText username;
        public IAddUser AddUserViewModel { get; private set; }

        void BuildInteractions()
        {
            avatar = FindViewById<CircleImageView>(Resource.Id.CivAvatar);
            createButton = FindViewById<Button>(Resource.Id.BCreate);
            password = FindViewById<TextInputEditText>(Resource.Id.EtPassword);
            phone = FindViewById<TextInputEditText>(Resource.Id.EtPhone);
            username = FindViewById<TextInputEditText>(Resource.Id.EtUsername);
            

            avatar.Click += async (s, e) =>
            {
                // Get the avatar image stream
                var stream = await AddUserViewModel.SelectAvatar();
                if (stream != null)
                {
                    // Create a bitmap
                    var sourceBitmap = AddUserViewModel.Image.ToBitmap();

                    // Check the rotation of the image
                    if (stream.CanSeek)
                        stream.Seek(0, System.IO.SeekOrigin.Begin);
                    var exif = new ExifInterface(stream);
                    var orientation = exif.GetAttributeInt(ExifInterface.TagOrientation, 1);

                    // Rotate the image if needed
                    var matrix = new Matrix();
                    Bitmap rotatedBitmap = null;
                    switch (orientation)
                    {
                        case 3:
                            matrix.PostRotate(90);
                            rotatedBitmap = Bitmap.CreateBitmap(sourceBitmap, 0, 0, sourceBitmap.Width, sourceBitmap.Height, matrix, true);
                            break;
                        case 6:
                            matrix.PostRotate(90);
                            rotatedBitmap = Bitmap.CreateBitmap(sourceBitmap, 0, 0, sourceBitmap.Width, sourceBitmap.Height, matrix, true);
                            break;
                        case 8:
                            matrix.PostRotate(270);
                            rotatedBitmap = Bitmap.CreateBitmap(sourceBitmap, 0, 0, sourceBitmap.Width, sourceBitmap.Height, matrix, true);
                            break;
                    }

                    // If it needs to be rotated
                    if (rotatedBitmap != null)
                    {
                        using (var rotatedStream = new MemoryStream())
                        {
                            rotatedBitmap.Compress(Bitmap.CompressFormat.Png, 90, rotatedStream);
                            AddUserViewModel.Image = rotatedStream.ToArray();
                        }
                    }

                    // Set the image in the UI
                    avatar.SetImageBitmap(rotatedBitmap != null ? rotatedBitmap : sourceBitmap);
                }
            };

            username.TextChanged += (s, e) =>
            {
                AddUserViewModel.Username = username.Text;
            };
            username.EditorAction += (s, e) =>
            {
                if (e.ActionId == ImeAction.Next)
                    phone.RequestFocus();
            };

            phone.TextChanged += (s, e) =>
            {
                AddUserViewModel.Phone = phone.Text;
            };
            phone.EditorAction += (s, e) =>
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
            catch(ValidationException ex)
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
                alert.SetPositiveButton("Ok", (s, e) =>
                {
                    // Do nothing
                });
                var dialog = alert.Create();
                dialog.Show();
            }
        }
    }
}