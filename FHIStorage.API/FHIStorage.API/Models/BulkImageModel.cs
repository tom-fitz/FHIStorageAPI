using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FHIStorage.API.Models
{
    public class BulkImageModel
    {
        public string FurnitureId { get; set; }
        public IFormFile image { get; set; }
    }
}
