using Microsoft.EntityFrameworkCore;
using WordSearchWebApplication.Models;
using WordSearchWebApplication.Repositories;
using WordSearchWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IWordSearchService, WordSearchService>();

builder.Services.AddScoped<IWordSearchRepository, WordSearchRepository>();

builder.Services.AddDbContext<WordSearchContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("WordSearchConString")));


var app = builder.Build();


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
    pattern: "{controller=WordSearch}/{action=Index}/{id?}");


app.Run();
