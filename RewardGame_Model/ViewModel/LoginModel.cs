﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardGame_Model.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\S+@\S+\.\S+$", ErrorMessage = "Please enter valid Email Id")]
        [MaxLength(50, ErrorMessage = "Email id length should be 50 characters")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "password must contain at least one number , one Uppercase, one Lowercase and one special character.")]
        [MaxLength(30, ErrorMessage = "password maximum length should be 30 characters.")]
        [MinLength(8, ErrorMessage = "password minimum length should be 8 characters.")]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
