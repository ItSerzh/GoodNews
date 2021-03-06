﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAnalyzer.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Field Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Field Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    }
}
