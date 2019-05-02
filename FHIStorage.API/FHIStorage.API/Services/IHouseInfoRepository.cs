using FHIStorage.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FHIStorage.API.Services
{
    public interface IHouseInfoRepository
    {
        IEnumerable<House> GetHouses();
        House GetHouse(int houseId);
        void AddNewHouse(House finalHouse);
        void UpdateHouseById(House houseToUpdate);
        void DeleteHouseById(House houseToDelete);
        bool HouseExists(int houseId);
    }
}
