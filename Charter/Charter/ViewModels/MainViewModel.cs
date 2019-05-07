using Charter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Charter.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<User> Users { get; }

        public MainViewModel()
        {
            Users = new ObservableCollection<User>();

            Users.Add(new User("Jimi Page", "123456"));
            Users.Add(new User("Bob Dylan", "123456"));
            Users.Add(new User("Alicia Keys", "123456"));
            Users.Add(new User("Bob Parker", "123456"));
            Users.Add(new User("Whitney Houston", "123456"));
        }
    }
}
