using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FHIStorage.API.Entities;
using FHIStorage.API.Models;
using FHIStorage.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FHIStorage.API.Controllers
{
    [Route("api")]
    //[ApiController]
    public class FurnitureController : Controller
    {
        private IFurnitureInfoRepository _furnitureInfoRepository;

        public FurnitureController(IFurnitureInfoRepository furnitureInfoRepository)
        {
            _furnitureInfoRepository = furnitureInfoRepository;
        }

        [HttpGet("furniture/{furnitureId}", Name = "GetFurnitureByFurnitureId")]
        public IActionResult GetFurnitureByFurnitureId(int furnitureId)
        {
            var singleFurniture = _furnitureInfoRepository.GetFurnitureByFurnitureId(furnitureId);

            if (singleFurniture == null)
            {
                return NotFound();
            }

            var result = new List<Furniture>();

            foreach (var x in singleFurniture)
            {
                result.Add(new Furniture()
                {
                    FurnitureId = x.FurnitureId,
                    Name = x.Name,
                    UID = x.UID,
                    CategoryId = x.CategoryId,
                    Cost = x.Cost,
                    DatePurchased = x.DatePurchased,
                    PurchasedFrom = x.PurchasedFrom,
                    HouseId = x.HouseId,
                    Turns = x.Turns,
                    FurnitureImageId = x.FurnitureImageId,
                    FurnitureImages = x.FurnitureImages.ToList()
                });
            }

            return Ok(result);
        }
        [HttpGet("houses/{houseId}/furniture")]
        public IActionResult GetFurnitureByHouseId(int houseID)
        {
            var furnitureInHouse = _furnitureInfoRepository.GetFurnitureByHouseId(houseID);

            if (furnitureInHouse == null)
            {
                return NotFound();
            }

            var results = new List<FurnitureModel>();

            foreach (var f in furnitureInHouse)
            {
                results.Add(new FurnitureModel
                {
                    Id = f.FurnitureId,
                    Name = f.Name,
                    UID = f.UID,
                    CategoryId = f.CategoryId,
                    Cost = Convert.ToDecimal(f.Cost),
                    PurchasedFrom = f.PurchasedFrom,
                    DatePurchased = Convert.ToDateTime(f.DatePurchased),
                    HouseId = f.HouseId,
                    Turns = f.Turns,
                    FurnitureImages = f.FurnitureImages.ToList()
                });
            }

            return Ok(results);
        }
        [HttpGet("categories/{categoryId}/furniture")]
        public IActionResult GetFurnitureByCategoryId(int categoryId)
        {
            var furnitureInCategory = _furnitureInfoRepository.GetFurnitureByCategoryId(categoryId);

            if (furnitureInCategory == null)
            {
                return NotFound();
            }

            var results = new List<Furniture>();

            foreach (var f in furnitureInCategory)
            {
                results.Add(new Furniture
                {
                    FurnitureId = f.FurnitureId,
                    Name = f.Name,
                    UID = f.UID,
                    CategoryId = f.CategoryId,
                    Cost = Convert.ToDecimal(f.Cost),
                    PurchasedFrom = f.PurchasedFrom,
                    DatePurchased = Convert.ToDateTime(f.DatePurchased),
                    HouseId = f.HouseId,
                    Turns = f.Turns,
                    FurnitureImages = f.FurnitureImages.ToList()
                });
            }

            return Ok(results);
        }

        [HttpGet("furniture", Name = "GetFurniture")]
        public IActionResult GetAllFurniture()
        {
            var allFurniture = _furnitureInfoRepository.GetFurnitures();

            if (allFurniture == null)
            {
                return NotFound();
            }

            var results = new List<Furniture>();

            foreach (var f in allFurniture)
            {
                results.Add(new Furniture
                {
                    FurnitureId = f.FurnitureId,
                    Name = f.Name,
                    UID = f.UID,
                    CategoryId = f.CategoryId,
                    Cost = Convert.ToDecimal(f.Cost),
                    PurchasedFrom = f.PurchasedFrom,
                    DatePurchased = Convert.ToDateTime(f.DatePurchased),
                    HouseId = f.HouseId,
                    Turns = f.Turns,
                    FurnitureImages = f.FurnitureImages.ToList()
                });
            }

            return Ok(results);
        }

        [HttpGet("categories")]
        public IActionResult GetAllCategories()
        {
            var allCategories = _furnitureInfoRepository.GetCategories();

            if (allCategories == null)
            {
                return NotFound();
            }

            var results = new List<CategoryModel>();

            foreach (var c in allCategories)
            {
                results.Add(new CategoryModel
                {
                    Id = c.CategoryId,
                    Type = c.Type
                });
            }

            return Ok(results);
        }
        [HttpPost("furniture")]
        public IActionResult AddNewFurniture([FromBody] Furniture newFurniture)
        {
            if (newFurniture == null)
            {
                return BadRequest();
            }

            var finalFurniture = new Furniture()
            {
                Name = newFurniture.Name,
                UID = newFurniture.UID,
                CategoryId = newFurniture.CategoryId,
                Cost = newFurniture.Cost,
                PurchasedFrom = newFurniture.PurchasedFrom,
                DatePurchased = newFurniture.DatePurchased,
                HouseId = newFurniture.HouseId,
                Turns = newFurniture.Turns
            };

            _furnitureInfoRepository.AddNewFurniture(finalFurniture);

            //return Ok();
            return CreatedAtRoute("GetFurnitureByFurnitureId", new { furnitureId = finalFurniture.FurnitureId }, finalFurniture);
        }

        [HttpPut("furniture/{id}")]
        public IActionResult UpdateFurnitureById([FromBody] Furniture newFurniture)
        {
            if (newFurniture == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_furnitureInfoRepository.FurnitureExists(newFurniture.FurnitureId))
            {
                return NotFound();
            }

            _furnitureInfoRepository.updateFurnitureByFurnitureId(newFurniture);

            return Ok(newFurniture);
        }
        [HttpDelete("furniture/{id}")]
        public IActionResult DeleteFurnitureByFurnitureId(int id)
        {
            var furnitureToDelete = _furnitureInfoRepository.GetFurnitureByFurnitureId(id);

            var itemToDelete = furnitureToDelete.First(f => f.FurnitureId == id);

            if (itemToDelete == null)
            {
                NotFound();
            }

            _furnitureInfoRepository.DeleteFurnitureByFurnitureId(itemToDelete);

            return Ok();
        }
    }
}