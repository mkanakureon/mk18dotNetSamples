namespace WebApplication3
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // リクエストをログに記録
            _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

            // 既存のレスポンスストリームを保持
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                // レスポンスを新しいストリームに書き込むように設定
                context.Response.Body = responseBody;

                await _next(context);

                // レスポンスをログに記録
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation($"Response: {text}");

                // レスポンスを元のストリームにコピー
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
