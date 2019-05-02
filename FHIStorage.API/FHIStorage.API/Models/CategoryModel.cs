using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FHIStorage.API.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
    }
}
