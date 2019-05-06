using System;
using System.Collections.Generic;
using System.Text;

namespace Charter.ViewModels
{
    public interface INewUser
    {
        string Username { get; set; }
        string Password { get; set; }

        void TrySave();
    }

    public class NewUserViewModel : INewUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public IEnumerable<string> Rules => new List<string>()
        {
            "/^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$/", // Checks at least one number, one letter, only alphanumeric chars
            "/^[a-zA-Z0-9]{5,12}", // Checks the lenth is between 5 and 12
        };

        public void TrySave ()
        {

        }
    }
}
