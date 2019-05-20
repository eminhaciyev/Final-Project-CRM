using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class MobileNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Number { get; set; }

        public int PackageId { get; set; }
        public virtual Package Package { get; set; }

        public DateTime PackageAddDate { get; set; }

        

    }
}
