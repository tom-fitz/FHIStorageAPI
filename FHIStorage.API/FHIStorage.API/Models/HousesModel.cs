using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FHIStorage.API.Models
{
    public class HousesModel
    {
        public int id { get; set; }
        [Required]
        public string address { get; set; }
        public int zipcode { get; set; }
        [Required]
        public Decimal cost { get; set; }
        [Required]
        public DateTime contractDate { get; set; }
        public DateTime? dateSold { get; set; }
        public bool sold { get; set; }
    }
}
