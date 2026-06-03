using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using HouseRentingSystemApi.Models.Enums;
using HouseRentingSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace HouseRentingSystemApi.Controllers
{
	[Route("api/[controller]")]
	public class HouseController(IHouseService service) : ControllerBase
	{
		private AppDbContext context;
		private UserManager<AppUser> users;

		//public HouseController(AppDbContext context, UserManager<AppUser> users)
		//{
		//	this.context = context;
		//	this.users = users;
		//}

		[HttpGet("All")]
		[Produces(typeof(IEnumerable<HouseDetailModel>))]
		public async Task<IActionResult> GetAll()
		{
			var model = await service.GetAll();

			return Ok(model);
		}

		[HttpGet("{id}")]
		[Produces(typeof(HouseDetailModel))]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				HouseDetailModel house = await service.GetById(id);
				return Ok(house);
			}
			catch (Exception ex) 
			{
				return BadRequest(ex.Message);
			}
			
		}

		[Authorize(Roles = "Agent")]
		[HttpPost("Create")]
		[Produces(typeof(HouseDetailModel))]
		public async Task<IActionResult> Create([FromBody] HouseDetailModel model)
		{
			if (ModelState.IsValid == false)
			{
				return BadRequest();
			}
			var isAuthenticated = User.Identity?.IsAuthenticated;
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var newHouse = await service.Create(model, userId);

			return Created($"api/Create/{newHouse.Id}", new HouseDetailModel()
			{
				Address = newHouse.Address,
				ImageUrl = newHouse.ImageUrl,
				Title = newHouse.Title,
				Description = newHouse.Description,
				PricePerMonth = newHouse.PricePerMonth,
				Category = model.Category
			});
		}

		[Authorize(Roles = "Agent")]
		[HttpPut("Update/{id}")]
		[Produces(typeof(HouseDetailModel))]
		public async Task<IActionResult> Edit(int id, [FromBody] HouseDetailModel model)
		{
			if (ModelState.IsValid == false)
			{
				return BadRequest(new { message = "No such house found" });
			}

			try
			{
				House house = await service.Edit(id, model);
				return Ok(model);
			}
			catch (Exception ex)
			{
				{
					return BadRequest(ex.Message);
				}
			}
		}

		[Authorize(Roles = "Agent")]
		[HttpDelete("Delete/{id}")]
		[Produces(typeof(HttpStatusCode))]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				House house = await service.Delete(id);
				return Ok();
			}
			catch ( Exception ex) 
			{
				return BadRequest(ex.Message );
			}
		}

		[HttpGet("Search")]
		[Produces(typeof(List<HouseDetailModel>))]
		public async Task<IActionResult> Search([FromQuery] string category, [FromQuery] string search, [FromQuery] string sort)
		{
			try
			{
				List<HouseDetailModel> houses = await service.Search(category, search, sort);
				return Ok(houses);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles = "Client")]
		[HttpPut("RentHouse/{id}")]
		[Produces(typeof(IActionResult))]
		public async Task<IActionResult> RentHouse(int id)
		{
			var userId = User.FindFirst("sub")?.Value;
			var user = await users.FindByIdAsync(userId);

			try
			{
				var house = await service.RentHouse(id, user);
				return Ok($"House rented! You will be paying the tiny sum of {house.PricePerMonth}€ per month.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles = "Client")]
		[HttpPut("LeaveHouse/{id}")]
		[Produces(typeof(IActionResult))]
		public async Task<IActionResult> LeaveHouse(int id)
		{
			try
			{
				var house = service.LeaveHouse(id);
				return Ok("You left the house");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
		}
		
	}
}
