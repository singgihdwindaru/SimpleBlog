using simpleBlog.Ui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simpleBlog.Ui.Interface
{
    public interface INews<T>
    {
        Task<bool> CreateNewsAsync(params object[] id);
        Task<bool> UpdateNewsAsync(params object[] id);
        Task<bool> DeleteNewsAsync(params object[] id);
        Task<IEnumerable<T>> GetNewsByCategoriesAsync(params object[] id);
        Task<IEnumerable<T>> GetNewsByTagsAsync(params object[] id);
        Task<IEnumerable<T>> GetDataAsync(params object[] id);
    }
}
