using Application.Interfaces.Services;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection service)
    {
        service.AddTransient<IDataLatihService, DataLatihService>();
        service.AddTransient<IPelatihanService, PelatihanService>();
        service.AddTransient<IPengujianService, PengujianService>();
    }
}