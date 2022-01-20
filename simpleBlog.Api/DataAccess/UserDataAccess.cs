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
        private IConfigApi configApi;
        public UserDataAccess(IConfigApi ConfigApi)
        {
            configApi = ConfigApi;
        }
        public async Task<List<UserModel.Response>> GetUser(string username, string password)
        {
            List<UserModel.Response> userModel = null;
            try
            {
                using (NpgsqlConnection pgConn = new NpgsqlConnection(configApi.connectionString))
                {
                    await pgConn.OpenAsync();
                    var sql = @"
                    SELECT ur.id, u.username,u.is_active,r.role_name
                    from user_roles ur
                    inner join users u on u.id = ur.user_id
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
                                data.id = rdr.GetGuid(0);
                                data.username = rdr.GetString(1);
                                data.is_active = rdr.GetBoolean(2);
                                data.role = rdr.GetString(3);

                                userModel.Add(data);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                //TODO : Write log
            }

            return userModel;
        }

        public bool Insert(UserModel.Response userModel)
        {
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

        public bool Update(UserModel.Response userModel)
        {
            throw new NotImplementedException();
        }
    }
}
