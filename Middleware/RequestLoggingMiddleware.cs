namespace TestTaskVR.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Логирование информации о запросе
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}"); // берем метод и путь к выполнению запроса
            await _next(context); // передаем далее
        }
    }
}
