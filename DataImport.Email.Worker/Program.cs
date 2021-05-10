using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataImport.Storage.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataImport.Email.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new DefaultServiceProviderFactory())
                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfigureDependencyInjection(hostContext.Configuration);
                    services.AddDbContext<AppDbContext>(opts => 
                        opts.UseSqlServer(
                            hostContext.Configuration.GetConnectionString("DefaultConnection"), 
                            opt => 
                                opt.MigrationsAssembly("DataImport.Storage.Migrations")));
                    services.AddHostedService<Worker>();
                });
    }}