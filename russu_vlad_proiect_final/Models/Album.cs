using System;
using System.Collections.Generic;
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
        public decimal Price { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
