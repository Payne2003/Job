using C___MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Th?i gian h?t h?n session (30 phút)
	options.Cookie.HttpOnly = true; // Cookie ch? ???c truy c?p qua HTTP
	options.Cookie.IsEssential = true; // Cookie này là c?n thi?t cho ?ng d?ng
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<JobContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("JobContext"))
);
AppDomain.CurrentDomain.SetData("Select.Html.dep", Path.Combine(Directory.GetCurrentDirectory(), "bin", "Select.Html.dep"));


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();



app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
