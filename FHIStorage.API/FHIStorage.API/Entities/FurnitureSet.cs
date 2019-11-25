using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FHIStorage.API.Entities
{
    public class FurnitureSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FurnitureSetId { get; set; }
        public int FurnitureId { get; set; }
        public int Quantity { get; set; }

    }
}
