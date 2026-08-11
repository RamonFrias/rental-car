using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCar.Application.Intrerfaces;
using RentalCar.Application.Mapping;
using RentalCar.Application.Services;
using RentalCar.Domain.Interfaces;
using RentalCar.Infraestructure.Context;
using RentalCar.Infraestructure.Identity;
using RentalCar.Infraestructure.Persistence;
using RentalCar.Infraestructure.Persistence.Repositories;
using RentalCar.Infraestructure.Services;

namespace RentalCar.CrossCutting.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPIs(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => 
                     options.UseMySql(connectionString, 
                     ServerVersion.AutoDetect(connectionString)));
            services.AddAutoMapper(typeof(ApiMappingDTOProfile));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
                options.AddPolicy("ExclusiveAccess", policy => policy.RequireClaim("id", "RamonFrias2"));
            });
            return services;
        }
    }
}
