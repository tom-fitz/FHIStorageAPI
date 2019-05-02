using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FHIStorage.API.Entities;
//using FHIStorage.API.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Remotion.Linq.Clauses;

namespace FHIStorage.API.Services
{
    public class HouseInfoRepository : IHouseInfoRepository
    {
        private HouseInfoContext _ctx;
        public HouseInfoRepository(HouseInfoContext ctx)
        {
            _ctx = ctx;
        }

        public void AddNewHouse(House newHouse)
        {
            _ctx.Houses.Add(newHouse);
            _ctx.SaveChangesAsync();
        }
        public House GetHouse(int houseId)
        {
            return _ctx.Houses.Where(c => c.HouseId == houseId).FirstOrDefault();
        }

        public IEnumerable<House> GetHouses()
        {
            return _ctx.Houses.OrderBy(c => c.ContractDate).ToList();
        }
        public bool HouseExists(int houseId)
        {
            return _ctx.Houses.Any(h => h.HouseId == houseId);
        }

        public void UpdateHouseById(House houseToUpdate)
        {
            House entity = _ctx.Houses.FirstOrDefault(h => h.HouseId == houseToUpdate.HouseId);

            if (entity != null)
            {
                entity.HouseId = houseToUpdate.HouseId;
                entity.Address = Convert.ToString(houseToUpdate.Address);
                entity.Zipcode = Convert.ToInt32(houseToUpdate.Zipcode);
                entity.Cost = Convert.ToDecimal(houseToUpdate.Cost);
                entity.ContractDate = Convert.ToDateTime(houseToUpdate.ContractDate);
                entity.DateSold = Convert.ToDateTime(houseToUpdate.DateSold);
                entity.Sold = Convert.ToBoolean(houseToUpdate.Sold);

                _ctx.Houses.Update(entity);

                _ctx.SaveChangesAsync();
            }
        }

        public void DeleteHouseById(House houseToDelete)
        {
            var changFurnIds = _ctx.Furniture.Where(f => (f.HouseId == houseToDelete.HouseId));
            //var changFurnIds = GetHouse(houseToDelete.HouseId);
            foreach (var x in changFurnIds)
            {
                x.HouseId = 7;
                _ctx.Furniture.Update(x);
            }

            _ctx.Houses.Remove(houseToDelete);
            _ctx.SaveChangesAsync();
        }
    }
}
