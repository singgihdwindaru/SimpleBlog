using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simpleBlog.Api.Models.User;

namespace simpleBlog.Api.Interfaces
{
    public interface IUser
    {
        Task<List<UserModel.Response>> GetUserAsync(string username, string password);
        Task<bool> InsertAsync(UserModel.Response userModel);
        Task<bool> UpdateAsync(UserModel.Response userModel);
    }
}
