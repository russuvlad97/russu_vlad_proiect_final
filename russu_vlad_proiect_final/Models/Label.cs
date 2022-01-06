using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace russu_vlad_proiect_final.Models
{
    public class Label
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Label Name")]
        [StringLength(50)]
        public string LabelName { get; set; }

        [StringLength(70)]
        public string Address { get; set; }
        public ICollection<ReleasedAlbum> ReleasedAlbum { get; set; }
    }
}
