using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace russu_vlad_proiect_final.Models.RecordStoreViewModels
{
    public class LabelIndexData
    {
        public IEnumerable<Label> Labels { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
