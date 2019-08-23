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
    public interface IImageInfoRepository
    {
        void AddNewFurnitureImage(FurnitureImage newImage);
        Task<string> SaveImage(Stream imageStream);
        string UriFor(string imageId);
        string GetImageByImageId(int imageId);
        FurnitureImage GetImageByFurnitureId(int furnitureId);
        void DeleteImage(string guid, FurnitureImage furnImageToDelete);
    }
}
