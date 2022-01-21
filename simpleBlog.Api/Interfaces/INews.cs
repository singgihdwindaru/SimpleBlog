using simpleBlog.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simpleBlog.Api.Interfaces
{
    public interface INews<T> where T : class
    {
        Task<List<T>> GetDataAsync(EnumParam param = default, params object[] parameters);
        Task<bool> InsertAsync(T parameter);
        Task<bool> UpdateAsync(T parameter);
        Task<bool> DeleteAsync(T parameter);
    }
}
