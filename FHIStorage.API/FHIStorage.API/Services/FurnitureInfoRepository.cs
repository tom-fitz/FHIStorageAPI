using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FHIStorage.API.Entities;
using FHIStorage.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.WindowsAzure.Storage.Blob;
using Remotion.Linq.Clauses;

namespace FHIStorage.API.Services
{
    public class FurnitureInfoRepository : IFurnitureInfoRepository
    {
        private HouseInfoContext _ctx;
        public FurnitureInfoRepository(HouseInfoContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _ctx.Categories.OrderBy(c => c.CategoryId).ToList();
        }

        public IEnumerable<Furniture> GetFurnitureByCategoryId(int categoryId)
        {
            return _ctx.Furniture
                .Where(furn => (furn.CategoryId == categoryId))
                .GroupJoin(
                    _ctx.FurnitureImages,
                    furn => (Int32?)(furn.FurnitureId),
                    furnImage => furnImage.FurnitureId,
                    (furn, imageGroup) =>
                        new Furniture
                        {
                            FurnitureId = furn.FurnitureId,
                            Name = furn.Name,
                            UID = furn.UID,
                            CategoryId = furn.CategoryId,
                            Cost = furn.Cost,
                            PurchasedFrom = furn.PurchasedFrom,
                            DatePurchased = furn.DatePurchased,
                            HouseId = furn.HouseId,
                            Turns = furn.Turns,
                            FurnitureImages = imageGroup.ToList(),
                            FurnitureImageId = furn.FurnitureImageId
                            //Address = house.Address
                        }
                );
        }

        public IQueryable<Furniture> GetFurnitureByFurnitureId(int furnitureId)
        {
            return _ctx.Furniture
                .Where(furn => (furn.FurnitureId == furnitureId))
                .Include(furn => furn.FurnitureImages)
                .Include(furn => furn.House);
        }

        public IEnumerable<Furniture> GetFurnitureByHouseId(int houseId)
        {
            return from furn in _ctx.Furniture
                   where furn.HouseId == houseId
                   join furnImage in _ctx.FurnitureImages on furn.FurnitureId equals furnImage.FurnitureId into imageGroup
                   select new Furniture
                   {
                       FurnitureId = furn.FurnitureId,
                       Name = furn.Name,
                       UID = furn.UID,
                       CategoryId = furn.CategoryId,
                       Cost = furn.Cost,
                       PurchasedFrom = furn.PurchasedFrom,
                       DatePurchased = furn.DatePurchased,
                       HouseId = furn.HouseId,
                       Turns = furn.Turns,
                       FurnitureImageId = furn.FurnitureImageId,
                       FurnitureImages = imageGroup.ToList()
                   };
        }

        public IEnumerable<Furniture> GetFurnitures()
        {
            return from furn in _ctx.Furniture
                   join furnImage in _ctx.FurnitureImages on furn.FurnitureId equals furnImage.FurnitureId into imageGroup
                   select new Furniture
                   {
                       FurnitureId = furn.FurnitureId,
                       Name = furn.Name,
                       UID = furn.UID,
                       CategoryId = furn.CategoryId,
                       Cost = furn.Cost,
                       PurchasedFrom = furn.PurchasedFrom,
                       DatePurchased = furn.DatePurchased,
                       HouseId = furn.HouseId,
                       Turns = furn.Turns,
                       FurnitureImageId = furn.FurnitureImageId,
                       FurnitureImages = imageGroup.ToList()
                   };
        }
        public void AddNewFurniture(Furniture newFurniture)
        {
            _ctx.Furniture.Add(newFurniture);
            _ctx.SaveChanges();
        }

        public bool FurnitureExists(int furnitureId)
        {
            return _ctx.Furniture.Any(f => f.FurnitureId == furnitureId);
        }
        public void updateFurnitureByFurnitureId(Furniture newFurniture)
        {
            _ctx.UpdateRange(newFurniture);
            _ctx.SaveChanges();
        }

        public void DeleteFurnitureByFurnitureId(Furniture furnitureToDelete)
        {
            furnitureToDelete.HouseId = 0;
            furnitureToDelete.CategoryId = 0;

            _ctx.Furniture.Remove(furnitureToDelete);
            _ctx.SaveChangesAsync();
        }
    }
}
