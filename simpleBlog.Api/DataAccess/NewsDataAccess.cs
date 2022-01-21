using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simpleBlog.Api.Interfaces;
using simpleBlog.Api.Models.User;
using Npgsql;
using simpleBlog.Api.Models;

namespace simpleBlog.Api.DataAccess
{
    public partial class NewsDataAccess : INews<NewsModel>
    {
        protected IAppSettings appSettings;
        public NewsDataAccess(IAppSettings AppSettings)
        {
            appSettings = AppSettings;
        }

        public Task<bool> DeleteAsync(NewsModel parameter)
        {
            throw new NotImplementedException();
        }
        public Task<bool> InsertAsync(NewsModel parameter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(NewsModel parameter)
        {
            throw new NotImplementedException();
        }
        public async Task<List<NewsModel>> GetDataAsync(EnumParam enumParam, params object[] parameters)
        {
            List<NewsModel> newsModels = null;
            switch (enumParam)
            {
                case EnumParam.byId:
                    break;
                case EnumParam.byName:
                    break;
                case EnumParam.byIdAndName:
                    await getByIdAndName(newsModels);
                    break;
                default:
                    break;
            }
            return newsModels;
        }

        private async Task getByIdAndName(List<NewsModel> newsModels)
        {
            try
            {
                using (NpgsqlConnection pgConn = new NpgsqlConnection(appSettings.connectionString))
                {
                    await pgConn.OpenAsync();
                    var sql = @"
                    SELECT ur.id, u.username,u.is_active,r.role_name
                    from user_roles ur
                    inner join users u on u.id = ur.user_id
                    inner join roles r on r.id = ur.role_id
                    where u.username=@username and u.password=@password";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, pgConn);
                    //cmd.Parameters.AddWithValue("@userName", username);
                    //cmd.Parameters.AddWithValue("@password", password);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            newsModels = new List<NewsModel>();

                            while (await rdr.ReadAsync())
                            {
                                NewsModel data = new NewsModel();
                                //data.id = rdr.GetGuid(0);
                                //data.username = rdr.GetString(1);
                                //data.is_active = rdr.GetBoolean(2);
                                //data.role = rdr.GetString(3);

                                //NewsModel.Add(data);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                //TODO : Write log
            }
        }
    }
}
