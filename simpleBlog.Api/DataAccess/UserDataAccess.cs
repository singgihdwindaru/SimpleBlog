using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simpleBlog.Api.Interfaces;
using simpleBlog.Api.Models.User;
using Npgsql;

namespace simpleBlog.Api.DataAccess
{
    public class UserDataAccess : IUser
    {
        private IAppSettings configApi;
        public UserDataAccess(IAppSettings ConfigApi)
        {
            configApi = ConfigApi;
        }
        public async Task<List<UserModel.Response>> GetUserAsync(string username, string password)
        {
            List<UserModel.Response> userModel = null;
            try
            {
                using (NpgsqlConnection pgConn = new NpgsqlConnection(configApi.connectionString))
                {
                    await pgConn.OpenAsync();
                    var sql = @"
                    SELECT u.id AS userId, u.username, u.is_active,ur.id AS roleId, r.role_name
                    , u.name
                    from users u 
                    inner join user_roles ur on u.id = ur.user_id
                    inner join roles r on r.id = ur.role_id
                    where u.username=@username and u.password=@password";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, pgConn);
                    cmd.Parameters.AddWithValue("@userName", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            userModel = new List<UserModel.Response>();

                            while (await rdr.ReadAsync())
                            {
                                UserModel.Response data = new UserModel.Response();
                                data.id = rdr.GetInt32(0);
                                data.username = rdr.GetString(1);
                                data.is_active = rdr.GetBoolean(2);
                                data.roleId = rdr.GetGuid(3);
                                data.role = rdr.GetString(4);
                                data.fullname = rdr.GetString(5);
                                userModel.Add(data);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
              _ = ex.Message;
            }

            return userModel;
        }

        public async Task<bool> InsertAsync(UserModel.Response userModel)
        {
            await Task.Yield();
            bool result = false;
            // using (NpgsqlConnection pgConn = new NpgsqlConnection(configApi.connectionString))
            // {
            //     pgConn.Open();
            //     var sql = string.Format("INSERT INTO get_token (httpcontext, httprequest_httpcontext) VALUES ('{0}', '{1}');",
            //     userModel.Address1, userModel.Address2);
            //     NpgsqlCommand cmd = new NpgsqlCommand(sql, pgConn);
            //     result = cmd.ExecuteNonQuery() == 1 ? true : false;
            // }
            return result;
        }

        public async Task<bool> UpdateAsync(UserModel.Response userModel)
        {
            await Task.Yield();
            return true;
        }
    }
}
