namespace HouseRentingSystemApi.MiddleWares
{
    public static class StopwatchExtensionMW
    {
        public static IApplicationBuilder UseStopWatch(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<StopWacthMiddleware>();
        }
    }
}
