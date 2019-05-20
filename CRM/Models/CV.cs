using CRM.Models.InnerModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class CV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Fullname { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(500)]
        public string CV_Name { get; set; }

        [NotMapped]
        public IFormFile CV_File { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
