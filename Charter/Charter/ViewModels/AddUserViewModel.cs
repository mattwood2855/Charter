using Charter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Charter.ViewModels
{
    public interface INewUser
    {
        string Username { get; set; }
        string Password { get; set; }

        bool TrySave();
    }

    public class AddUserViewModel : INewUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public IEnumerable<string> Rules => new List<string>()
        {
            "/^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$/", // Checks at least one number, one letter, only alphanumeric chars
            "/^[a-zA-Z0-9]{5,12}", // Checks the lenth is between 5 and 12
        };
        public IStorage Storage = CharterApp.Instance.Storage;

        public bool TrySave ()
        {
            //foreach(var rule in Rules)
            //{
            //    if (!Regex.Match(Password, rule).Success)
            //        throw new Exception(rule);
            //}


            Storage.AddUser(new User(Username, Password));

            return true;
        }
    }
}
