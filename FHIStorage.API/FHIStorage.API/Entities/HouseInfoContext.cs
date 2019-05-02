using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FHIStorage.API.Entities
{
    public class HouseInfoContext : DbContext
    {
        public HouseInfoContext(DbContextOptions<HouseInfoContext> options) : base(options)
        {
            Database.Migrate();
        }
        public DbSet<House> Houses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<FurnitureImage> FurnitureImages { get; set; }
    }
}
