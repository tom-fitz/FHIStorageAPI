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
        private IImageInfoRepository _imageInfoRepository;

        public FurnitureController(IFurnitureInfoRepository furnitureInfoRepository, IImageInfoRepository imageInfoRepository)
        {
            _furnitureInfoRepository = furnitureInfoRepository;
            _imageInfoRepository = imageInfoRepository;
        }

        public bool IsNotEmpty(IEnumerable<FurnitureSet> Dataset)
        {
            return Dataset != null && Dataset.Any(c => c != null);
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
                    Width = x.Width,
                    Height = x.Height,
                    Quantity = x.Quantity,
                    Notes = x.Notes,
                    IsFurnitureSet = x.IsFurnitureSet,
                    FurnitureImageId = x.FurnitureImageId,
                    FurnitureImages = x.FurnitureImages.ToList(),
                    House = x.House
                });
            }

            return Ok(result);
        }
        [HttpGet("houses/{houseId}/furniture")]
        public IActionResult GetFurnitureByHouseId(int houseID)
        {
            var furnitureInHouse = _furnitureInfoRepository.GetFurnitureByHouseId(houseID);

            if (furnitureInHouse != null)
            {

            }

            if (furnitureInHouse == null)
            {
                return NotFound();
            }

            var results = new List<Furniture>();

            foreach (var f in furnitureInHouse)
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
                    Width = f.Width,
                    Height = f.Height,
                    Quantity = f.Quantity,
                    Notes = f.Notes,
                    IsFurnitureSet = f.IsFurnitureSet,
                    FurnitureImageId = f.FurnitureImageId,
                    FurnitureImages = f.FurnitureImages.ToList(),
                    House = f.House
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
                    Width = f.Width,
                    Height = f.Height,
                    Quantity = f.Quantity,
                    Notes = f.Notes,
                    IsFurnitureSet = f.IsFurnitureSet,
                    FurnitureImageId = f.FurnitureImageId,
                    FurnitureImages = f.FurnitureImages.ToList(),
                    House = f.House
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
                    Width = f.Width,
                    Height = f.Height,
                    Quantity = f.Quantity,
                    Notes = f.Notes,
                    IsFurnitureSet = f.IsFurnitureSet,
                    FurnitureImageId = f.FurnitureImageId,
                    FurnitureImages = f.FurnitureImages.ToList(),
                    House = f.House
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
                    Type = c.Type,
                    CategoryGroupId = c.CategoryGroupId
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
                Turns = newFurniture.Turns,
                Width = newFurniture.Width,
                Height = newFurniture.Height,
                Quantity = newFurniture.Quantity,
                Notes = newFurniture.Notes,
                IsFurnitureSet = newFurniture.IsFurnitureSet
            };

            _furnitureInfoRepository.AddNewFurniture(finalFurniture);

            return Ok(finalFurniture);
        }

        [HttpPut("furnitureSets/{id}/{qty}")]
        public IActionResult UpdateFurnitureSets([FromBody] FurnitureSet newFurnitureSet)
        {
            int quantity = newFurnitureSet.Quantity;

            if (newFurnitureSet == null || quantity == 0)
            {
                return BadRequest();
            }

            if (!_furnitureInfoRepository.FurnitureExists(newFurnitureSet.FurnitureId))
            {
                return NotFound($"No furniture set with the id: {newFurnitureSet.FurnitureId} was found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _furnitureInfoRepository.AddFurnitureSet(newFurnitureSet);

            return Ok(newFurnitureSet);
        }

        [HttpPut("furnitureSets/Assignment/{houseid}/{qty}")]
        public IActionResult CopyFurnitureItemAndInsertNewRow([FromBody] FurnitureSet assignedFurnitureSet)
        {
            if (!_furnitureInfoRepository.FurnitureExists(assignedFurnitureSet.FurnitureId))
            {
                return NotFound($"No furniture with the id: {assignedFurnitureSet.FurnitureId} was found");
            }

            var getFurnitureToCopy = _furnitureInfoRepository.GetFurnitureByFurnitureId(assignedFurnitureSet.FurnitureId);
            //_furnitureInfoRepository.FurnitureExists(assignedFurnitureSet.FurnitureId);

            if (getFurnitureToCopy == null)
            {
                return NotFound($"Furniture to copy not found");
            }

            var furnitureIdToCopy = 0;

            foreach (var x in getFurnitureToCopy)
            {
                furnitureIdToCopy = x.FurnitureId;
            }

            var totalQuantity = 0;
            var furnitureSetId = 0;

            FurnitureSet getFurnitureSet = _furnitureInfoRepository.GetFurnitureSetByFurnitureId(assignedFurnitureSet.FurnitureId);

            if(getFurnitureSet == null)
            {
                foreach (var x in getFurnitureToCopy)
                {
                    totalQuantity = x.Quantity;
                }
            }
            else
            {
                totalQuantity = getFurnitureSet.Quantity;
                furnitureSetId = getFurnitureSet.FurnitureSetId;
            }

            if (assignedFurnitureSet.Quantity > totalQuantity)
            {
                return BadRequest($"Assigning more items than are avaiable in the warehouse.");
            }
            

            int houseId = Convert.ToInt32(RouteData.Values["houseid"]);


            _furnitureInfoRepository.AssignFurnitureSet(assignedFurnitureSet.Quantity, furnitureIdToCopy, houseId);

            return Ok(assignedFurnitureSet);
        }

        [HttpPut("furnitureSet/Warehouse")]
        public IActionResult AssignFurnitureBackToWarehouse([FromBody] Furniture updateFurnitureSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_furnitureInfoRepository.FurnitureExists(updateFurnitureSet.FurnitureId))
            {
                return NotFound($"No furniture with the id: {updateFurnitureSet.FurnitureId} was found");
            }

            _furnitureInfoRepository.AssignFurnitureSetBackToWarehouse(updateFurnitureSet);

            return Ok();
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
                return NotFound($"No furniture with the id: {newFurniture.FurnitureId} was found");
            }

            _furnitureInfoRepository.updateFurnitureByFurnitureId(newFurniture);

            return Ok(newFurniture);
        }
        [HttpDelete("furniture/{id}")]
        public IActionResult DeleteFurnitureByFurnitureId(int id)
        {
            var furnitureToDelete = _furnitureInfoRepository.GetFurnitureByFurnitureId(id).ToList();

            if (furnitureToDelete[0] == null)
            {
                NotFound();
            }

            string guid = "";
            var furnitureImagedeletion = new FurnitureImage{};

            foreach (var f in furnitureToDelete)
            {
                if (f.Quantity > 1)
                {
                    FurnitureSet furnitureSetToDelete = _furnitureInfoRepository.GetFurnitureSetByFurnitureId(f.FurnitureId);
                    _furnitureInfoRepository.DeleteFurnitureSet(furnitureSetToDelete);
                }
                if (f.FurnitureImages.Count > 0)
                {
                    foreach (var p in f.FurnitureImages)
                    {
                            furnitureImagedeletion = p;
                            var newReg = new Regex(@"([^/]+$)");
                            string strMatch = p.PictureInfo;
                            guid = newReg.Matches(strMatch)[0].Value;
                    }
                }
            }

            if (furnitureImagedeletion.PictureInfo != null || furnitureImagedeletion.FurnitureImageId > 0)
            {
                _imageInfoRepository.DeleteImage(guid, furnitureImagedeletion);
            }
            _furnitureInfoRepository.DeleteFurnitureByFurnitureId(furnitureToDelete[0]);

            return NoContent();
        }
    }
}