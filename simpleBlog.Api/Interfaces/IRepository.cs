using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace simpleBlog.Api.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetData(params object[] id);
    }
}
