using System;

namespace simpleBlog.Ui.Models
{
    public class BaseResponseModel<T>
    {
        public string status { get; set; }
        public T data { get; set; }
        public BaseResponseModel()
        {
            data = default;
        }
    }
}
