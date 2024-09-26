using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tickets.BLL.Interfaces;
using Tickets.BLL.Repositories;
using Tickets.DLL.Context;
using Tickets.DLL.Models;
using Tickets.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(ob =>
{
    ob.Password.RequireNonAlphanumeric = false;
    ob.Password.RequireUppercase= false;
    ob.Password.RequiredLength= 5;
})
       .AddEntityFrameworkStores<TicketsDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddDbContext<TicketsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")));


builder.Services.AddScoped<IOrganizrRepository, OrganizrRepository>();

builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<IUintOfWork, UintOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Event}/{action=ApprovedEvents}/{id?}");

app.Run();
