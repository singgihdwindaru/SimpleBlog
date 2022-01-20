using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace simpleBlog.Api.DataAccess
{
    public class ConfigurationApi : IConfigApi
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
    public interface IConfigApi
    {
        string connectionString { get; }
        long dateTimeNow { get; }
    }
}
