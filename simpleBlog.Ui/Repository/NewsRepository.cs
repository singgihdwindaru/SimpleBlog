using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using simpleBlog.Ui.Interface;
using simpleBlog.Ui.Models;
using Newtonsoft.Json;
using System.Net.Http;
using simpleBlog.Ui.Helper;
using System;
using System.Linq;

namespace simpleBlog.Ui.Repository
{
    public class NewsRepository : INews<Post>
    {
        private IConfigUi configApi;
        public NewsRepository(IConfigUi ConfigUi)
        {
            configApi = ConfigUi;
        }

        public async Task<bool> CreateNewsAsync(params object[] parameters)
        {
            bool result = false;
            if (parameters == null || parameters.Length < 3)
            {
                return result;
            }
            List<Post> post = new List<Post> { (Post)parameters[0] };
            string token = parameters[1].ToString();
            string reporterName = parameters[2].ToString();
            BaseResponseModel<List<Post>> response = new BaseResponseModel<List<Post>>();
            try
            {
                Dictionary<string, object> request = new Dictionary<string, object>();
                request.Add("artikelId", Convert.ToInt32(post.FirstOrDefault().id));
                request.Add("reporterId", -1);
                request.Add("content", post.FirstOrDefault().content);
                request.Add("excerpt", post.FirstOrDefault().excerpt);
                request.Add("isPublished", Convert.ToBoolean(post.FirstOrDefault().isPublished));
                request.Add("lastModified", post.FirstOrDefault().lastModified);
                request.Add("pubDate", post.FirstOrDefault().isPublished ? DateTime.Now : DateTime.MinValue);
                request.Add("title", post.FirstOrDefault().title);
                request.Add("reporterName", reporterName);

                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + token);

                response = await HTTPClientHelper.PostAsync<BaseResponseModel<List<Post>>>(configApi.url + "/api/News/PostNews", stringContent, headers);
                if (response.data != null)
                {
                    result = true;
                }
            }
            catch (System.Exception ex)
            {
                _ = ex.ToString();
            }

            return result;

        }

        public Task<bool> DeleteNewsAsync(params object[] id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateNewsAsync(params object[] id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetDataAsync(params object[] parameters)
        {
            BaseResponseModel<List<Post>> response = new BaseResponseModel<List<Post>>();
            try
            {
                Dictionary<string, int> request = new Dictionary<string, int>();
                request.Add("artikelId", Convert.ToInt32(parameters[0].ToString()));

                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + parameters[1].ToString());

                response = await HTTPClientHelper.PostAsync<BaseResponseModel<List<Post>>>(configApi.url + "/api/News/GetArticleById", stringContent, headers);
            }
            catch (System.Exception ex)
            {
                _ = ex.ToString();
            }

            return response.data;
        }

        public Task<IEnumerable<Post>> GetNewsByCategoriesAsync(params object[] id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetNewsByTagsAsync(params object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
