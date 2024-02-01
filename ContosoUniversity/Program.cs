using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContosoUniversity.Data;
// ASP.NET Core �ł� Entity Framework Core ���g�p���� Razor Pages - �`���[�g���A�� 1/8
// https://learn.microsoft.com/ja-jp/aspnet/core/data/ef-rp/intro?view=aspnetcore-8.0&tabs=visual-studio
//

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// ASP.NET Core �ł� Entity Framework Core ���g�p���� Razor Pages - �`���[�g���A�� 1/8
// https://learn.microsoft.com/ja-jp/aspnet/core/data/ef-rp/intro?view=aspnetcore-8.0&tabs=visual-studio
//

builder.Services.AddDbContext<SchoolContext>(options =>
// ASP.NET Core �ł� Entity Framework Core ���g�p���� Razor Pages - �`���[�g���A�� 1/8
// https://learn.microsoft.com/ja-jp/aspnet/core/data/ef-rp/intro?view=aspnetcore-8.0&tabs=visual-studio
//

    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext") ?? throw new InvalidOperationException("Connection string 'SchoolContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SchoolContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
