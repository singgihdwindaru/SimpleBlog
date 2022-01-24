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

        public async Task<bool> DeleteAsync(NewsModel parameter)
        {
            bool isSucces = false;
            try
            {
                using (NpgsqlConnection pgConn = new NpgsqlConnection(appSettings.connectionString))
                {
                    await pgConn.OpenAsync();
                    var sql = @"
                    DELETE FROM artikel WHERE id=@artikelId and author_id=@reporterId";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, pgConn);
                    cmd.Parameters.AddWithValue("@artikelId", parameter.artikelId);
                    cmd.Parameters.AddWithValue("@reporterId", parameter.reporterId);
                    cmd.Prepare();
                    if ((await cmd.ExecuteNonQueryAsync()) != -1)
                    {
                        isSucces = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                _ = ex.Message;
                //TODO : Write log
            }
            return isSucces;
        }
        public async Task<bool> InsertAsync(NewsModel parameter)
        {
            bool isSucces = false;
            try
            {
                using (NpgsqlConnection pgConn = new NpgsqlConnection(appSettings.connectionString))
                {
                    await pgConn.OpenAsync();
                    var sql = @"
                    INSERT INTO artikel (title, content, pub_date, author_id, status, excerpt, created_date, created_by)
                    VAlUES (@title, @content, @pub_date, @author_id, @status, @excerpt, @created_date, @created_by)";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, pgConn);
                    cmd.Parameters.AddWithValue("@title", parameter.title);
                    cmd.Parameters.AddWithValue("@content", parameter.content);
                    cmd.Parameters.AddWithValue("@pub_date", parameter.pubDate);
                    cmd.Parameters.AddWithValue("@author_id", parameter.reporterId);
                    cmd.Parameters.AddWithValue("@status", parameter.isPublished ? 1 : 0);
                    cmd.Parameters.AddWithValue("@excerpt", parameter.excerpt);
                    cmd.Parameters.AddWithValue("@created_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@created_by", parameter.reporterId);
                    cmd.Prepare();
                    if ((await cmd.ExecuteNonQueryAsync()) != -1)
                    {
                        isSucces = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                _ = ex.Message;
                //TODO : Write log
            }
            return isSucces;
        }

        public async Task<bool> UpdateAsync(NewsModel parameter)
        {
            bool isSucces = false;
            try
            {
                using (NpgsqlConnection pgConn = new NpgsqlConnection(appSettings.connectionString))
                {
                    await pgConn.OpenAsync();
                    var sql = @"
                    UPDATE artikel set title =@title
                    , content=@content
                    , pub_date=@pub_date
                    , author_id=@author_id
                    , status=@status
                    , excerpt=@excerpt
                    , updated_date=@updated_date
                    , updated_by=@updated_by
                    where id =@artikelId";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, pgConn);
                    cmd.Parameters.AddWithValue("@artikelId", parameter.artikelId);
                    cmd.Parameters.AddWithValue("@title", parameter.title);
                    cmd.Parameters.AddWithValue("@content", parameter.content);
                    cmd.Parameters.AddWithValue("@pub_date", parameter.pubDate);
                    cmd.Parameters.AddWithValue("@author_id", parameter.reporterId);
                    cmd.Parameters.AddWithValue("@status", parameter.isPublished ? 1 : 0);
                    cmd.Parameters.AddWithValue("@excerpt", parameter.excerpt);
                    cmd.Parameters.AddWithValue("@updated_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@updated_by", parameter.reporterId);
                    cmd.Prepare();
                    if ((await cmd.ExecuteNonQueryAsync()) != -1)
                    {
                        isSucces = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                _ = ex.Message;
            }
            return isSucces;
        }
        public async Task<List<NewsModel>> GetDataAsync(EnumParam enumParam, params object[] parameters)
        {
            List<NewsModel> newsModels = null;
            switch (enumParam)
            {
                case EnumParam.all:
                    newsModels = await getAll();
                    break;
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
                    , au.id AS reportedId, au.name as reporterName, ar.status as isPublish, ar.excerpt
                    , case when ar.updated_date is null then ar.created_date else ar.updated_date end AS lastModified
                    from artikel ar
                    inner join users au on ar.author_id = au.id
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
                                    lastModified = rdr.IsDBNull(8) ? DateTime.MinValue : rdr.GetDateTime(8),

                                };

                                newsModels.Add(data);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _ = ex.Message;
            }
            return newsModels;
        }
        private async Task<List<NewsModel>> getAll()
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
                    , au.id AS reportedId, au.name as reporterName, ar.status as isPublish, ar.excerpt
                    , case when ar.updated_date is null then ar.created_date else ar.updated_date end AS lastModified
                    from artikel ar
                    inner join users au on ar.author_id = au.id
                    order by ar.pub_date desc";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, pgConn);
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
                                    lastModified = rdr.IsDBNull(8) ? DateTime.MinValue : rdr.GetDateTime(8),

                                };

                                newsModels.Add(data);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _ = ex.Message;
            }
            return newsModels;
        }

    }
}
