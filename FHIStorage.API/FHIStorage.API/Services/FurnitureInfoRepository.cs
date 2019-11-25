﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FHIStorage.API.Entities;
using FHIStorage.API.Models;
using Microsoft.AspNetCore.Mvc;
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
                .Include(furn => furn.FurnitureImages)
                .Include(furn => furn.House);
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
            return _ctx.Furniture
                .Where(furn => (furn.HouseId == houseId))
                .Include(furn => furn.FurnitureImages)
                .Include(furn => furn.House);
        }

        public IEnumerable<Furniture> GetFurnitures()
        {
            return _ctx.Furniture
                .Include(furn => furn.FurnitureImages)
                .Include(furn => furn.House);
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

        public IEnumerable<FurnitureSet> GetFurnitureSetByFurnitureId(int furnitureId)
        {
            return _ctx.FurnitureSets.Where(f => f.FurnitureId == furnitureId);
        }

        public void AddFurnitureSet(FurnitureSet newFurnitureSet)
        {
            _ctx.FurnitureSets.Add(newFurnitureSet);
            _ctx.SaveChanges();
        }

        public void AssignFurnitureSet(int updateQuantity, int furnitureIdToCopy, int houseId)
        {
            SqlParameter prm1 = new SqlParameter("@FurnitureIdToCopy", furnitureIdToCopy);
            SqlParameter prm2 = new SqlParameter("@QuantityOfNewFurniture", updateQuantity);
            SqlParameter prm3 = new SqlParameter("@HouseIdToAssignTo", houseId);

            _ctx.Database.ExecuteSqlCommand($"EXEC dbo.AssignQuantityToFurnitureSet @FurnitureIdToCopy, @QuantityOfNewFurniture, @HouseIdToAssignTo", prm1, prm2, prm3);
        }
        public void UpdateQuantityTable(FurnitureSet updatedFurnitureSet)
        {
            _ctx.UpdateRange(updatedFurnitureSet);
            _ctx.SaveChanges();
        }

        public void DeleteFurnitureByFurnitureId(Furniture furnitureToDelete)
        {
            furnitureToDelete.HouseId = 0;
            furnitureToDelete.CategoryId = 0;

            _ctx.Furniture.Remove(furnitureToDelete);
            _ctx.SaveChanges();
        }
    }
}
