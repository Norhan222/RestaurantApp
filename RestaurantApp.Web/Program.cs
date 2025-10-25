using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Application.Services;
using RestaurantApp.Infrastructure.Data;
using RestaurantApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using RestaurantApp.Infrastructure.Services;
using RestaurantApp.Web.Helper;
using RestaurantApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace RestaurantApp.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
             

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<RestaurantDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

          
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<IFileService ,FileService>();
            builder.Services.AddScoped<IAppEnvirnment, AppEnvirnment>();
            builder.Services.AddScoped<IMenuItemService, MenuItemService>();
            builder.Services.AddScoped<ICartService ,Cartservice>();
            builder.Services.AddScoped<IMenuItemRepo, MenuItemRepo>();
            builder.Services.AddScoped<ICartRepo, CartRepo>();
            builder.Services.AddScoped<IOrderRepo ,OrderRepo>();
            builder.Services.AddScoped<IOrderService  ,OrderService>();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<RestaurantDbContext>();

            builder.Services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = builder.Configuration.GetConnectionString("RedisConnection");  // store tempdata or session in redis instadof memory
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return  ConnectionMultiplexer.Connect(connection);
            });
            
            builder.Services.AddSession(op =>
            {
                op.IdleTimeout = TimeSpan.FromDays(1);
                op.Cookie.HttpOnly = true;
                op.Cookie.IsEssential = true;
            });
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services=scope.ServiceProvider;
                var context=services.GetRequiredService<RestaurantDbContext>();
                context.Database.Migrate();
                await StoreContextSeed.SeedAsync(context);
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
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
