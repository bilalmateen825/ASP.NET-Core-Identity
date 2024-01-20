using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Identity.PageViewModels
{
    public class UserRegistrationViewModel
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]        
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Designation { get; set; }
    }
}
