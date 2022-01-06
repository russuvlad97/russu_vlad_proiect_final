using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace russu_vlad_proiect_final.Models
{
    public class Album
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public DateTime ReleaseDate { get; set; }

        [Column(TypeName = "decimal(6, 2")]
        public decimal Price { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<ReleasedAlbum> ReleasedAlbums { get; set; }
    }
}
