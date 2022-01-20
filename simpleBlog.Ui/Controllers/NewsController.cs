using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpleBlog.Ui.Interface;
using simpleBlog.Ui.Models;
using simpleBlog.Ui.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simpleBlog.Ui.Controllers
{
    public class NewsController : Controller
    {
        private INews<Post> newsRepo;
        public NewsController(IConfigUi config)
        {
            newsRepo = new NewsRepository(config);
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/[controller]/DeleteNews/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteNews(string id)
        {
            var existing = await newsRepo.GetData(id);
            if (existing is null)
            {
                return this.NotFound();
            }
            await newsRepo.DeleteNews(id);
            return Redirect("/");
        }

        [Route("/[controller]/edit/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //var categories = await newsRepo.GetData(id, reporterId);
            if (string.IsNullOrEmpty(id))
            {
                return this.View(new Post());
            }
            var post = await newsRepo.GetData(id);
            return post is null ? this.NotFound() : this.View(post);
        }

    }
}
