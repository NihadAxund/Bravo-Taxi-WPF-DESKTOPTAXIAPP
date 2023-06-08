using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravo_Taksi.Models
{
    public class Users
    {
        public Users(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            History = new List<History>();
        }
        public List<History> History { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


    }
}
