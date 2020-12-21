using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class RegisterModel
    {
<<<<<<< HEAD
        public string Email { get; set; }
=======
        [Required]
        public string Email { get; set; }
        
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9

        [Required]
        public string UserName { get; set; }

<<<<<<< HEAD
        public void EmailIsUserName()
        {
            Email = UserName;
        }

        [Required]
        public string Name { get; set; }

=======
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}