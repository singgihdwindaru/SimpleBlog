using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using simpleBlog.Api.DataAccess;
using simpleBlog.Api.Interfaces;
using simpleBlog.Api.Models;

namespace simpleBlog.Api.BusinessLogic
{
    public class NewsBusinessLogic
    {
        INews<NewsModel> news;

        public NewsBusinessLogic(IAppSettings AppSettings)
        {
            news = new NewsDataAccess(AppSettings);
        }
        public async Task<NewsModel> GetDataById(int idArticle)
        {
            var data = await news.GetDataAsync(EnumParam.byId, idArticle);
            if (data != null)
            {
                return data.FirstOrDefault();
            }
            return new NewsModel();
        }

    }
}
