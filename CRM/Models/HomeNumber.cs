using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class HomeNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,StringLength(150)]
        public string Number { get; set; }

        public int SpeedId { get; set; }
        public virtual SpeedInternet SpeedInternet { get; set; }

        [Required,StringLength(300)]
        public string Borrow { get; set; }

        [Required]
        public DateTime CreateeDate { get; set; }

        [Required]
        public DateTime LastPayDate { get; set; }


    }
}
