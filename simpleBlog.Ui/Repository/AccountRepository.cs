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
    public class AccountRepository :IRepository<LoginModel.Response>
    {
        private IConfigUi configApi;
          public AccountRepository(IConfigUi ConfigUi)
        {
            configApi = ConfigUi;
        }

        public async Task<IEnumerable<LoginModel.Response>> GetData(params object[] parameters)
        {
            LoginModel.Request request = new LoginModel.Request();
            request.username = parameters[0].ToString();
            request.password = parameters[1].ToString();

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            BaseResponseModel<List<LoginModel.Response>> response =
            await HTTPClientHelper.PostAsync<BaseResponseModel<List<LoginModel.Response>>>(configApi.url + "/api/User/Login", stringContent);
            return response.data;
        }
    }
}
