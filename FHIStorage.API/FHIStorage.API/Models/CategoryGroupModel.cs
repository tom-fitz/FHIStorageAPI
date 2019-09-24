using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FHIStorage.API.Models
{
    public class CategoryGroupModel
    {
        [Key]
        public int CategoryGroupId { get; set; }
        public string GroupType { get; set; }
    }
}
