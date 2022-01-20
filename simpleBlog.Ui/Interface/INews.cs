using simpleBlog.Ui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simpleBlog.Ui.Interface
{
    public interface INews<T>: IRepository<Post>
    {
        //IRepository<T> repository();
        Task<IEnumerable<T>> CreateNews(params object[] id);
        Task<IEnumerable<T>> UpdateNews(params object[] id);
        Task<IEnumerable<T>> DeleteNews(params object[] id);
        Task<IEnumerable<T>> GetNewsByCategories(params object[] id);
        Task<IEnumerable<T>> GetNewsByTags(params object[] id);
    }
}
