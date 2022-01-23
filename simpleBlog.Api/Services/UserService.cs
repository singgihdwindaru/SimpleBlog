using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simpleBlog.Api.BusinessLogic;
using simpleBlog.Api.DataAccess;
using simpleBlog.Api.Interfaces;
using simpleBlog.Api.Models.User;

namespace simpleBlog.Api.Services
{
    public class UserService
    {
        private readonly UserBusinessLogic userBusinessLogic;
        public UserService(IAppSettings configApi)
        {
            userBusinessLogic = new UserBusinessLogic(new UserDataAccess(configApi));
        }
        public async Task<List<UserModel.Response>> GetUser(string username, string password)
        {
            return await userBusinessLogic.Authenticate(username, password);
        }
        public async Task<bool> TokenSource(UserModel.Response userModel)
        {
            return await userBusinessLogic.InsertRequesterTokenAsync(userModel);
        }
    }
}
