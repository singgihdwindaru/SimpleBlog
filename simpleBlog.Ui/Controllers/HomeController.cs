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
    public class HomeController : Controller
    {
        private INews<Post> newsRepo;
        public HomeController(IConfigUi config)
        {
            newsRepo = new NewsRepository(config);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var post = await newsRepo.GetAllAsync();
            post = post.Where(c => c.isPublished == true);
            return View(post.ToList());
        }
    }
}
