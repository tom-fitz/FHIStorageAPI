using FHIStorage.API.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FHIStorage.API.Models
{
    public class FurnitureModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required] [MaxLength(50)] public string Name { get; set; }
        [Required] public string UID { get; set; }
        [ForeignKey("CategoryId")]
        [Required] public int CategoryId { get; set; }
        [Required] public Decimal? Cost { get; set; }
        public string PurchasedFrom { get; set; }
        public DateTime? DatePurchased { get; set; }
        [ForeignKey("HouseId")]
        [Required] public int HouseId { get; set; }
        [Required] public int Turns { get; set; }
        public List<FurnitureImage> FurnitureImages { get; set; }
        [ForeignKey("FurnitureImageId")]
        public int? FurnitureImageId { get; set; }
    }
}
