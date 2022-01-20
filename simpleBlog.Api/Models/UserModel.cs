using System;
using System.Text.Json.Serialization;

namespace simpleBlog.Api.Models.User
{
    public class UserModel
    {
        public class Request
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class Response
        {
            [JsonIgnore]
            public Guid? id { get; set; }
            public string username { get; set; }
            public string fullname { get; set; }
            public bool? is_active { get; set; }
            public string token { get; set; }
            public string refresh_token { get; set; }
            public string role { get; set; }
        }

    }
}
