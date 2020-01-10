﻿using ApiC.Data.Context;
using ApiC.Data.Repository;
using ApiC.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiC.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository (IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql("Server=localhost;Port=3306;Database=Api;User=root;Pwd=password")
            );
        }
    }
}
