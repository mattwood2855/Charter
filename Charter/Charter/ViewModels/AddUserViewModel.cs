using Charter.Extensions;
using Charter.Models;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Charter.ViewModels
{
    /// <summary>
    /// View Model for add user activity
    /// </summary>
    public interface IAddUser
    {
        /// <summary>
        /// The user's avatar.
        /// </summary>
        byte[] Image { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// The user's phone number.
        /// </summary>
        string Phone { get; set; }

        /// <summary>
        /// The username for this account.
        /// </summary>
        string Username { get; set; }


        /// <summary>
        /// Allows the user to pick an avatar from their device.
        /// </summary>
        /// <returns></returns>
        Task<Stream> SelectAvatar();

        /// <summary>
        /// Attempts to save the user.
        /// </summary>
        /// <returns></returns>
        bool TrySave();
    }

    public class AddUserViewModel : IAddUser
    {
        public byte[] Image { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }

        public Stream ImageStream => new MemoryStream(Image);
        public IStorage Storage => CharterApp.Instance.Storage;        

        IEnumerable<string> GetViolations()
        {
            var violations = new List<string>();

            // Username check
            if (Storage.Users.Any(user => user.Username == Username))
                violations.Add("\u2022 That username is not available.");

            // If any characters are not a letter or digit
            if (Password.Any(character => !char.IsLetterOrDigit(character)))
                violations.Add("\u2022 Password can only have alphanumeric characters.");

            // If not any alpha characters
            if (!Password.Any(character => char.IsLetter(character)))
                violations.Add("\u2022 Password must have at least one letter.");

            // If not any alpha characters
            if (!Password.Any(character => char.IsNumber(character)))
                violations.Add("\u2022 Password must have at least one number.");

            // If not right length
            if (Password.Length < 5 || Password.Length > 12)
                violations.Add("\u2022 Password must be between 5 and 12 characters in length.");

            // If any repeating sequences
            if (Password.HasConsecutiveRepeatingSequences())
                violations.Add("\u2022 Password must not have any consecutive repeating sequences.");

            // Phone number length check
            if(Phone.Length != 10)
                violations.Add("\u2022 Phone number must be a 10 digit US phone number.");

            // Phone number validity
            if (Phone.Any(character=>!char.IsNumber(character)))
                violations.Add("\u2022 Phone number must only contain numbers.");

            return violations;
        }

        public async Task<Stream> SelectAvatar()
        {
            // Let the user pic a file
            using (var fileData = await CrossFilePicker.Current.PickFile(new[] { ".jpg", ".jpeg", ".png" }))
            {
                // If there was a problem
                if (fileData == null)
                    return null;

                // Set the image data
                Image = fileData.DataArray;

                // Return a new stream made from the byte array
                return ImageStream;
            }
        }

        public bool TrySave ()
        {
            // Get any violations in the form
            var violations = GetViolations();

            // If there are violations throw them
            if (violations.Any())
                throw new ValidationException(violations);

            // Create a new user
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Image = Image,
                Password = Password,
                Phone = Phone,
                Username = Username
            };

            // Save the avatar as a file on the device
            Storage.SaveAvatar(user.Id, Image);

            // Add the user to persistent storage
            Storage.AddUser(user);

            // return succes
            return true;
        }
    }
}
