using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simpleBlog.Api.Models.User;

namespace simpleBlog.Api.Interfaces
{
    public interface IUser
    {
        Task<List<UserModel.Response>> GetUser(string username, string password);
        public bool Insert(UserModel.Response userModel);
        public bool Update(UserModel.Response userModel);
    }
}
