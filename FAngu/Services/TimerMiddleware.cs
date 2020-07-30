namespace FAngu.Services
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;
        TestDITwo _testDI;

        public TimerMiddleware(RequestDelegate next, TestDITwo testDI)
        {
            _next = next;
            _testDI = testDI;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
        }
    }
}
