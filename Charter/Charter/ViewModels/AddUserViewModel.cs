using Charter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<(string, string)> Rules => new List<(string, string)>()
        {
            ("/^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$/", "\u2022 Password must be alphanumeric and have at least one number and one letter."), // Checks at least one number, one letter, only alphanumeric chars
            ("/^[a-zA-Z0-9]{5,12}", "\u2022 Password must be between 5 and 12 characters") // Checks the lenth is between 5 and 12
        };
        public IStorage Storage = CharterApp.Instance.Storage;

        public bool TrySave ()
        {
            var violations = new List<string>();

            foreach (var rule in Rules)
            {
                if (!Regex.Match(Password, rule.Item1).Success)
                    violations.Add(rule.Item2);                    
            }

            if(violations.Any())
                throw new PasswordException(violations);

            Storage.AddUser(new User(Username, Password));

            return true;
        }
    }
}
