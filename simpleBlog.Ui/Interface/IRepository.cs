using System.Collections.Generic;
using System.Threading.Tasks;
namespace simpleBlog.Ui.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetData(params object[] id);
    }
}
