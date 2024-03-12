using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema1_dictionar
{
    public class Admin
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // Constructor
        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

}
