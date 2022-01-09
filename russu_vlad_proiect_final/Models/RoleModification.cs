using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace russu_vlad_proiect_final.Models
{
    public class RoleModification
    {
        public string RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
        
    }
}
