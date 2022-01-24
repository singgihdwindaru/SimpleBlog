using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simpleBlog.Api.DataAccess;
using simpleBlog.Api.Interfaces;
using simpleBlog.Api.Models.User;

namespace simpleBlog.Api.BusinessLogic
{
    public class UserBusinessLogic
    {
        IUser user;
        public UserBusinessLogic(IAppSettings AppSettings)
        {
            user = new UserDataAccess(AppSettings);
        }

        public async Task<List<UserModel.Response>> Authenticate(string username, string password)
        {
            return await user.GetUserAsync(username, password);
        }
        public async Task<bool> InsertRequesterTokenAsync(UserModel.Response userModel)
        {
            return await user.InsertAsync(userModel);
        }
    }
}
