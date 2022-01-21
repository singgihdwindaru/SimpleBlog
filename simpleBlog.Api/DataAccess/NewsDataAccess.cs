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
                    int? artikelId = (int)parameters[0];
                    newsModels = await getByIdAndName(artikelId);
                    break;
                case EnumParam.byName:
                    break;
                case EnumParam.byIdAndName:

                    break;
                default:
                    break;
            }
            return newsModels;
        }

        private async Task<List<NewsModel>> getByIdAndName(int? artikelId)
        {
            List<NewsModel> newsModels = null;
            try
            {
                using (NpgsqlConnection pgConn = new NpgsqlConnection(appSettings.connectionString))
                {
                    await pgConn.OpenAsync();
                    var sql = @"
                    SELECT ar.id AS artikelId, ar.title, ar.content
                    , ar.pub_date AS publish_date
                    , au.id AS reportedId, au.nama as reporterName, ar.status as isPublish, ar.excerpt
                    , case when ar.updated_date is null then ar.created_date else ar.updated_date end AS lastModified
                    from artikel ar
                    inner join author au on ar.author_id = au.id
                    where ar.id = @artikelId";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, pgConn);
                    cmd.Parameters.AddWithValue("@artikelId", artikelId);
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            newsModels = new List<NewsModel>();

                            while (await rdr.ReadAsync())
                            {
                                NewsModel data = new NewsModel
                                {
                                    artikelId = rdr.GetInt32(0),
                                    title = rdr.GetString(1),
                                    content = rdr.GetString(2),
                                    pubDate = rdr.GetDateTime(3),
                                    reporterId = rdr.GetInt32(4),
                                    reporterName = rdr.GetString(5),
                                    isPublished = Convert.ToBoolean(rdr.GetInt32(6)),
                                    excerpt = rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7),
                                    lastModified = rdr.IsDBNull(8) ? DateTime.MinValue : rdr.GetDateTime(8)
                                };

                                newsModels.Add(data);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                //TODO : Write log
            }
            return newsModels;
        }
    }
}
