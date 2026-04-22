using System.Runtime.CompilerServices;

namespace HouseRentingSystemApi.MiddleWares
{
    public static class CustomExtensionMW
    {
        public static IApplicationBuilder UseCustom(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
