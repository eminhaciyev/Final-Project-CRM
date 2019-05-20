using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.InnerModel
{
    public class User:IdentityUser
    {
        public User()
        {
            CVs = new HashSet<CV>();
            Complaints = new HashSet<Complaint>();
            UserBalances = new HashSet<UserBalance>();
            Feedbacks = new HashSet<Feedbacks>();
            Contacts = new HashSet<Contact>();
        }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [StringLength(300)]
        public string Image { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public virtual ICollection<CV> CVs { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<UserBalance> UserBalances { get; set; }
        public virtual ICollection<Feedbacks> Feedbacks { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }


    }
}
