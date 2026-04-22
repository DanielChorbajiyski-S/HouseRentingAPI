using System.Diagnostics;

namespace HouseRentingSystemApi.MiddleWares
{
    public class StopWacthMiddleware
    {
        public readonly RequestDelegate next;
        public StopWacthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await this.next(context);
            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");
        }
    }
}
