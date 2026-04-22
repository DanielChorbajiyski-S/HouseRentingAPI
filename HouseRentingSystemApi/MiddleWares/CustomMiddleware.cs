
using HouseRentingSystemApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemApi.MiddleWares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate next;
        public CustomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext ctx)
        {
            var housesCount = await ctx.Houses.CountAsync();
            Console.WriteLine($"Total houses = {housesCount}");
            await this.next(context);
        }
    }
}
