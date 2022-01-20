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
        public UserBusinessLogic(IUser User)
        {
            user = User;
        }

        public async Task<List<UserModel.Response>> Authenticate(string username, string password)
        {
            return await user.GetUser(username, password);
        }
        public bool InsertRequesterToken(UserModel.Response userModel)
        {
            return user.Insert(userModel);
        }
    }
}
