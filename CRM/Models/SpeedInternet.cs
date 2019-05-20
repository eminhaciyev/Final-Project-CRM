using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class SpeedInternet
    {
        public SpeedInternet()
        {
            Packages = new HashSet<Package>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,StringLength(300)]
        public string Speed { get; set; }

        public virtual ICollection<Package> Packages { get; set; }
    }
}
