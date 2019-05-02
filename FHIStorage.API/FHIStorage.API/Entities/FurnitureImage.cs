using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FHIStorage.API.Entities
{
    public class FurnitureImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FurnitureImageId { get; set; }
        [Required]
        [ForeignKey("FurnitureId")]
        public int FurnitureId { get; set; }
        [Required]
        public string PictureInfo { get; set; }
    }
}
