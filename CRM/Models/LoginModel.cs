using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class LoginModel
    {
        //[EmailAddress]
        [Required, StringLength(100)]      
        public string UserEmail { get; set; }

        [Required, StringLength(100)]        
        public string Password { get; set; }

        public bool KeepMeLogged { get; set; }
    }
}
