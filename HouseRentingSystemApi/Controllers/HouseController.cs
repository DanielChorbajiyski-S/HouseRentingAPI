using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using HouseRentingSystemApi.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace HouseRentingSystemApi.Controllers
{
	[Route("api/[controller]")]
	public class HouseController : ControllerBase
	{
		private AppDbContext context;

		public HouseController(AppDbContext context)
		{
			this.context = context;
		}

		[HttpGet("All")]
		[Produces(typeof(IEnumerable<HouseDetailModel>))]
		public async Task<IActionResult> GetAll()
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

			return Ok(model);
		}

		[HttpGet("{id}")]
		[Produces(typeof(HouseDetailModel))]
		public async Task<IActionResult> GetById(int id)
		{
			var house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id && h.IsDeleted == false);
			if (house == null)
			{
				return NotFound();
			}

			return Ok(new HouseDetailModel()
			{
				Title = house.Title,
				Address = house.Address,
				ImageUrl = house.ImageUrl
			});
		}

		[Authorize]
		[HttpPost("Create")]
		[Produces(typeof(HouseDetailModel))]
		public async Task<IActionResult> Create([FromBody]HouseDetailModel model)
		{
			if (ModelState.IsValid == false)
			{
				return BadRequest();
			}
			var isAuthenticated = User.Identity?.IsAuthenticated;
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var newHouse = new House()
			{
				Description = model.Description,
				PricePerMonth = model.PricePerMonth,
				Address = model.Address,
				Title=model.Title,

				ImageUrl =model.ImageUrl
			};

			//var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userEmail = User.FindFirstValue(ClaimTypes.Email);

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
			newHouse.UserId = userId;
			context.Houses.Add(newHouse);
			await context.SaveChangesAsync();

			return Created($"api/Create/{newHouse.Id}",new HouseDetailModel() 
			{ 
				Address = newHouse.Address,
				ImageUrl = newHouse.ImageUrl,
				Title = newHouse.Title,
				Description = newHouse.Description,
				PricePerMonth = newHouse.PricePerMonth,
				Category = model.Category
			});
		}

		[Authorize]
		[HttpPut("update/{id}")]
		[Produces(typeof(HouseDetailModel))]
		public async Task<IActionResult> Edit(int id, [FromBody]HouseDetailModel model)
		{
			House? house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id && h.IsDeleted == false);
			Category? category = await context.Categories.FirstOrDefaultAsync(c => c.Name == model.Category.ToString());
			if (house == null || ModelState.IsValid == false)
			{
				return BadRequest(new { message = "No such house found" });
			}
			if (category == null)
			{
				return BadRequest(new { message = "No such category" });
			}

			house.Title = model.Title;
			house.PricePerMonth = model.PricePerMonth;
			house.Address = model.Address;
			house.Category = category;
			house.ImageUrl = model.ImageUrl;
			house.Description = model.Description;

			context.SaveChanges();

			return Ok(model);
        }

		[Authorize]
		[HttpDelete("Delete/{id}")]
		[Produces(typeof(HttpStatusCode))]
		public async Task<IActionResult> Delete(int id)
		{
            House? house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id);
			if (house == null || house.IsDeleted == true)
			{
				return BadRequest(new { message = "No such house found" });
			}
			house.IsDeleted = true;
			context.SaveChanges();

			return Ok(new { message = "Deleted" });
        }
	}
}
