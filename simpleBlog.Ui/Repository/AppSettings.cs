using System;
using Microsoft.Extensions.Configuration;
using simpleBlog.Ui.Interface;

namespace simpleBlog.Ui.Repository
{
    public class AppSettings : IConfigUi
    {
         public string connectionString { get; }
        public long dateTimeNow { get; }

        public IConfiguration configuration { get; set; }

        public string url { get; }

        public string secrteKey { get; }
           public AppSettings(IConfiguration Configuration)
        {
            configuration = Configuration;
            connectionString = configuration["ConnectionStrings:raufConnString"];
            url = configuration["url"];
            secrteKey = configuration["secretKey"];
            dateTimeNow = DateTime.Now.Ticks;
        }
    }
}
