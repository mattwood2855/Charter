using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Charter.Models
{
    public interface IStorage
    {
        void AddUser(User user);
        void DeleteUser(User user);
    }

    public class Storage : IStorage
    {
        public ObservableCollection<User> Users { get; }

        public Storage()
        {
            Users = new ObservableCollection<User>();

            Users.Add(new User("Jimi Page", "123456"));
            Users.Add(new User("Bob Dylan", "123456"));
            Users.Add(new User("Alicia Keys", "123456"));
            Users.Add(new User("Bob Parker", "123456"));
            Users.Add(new User("Whitney Houston", "123456"));
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            Users.Remove(user);
        }
    }
}
