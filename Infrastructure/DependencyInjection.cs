using Application.Interfaces.Repositories;
using Domain;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection service, IConfiguration configure)
    {
        service.AddDbContext<ApplicationDbContext>(option =>
        {
            var connectionString = configure.GetConnectionString("DefaultConnectionMySql");
            var mySqlVersion = ServerVersion.AutoDetect(connectionString);
            option.UseMySql(
                connectionString
                , mySqlVersion
            );
        });
        
        service.AddTransient<IGenericRepository<DataLatih>, GenericRepository<DataLatih>>();
    }
}