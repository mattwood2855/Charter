using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Charter.Models
{
    public interface IStorage
    {
        /// <summary>
        /// The users.
        /// </summary>
        ObservableCollection<User> Users { get; }

        /// <summary>
        /// Add a user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        void AddUser(User user);

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        void DeleteUser(User user);

        /// <summary>
        /// Save the avatar as a local file.
        /// </summary>
        /// <param name="fileName">The name.</param>
        /// <param name="data">The image file.</param>
        void SaveAvatar(string fileName, byte[] data);
    }

    public class Storage : IStorage
    {
        public ObservableCollection<User> Users { get; private set; }

        public string AvatarFile(string userId) => Path.Combine(StoragePath, userId + ".avatar");
        public string StoragePath => Xamarin.Essentials.FileSystem.AppDataDirectory;
        public string UserFile => Path.Combine(StoragePath, "users.txt");

        public Storage()
        {
            // Load the users from storage
            LoadUsers();

            // Watch for changes
            Users.CollectionChanged += SaveUsers;
        }

        /// <summary>
        /// Add a user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public void AddUser(User user)
        {
            Users.Add(user);
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        public void DeleteUser(User user)
        {
            Users.Remove(user);
        }

        /// <summary>
        /// Loads the persisted users
        /// </summary>
        void LoadUsers()
        {
            // If the file exists
            if (File.Exists(UserFile))
            {
                // Open and read it
                var userText = File.ReadAllText(UserFile);

                // Deserialize the data
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(userText);

                // If there are users
                if (users != null)
                {
                    // Go through each user
                    foreach (var user in users)
                    {
                        // Get the theoretical path to the avatar
                        var imagePath = AvatarFile(user.Id);

                        // If we stored an avatar for them
                        try
                        {
                            //Load it
                            user.Image = File.ReadAllBytes(imagePath);
                        }
                        catch
                        {
                            Debug.WriteLine($"Could not find user avatar: {imagePath}");
                        }
                    }
                }

                // Initialize the collection
                Users = new ObservableCollection<User>(users ?? new List<User>());
            }
            else // The file does not exist
            {
                // Create it
                File.Create(UserFile);

                // Initialize the collection
                Users = new ObservableCollection<User>();
            }
        }

        /// <summary>
        /// Save the avatar as a local file.
        /// </summary>
        /// <param name="fileName">The name.</param>
        /// <param name="data">The image file.</param>
        public void SaveAvatar(string userId, byte[] data)
        {
            try
            {
                File.WriteAllBytes(AvatarFile(userId), data);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Could not save avatar.");
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Saves the users to the device.
        /// </summary>
        /// <param name="s">The sender.</param>
        /// <param name="e">Event params.</param>
        void SaveUsers(object s, EventArgs e)
        {
            // Get the users as a string
            var userText = JsonConvert.SerializeObject(Users);

            // Write it to the file
            File.WriteAllText(UserFile, userText);
        }
    }
}
