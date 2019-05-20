using CRM.Models.InnerModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,StringLength(100)]
        public string Fullname { get; set; }

        [Required]
        public string EmailOrNumber { get; set; }

        [Required, StringLength(50)]
        public string Subject { get; set; }

        [Required, StringLength(1000)]
        public string Message { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
