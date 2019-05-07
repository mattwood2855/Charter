using Charter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Charter
{
    public class CharterApp
    {
        public static CharterApp Instance { get; } = new CharterApp();

        public Storage Storage { get; }

        CharterApp()
        {
            Storage = new Storage();
        }
    }
}
