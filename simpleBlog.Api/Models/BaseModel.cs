using System;

namespace simpleBlog.Api.Models
{
    public class BaseModel<T>
    {
        public string status { get; set; }
        public T data { get; set; }
        public BaseModel()
        {
            status = StatusEnum.success.ToString();
            data = default;
        }
    }
}
