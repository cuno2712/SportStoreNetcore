
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class StackInfo
    {
        [Key]
        public string StackName { get; set; }
        public string Version { get; set; }
    }
}
