using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemApi.Services
{
    public interface IHouseService
    {
        public  Task<List<HouseDetailModel>> GetAll();

        public  Task<HouseDetailModel> GetById(int id);

        public Task<House> Create(HouseDetailModel model, string userId);

        public Task<House> Edit(int id, HouseDetailModel model);

        public  Task<House> Delete(int id);

        public  Task<List<HouseDetailModel>> Search(string category, string search, string sort);

        public Task<House> RentHouse(int id, AppUser user);


        public Task<House> LeaveHouse(int id);

    }
}
