using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using simpleBlog.Api.Interfaces;

namespace simpleBlog.Api.DataAccess
{
    public class ConfigurationApi : IAppSettings
    {
        public string connectionString { get; }
        public long dateTimeNow { get; }

        public IConfiguration configuration { get; set; }

        public ConfigurationApi(IConfiguration Configuration)
        {
            configuration = Configuration;
            connectionString = configuration["ConnectionStrings:connString"];
            dateTimeNow = DateTime.Now.Ticks;
        }

    }
}
