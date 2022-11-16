using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcDB.Models;
var builder = WebApplication.CreateBuilder(args);


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<MvcContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("MvcContext")));
}
else
{
    builder.Services.AddDbContext<MvcContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMvcContext")));
}


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// seed people data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedPeople.Initialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
