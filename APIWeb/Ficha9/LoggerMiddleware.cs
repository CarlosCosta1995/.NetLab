using System.Diagnostics;
namespace Ficha9
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;
        public LoggerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string before = String.Format($"BEFORE: {context.Request.Path}, {context.Request.Method}, {DateTime.Now}\n");
            string after = String.Format($"AFTER: {context.Request.Path}, {context.Request.Method}, {DateTime.Now}\n");
           
            File.AppendAllText("logs.txt", before);
            Debug.WriteLine(before);

            await next(context);

            Debug.WriteLine(after);
            File.AppendAllText("logs.txt", after);
        }
    }
}
