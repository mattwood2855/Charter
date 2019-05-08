using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Charter.Models
{
    public class User
    {
        public string Id { get; set; }
        [JsonIgnore]
        public byte[] Image { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
    }
}
