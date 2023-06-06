using CleanArch.Infrastructure.Data;
using CleanArch.IoC.DependenciesContainers;
using Microsoft.EntityFrameworkCore;

#region Services


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

#region Db context

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString(
            StaticDataStore.SqlServerConnectionStringName)));

#endregion

#region Dependency injection

services.AddApplicationDependencies();

#endregion

#region Mvc

services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

#endregion

#endregion

#region Middlewares

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

#endregion