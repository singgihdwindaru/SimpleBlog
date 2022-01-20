using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using simpleBlog.Ui.Interface;
using simpleBlog.Ui.Models;
using Newtonsoft.Json;
using System.Net.Http;
using simpleBlog.Ui.Helper;

namespace simpleBlog.Ui.Repository
{
    public class NewsRepository : INews<Post>
    {
        private IConfigUi configApi;
          public NewsRepository(IConfigUi ConfigUi)
        {
            configApi = ConfigUi;
        }

        public IRepository<Post> repository()
        {
            throw new System.NotImplementedException();
        }
        public Task<IEnumerable<Post>> CreateNews(params object[] id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Post>> DeleteNews(params object[] id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Post>> UpdateNews(params object[] id)
        {
            throw new System.NotImplementedException();
        }

        async Task<IEnumerable<Post>> IRepository<Post>.GetData(params object[] parameters)
        {
            Post request = new Post();
            request.id = parameters[0].ToString();
            request.reporterId = parameters[1].ToString();

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            BaseResponseModel<List<Post>> response =
            await HTTPClientHelper.PostAsync<BaseResponseModel<List<Post>>>(configApi.url + "/api/News/Edit", stringContent);
            return response.data;
        }

        public Task<IEnumerable<Post>> GetNewsByCategories(params object[] id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetNewsByTags(params object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
