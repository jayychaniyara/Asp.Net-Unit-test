using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.Models
{
    public class UserDetails
    {
        public string Salutation { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        public int? Age { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [RegularExpression(@"^[MF]+$", ErrorMessage = "Select any one option")]
        public char Gender { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "Please enter your valid email address")]
        public string Email { get; set; }

        [MinLength(10)]
        [MaxLength(15)]
        [Required(ErrorMessage = "Please enter your valid mobile number")]
        public string Phone { get; set; }

        //[Required(ErrorMessage = "Please agree to be contacted by Civica")]
        public bool IsAgreedToBeContacted { get; set; }
    }
}
