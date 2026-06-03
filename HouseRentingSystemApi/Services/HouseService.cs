using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HouseRentingSystemApi.Services
{
    public sealed class HouseService(AppDbContext context, UserManager<AppUser> users) : IHouseService
    {
        public async Task<List<HouseDetailModel>> GetAll()
        {
            var model = await context.Houses
                .AsNoTracking()
                .Where(x => x.IsDeleted == false)
                .Select(h => new HouseDetailModel()
                {
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl
                })
                .ToListAsync();

            return model;
        }

        public async Task<HouseDetailModel> GetById(int id)
        {
            var search = await context.Houses.FirstOrDefaultAsync(h => h.Id == id && h.IsDeleted == false);
           
            if (search == null)
            {
                throw new NullReferenceException("No such house exists");
            }
            
            var house = new HouseDetailModel
            {
                Title = search.Title,
                Description = search.Description,
                Address = search.Address,
                Category = search.Category.Name,
                ImageUrl= search.ImageUrl,
                PricePerMonth = search.PricePerMonth,

            };

            return house;
        }

        public async Task<House> Create(HouseDetailModel model,string userId )
        {
            var newHouse = new House()
            {
                Description = model.Description,
                PricePerMonth = model.PricePerMonth,
                Address = model.Address,
                Title = model.Title,
                IsDeleted = false,
                ImageUrl = model.ImageUrl
            };

            var category = await context.Categories
                .FirstOrDefaultAsync(c => c.Name == model.Category
                .ToString());

            if (category == null)
            {
                var newCategory = new Category()
                {
                    Name = model.Category.ToString(),
                };
                context.Categories.Add(newCategory);
                await context.SaveChangesAsync();
                newHouse.CategoryId = newCategory.Id;
            }
            else
            {
                newHouse.CategoryId = category.Id;
            }

            newHouse.Owner = await users.FindByIdAsync(userId);
            context.Houses.Add(newHouse);
            await context.SaveChangesAsync();

            return newHouse;
        }

        public async Task<House> Edit(int id, HouseDetailModel model)
        {
            House? house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id && h.IsDeleted == false);
            Category? category = await context.Categories.FirstOrDefaultAsync(c => c.Name == model.Category.ToString());
            if (house == null)
            {
                throw new NullReferenceException("No such house found");
            }
            if (category == null)
            {
                throw new NullReferenceException("No such category found");
            }

            house.Title = model.Title;
            house.PricePerMonth = model.PricePerMonth;
            house.Address = model.Address;
            house.Category = category;
            house.ImageUrl = model.ImageUrl;
            house.Description = model.Description;

            await context.SaveChangesAsync();

            return house;
        }

        public async Task<House> Delete(int id)
        {
            House? house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id);
            if (house == null || house.IsDeleted == true)
            {
                throw new NullReferenceException("No such house exists");
            }
            house.IsDeleted = true;
            await context.SaveChangesAsync();

            return house;
        }

        public async Task<List<HouseDetailModel>> Search(string category,  string search,  string sort)
        {
            string text = search?.ToLower() ?? "";
            var query = context.Houses
                .Where(x => x.Description.ToLower().Contains(text) || x.Title.ToLower().Contains(text) || x.Address.ToLower().Contains(text));

            var categories = await context.Categories.AsNoTracking().Where(x => x.Name.ToLower() == category.ToLower()).ToListAsync();

            if (categories.Count < 1 && category != "all")
            {
                throw new NullReferenceException("No such category");
            }
            else if (category != "all")
            {
                query = query.Where(x => x.Category.Name == category);
            }



            if (sort == "asc")
            {
                query = query.OrderBy(x => x.PricePerMonth);
            }
            else if (sort == "desc")
            {
                query = query.OrderByDescending(x => x.PricePerMonth);
            }
            else
            {
                throw new NullReferenceException("No such sorting category");
            }

            var houses = await query.Select(x => new HouseDetailModel
            {
                Title = x.Title,
                Description = x.Description,
                Address = x.Address,
                Category = x.Category.Name,
                ImageUrl = x.ImageUrl,
                PricePerMonth = x.PricePerMonth,
            }).ToListAsync();

            return houses;
        }

        public async Task<House> RentHouse(int id, AppUser user)
        {
            var house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id);
            if (house == null)
            {
                throw new NullReferenceException("No such house");
            }
            if (house.Owner == null)
            {
                throw new NullReferenceException("House is already rented by someone");
            }

            house.Owner = user;

            return house;

        }

        public async Task<House> LeaveHouse(int id)
        {
            var house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id);
            if (house == null)
            {
                throw new NullReferenceException("No such house");
            }
            house.Owner = null;
            return house;
        }
    }
}
