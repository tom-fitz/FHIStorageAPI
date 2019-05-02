using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FHIStorage.API.Entities;
using FHIStorage.API.Models;
using FHIStorage.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FHIStorage.API.Controllers
{
    [Route("api/houses")]
    [ApiController]
    public class HousesController : ControllerBase
    {
        private IHouseInfoRepository _houseInfoRepository;

        public HousesController(IHouseInfoRepository houseInfoRepository)
        {
            _houseInfoRepository = houseInfoRepository;
        }

        [HttpGet]
        public IActionResult GetAllHouses()
        {
            var houseEntities = _houseInfoRepository.GetHouses();

            var results = new List<HousesModel>();

            foreach (var h in houseEntities)
            {
                results.Add(new HousesModel
                {
                    id = h.HouseId,
                    address = h.Address,
                    zipcode = h.Zipcode,
                    cost = h.Cost,
                    contractDate = h.ContractDate,
                    dateSold = h.DateSold,
                    sold = h.Sold
                });
            }

            return Ok(results);
        }
        [HttpGet("{id}", Name = "GetHouseById")]
        public IActionResult GetHouseByID(int id)
        {
            var houseToReturn = _houseInfoRepository.GetHouse(id);

            var results = new List<HousesModel>();

            results.Add(new HousesModel
            {
                id = houseToReturn.HouseId,
                address = houseToReturn.Address,
                zipcode = houseToReturn.Zipcode,
                cost = houseToReturn.Cost,
                contractDate = houseToReturn.ContractDate,
                dateSold = houseToReturn.DateSold,
                sold = houseToReturn.Sold
            });

            return Ok(results);
        }
        [HttpPost]
        public IActionResult CreateHouse([FromBody] House newHouse)
        {
            if (newHouse == null)
            {
                return BadRequest();
            }

            var finalHouse = new House()
            {
                Address = newHouse.Address,
                Zipcode = newHouse.Zipcode,
                Cost = Convert.ToDecimal(newHouse.Cost),
                ContractDate = Convert.ToDateTime(newHouse.ContractDate),
                DateSold = Convert.ToDateTime(newHouse.DateSold),
                Sold = newHouse.Sold
            };

            _houseInfoRepository.AddNewHouse(finalHouse);

            return CreatedAtRoute("GetHouseById", new { id = finalHouse.HouseId }, finalHouse);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateHouseById([FromBody] House houseToUpdate)
        {
            if (houseToUpdate == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_houseInfoRepository.HouseExists(houseToUpdate.HouseId))
            {
                return NotFound();
            }

            _houseInfoRepository.UpdateHouseById(houseToUpdate);

            return CreatedAtRoute("GetHouseById", new { id = houseToUpdate.HouseId }, houseToUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHouseById(int id)
        {
            var houseToDelete = _houseInfoRepository.GetHouse(id);

            if (houseToDelete == null)
            {
                NotFound();
            }

            _houseInfoRepository.DeleteHouseById(houseToDelete);

            return Ok();
        }
    }
}