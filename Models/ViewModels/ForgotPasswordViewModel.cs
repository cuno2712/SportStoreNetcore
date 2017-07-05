using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}