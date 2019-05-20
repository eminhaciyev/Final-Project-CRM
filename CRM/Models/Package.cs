using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Package
    {
        public Package()
        {
            Numbers = new HashSet<MobileNumber>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageId { get; set; }

        [Required,StringLength(150)]
        public string PackageName { get; set; }

        [StringLength(150)]
        public string PackageName_Az { get; set; }

        [StringLength(150)]
        public string PackageName_Ru { get; set; }

        public int SpeedId { get; set; }
        public virtual SpeedInternet Speed { get; set; }

        [StringLength(100)]
        public string Time { get; set; }

        public virtual ICollection<MobileNumber> Numbers { get; set; }

    }
}
