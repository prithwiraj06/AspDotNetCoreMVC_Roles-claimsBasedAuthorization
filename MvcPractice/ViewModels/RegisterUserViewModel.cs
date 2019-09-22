using Microsoft.AspNetCore.Mvc;
using MvcPractice.CustomValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPractice.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller:"Accounts")]
        [ValidEmailDomain(allowedDomain: "pragimtech.com", ErrorMessage = "Email Domain can be pragimtech.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}
