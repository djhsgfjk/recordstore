﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        public void EmailIsUserName()
        {
            Email = UserName;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}