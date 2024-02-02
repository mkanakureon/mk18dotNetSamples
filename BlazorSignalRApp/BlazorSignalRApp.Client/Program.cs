using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

await builder.Build().RunAsync();

var app = builder.Build();
app.UseResponseCompression();
app.MapHub<ChatHub>("/chathub");
