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
        public async Task<List<NewsModel>> GetDataById(int artikelId)
        {
            var data = await news.GetDataAsync(EnumParam.byId, artikelId);
            return data;
        }
        public async Task<NewsModel> PostData(NewsModel newsModel)
        {
            bool result;
            if (newsModel.artikelId! <= 0)
            {
                result = await news.InsertAsync(newsModel);
            }
            else
            {
                result = await news.UpdateAsync(newsModel);
            }

            if (result == false)
            {
                return null;
            }
            return newsModel;
        }
        public async Task<List<NewsModel>> GetAll()
        {
            var data = await news.GetDataAsync(EnumParam.all, "");
            return data;
        }
        public async Task<bool> DeleteData(NewsModel newsModel)
        {
            //NewsModel newsModel = new NewsModel();
            //newsModel.artikelId = artikelId;
            bool result = await news.DeleteAsync(newsModel);

            return result;
        }
    }
}
