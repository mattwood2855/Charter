using Charter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Charter.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<User> Users => CharterApp.Instance.Storage.Users;

        public MainViewModel()
        {            
        }
    }
}
