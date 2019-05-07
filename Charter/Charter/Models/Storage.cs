using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    }

    public class Storage : IStorage
    {
        public ObservableCollection<User> Users { get; private set; }

        public string StoragePath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);
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
