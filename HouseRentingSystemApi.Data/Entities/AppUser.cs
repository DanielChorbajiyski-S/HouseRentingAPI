using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystemApi.Data.Entities
{
	public class AppUser : IdentityUser
	{
		public List<House> Houses { get; set; } = null!;
	}
}
