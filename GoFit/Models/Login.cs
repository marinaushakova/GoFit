﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoFit.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username reqired", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password reqired", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}