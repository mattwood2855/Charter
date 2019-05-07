using Charter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Charter
{
    public class CharterApp
    {
        public static CharterApp Instance { get; } = new CharterApp();

        public IStorage Storage { get; }

        CharterApp()
        {
            Storage = new Storage();
        }
    }
}
