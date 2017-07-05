using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class StackInfo
    {
        [Key]
        public string StackName { get; set; }

        public string Version { get; set; }
    }
}