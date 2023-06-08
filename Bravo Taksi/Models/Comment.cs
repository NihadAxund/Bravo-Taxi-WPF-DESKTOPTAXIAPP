using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravo_Taksi.Models
{
   public class Comment
   {
        public Driver Driver { get; set; }
        public Users Users { get; set; }
        public string Comment_text { get; set; }
        public Comment(){ }
        public Comment(Driver driver, Users user)
        {
            Users = user; Driver = driver; 
        }
        public Comment(Driver driver, Users user,string text)
        {
            Users = user; Driver = driver; Comment_text = text;
        }
    }
}
