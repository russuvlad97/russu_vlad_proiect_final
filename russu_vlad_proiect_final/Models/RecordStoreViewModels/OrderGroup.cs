using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace russu_vlad_proiect_final.Models.RecordStoreViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int AlbumCount { get; set; }
    }
}
