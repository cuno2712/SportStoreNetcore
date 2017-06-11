using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class User
    {
        public int Id { get; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public DateTime? BirthDay { get; set; }

        public string Job { get; set; }

    }
}
