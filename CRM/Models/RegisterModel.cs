using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class RegisterModel
    {
        [Required, StringLength(50)]        
        public string FullName { get; set; }

        [Required, StringLength(100)]        
        //[EmailAddress]
        public string UserEmail { get; set; }

        [Required, StringLength(100)]        
        public string Password { get; set; }

        
        public DateTime BirthDate { get; set; }

        [Required, StringLength(100)]        
        [Compare(nameof(Password))]
        public string ComfirmPassword { get; set; }
    }
}
