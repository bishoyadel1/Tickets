using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tickets.BLL.Interfaces;
using Tickets.BLL.Repositories;
using Tickets.DLL.Context;
using Tickets.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<TicketsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddScoped<IOrganizrRepository, OrganizrRepository>();

builder.Services.AddScoped<IUintOfWork, UintOfWork>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
