using FHIStorage.API.Entities;
using FHIStorage.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FHIStorage.API.Services
{
    public interface IFurnitureInfoRepository
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Furniture> GetFurnitures();
        IQueryable<Furniture> GetFurnitureByFurnitureId(int furnitureId);
        IEnumerable<Furniture> GetFurnitureByHouseId(int houseId);
        IEnumerable<Furniture> GetFurnitureByCategoryId(int categoryId);
        //Task<string> SaveImage(Stream imageStream);
        void AddNewFurniture(Furniture finalFurniture);
        void AddFurnitureSet(FurnitureSet newFurnitureSet);
        void updateFurnitureByFurnitureId(Furniture newFurniture);
        void DeleteFurnitureByFurnitureId(Furniture furnitureToDelete);
        bool FurnitureExists(int furnitureId);
        void UpdateQuantityTable(FurnitureSet newFurnitureSet);
        IEnumerable<FurnitureSet> GetFurnitureSetByFurnitureId(int furnitureId);
        void AssignFurnitureSet(int updateQuantity, int furnitureIdToCopy, int houseId);
        void AssignFurnitureSetBackToWarehouse(Furniture updateFurnitureSet);
    }
}
