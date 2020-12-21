using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecordStore.Models
{
    public class LoginModel
    {
        [Required]
<<<<<<< HEAD
        public string UserName { get; set; }
=======
        public string Email { get; set; }
>>>>>>> 406e379bced60430c121c4509d9b369056c208c9
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}