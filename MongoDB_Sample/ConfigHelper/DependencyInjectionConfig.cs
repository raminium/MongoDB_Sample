using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB_Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_Sample.ConfigHelper
{
    public static class DependencyInjectionConfig
    {
        internal static void RegisterDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = false;

            });//.AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.Configure<List<Models.MongoDatabaseSettings>>(configuration.GetSection("DatabaseSettings"));
            services.AddSingleton<BooksService>();
            services.AddSingleton<LogsService>();

            //services.Configure<Models.MongoDatabaseSettings>(configuration.GetSection("BookStoreDatabase"));
            //services.AddSingleton<BooksService>(bs=>new BooksService(Newtonsoft.Json.JsonConvert.DeserializeObject<Models.MongoDatabaseSettings>(dbSetting)));
            //dbSetting= configuration.GetSection("APILogDatabase").Value;
            //services.AddSingleton<LogsService>(ls => new LogsService(Newtonsoft.Json.JsonConvert.DeserializeObject<Models.MongoDatabaseSettings>(dbSetting)));
        }
    }
}
