using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpleBlog.Ui.Interface;
using simpleBlog.Ui.Models;
using simpleBlog.Ui.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var existing = await newsRepo.GetDataAsync(id);
            if (existing is null)
            {
                return this.NotFound();
            }
            await newsRepo.DeleteNewsAsync(id);
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
            var token = ((ClaimsIdentity)User.Identity).Claims.ToList()[3].Value.ToString().Replace("Token: ", string.Empty);
            var post = await newsRepo.GetDataAsync(id, token);
            return post is null ? this.NotFound() : this.View(post.FirstOrDefault());
        }

        [HttpPost, Authorize, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Post post)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(nameof(Edit), post);
            }

            if (post is null)
            {
                throw new ArgumentNullException(nameof(post));
            }
            var token = ((ClaimsIdentity)User.Identity).Claims.ToList()[3].Value.ToString().Replace("Token: ", string.Empty);
            string reporterName = ((ClaimsIdentity)User.Identity).Claims.ToList()[1].Value.ToString();
            bool isSuccess = false;
            if (string.IsNullOrEmpty(post.id))
            {
                isSuccess = await newsRepo.CreateNewsAsync(post, token, reporterName);
            }
            else
            {
                isSuccess = await newsRepo.UpdateNewsAsync(post, token, reporterName);
            }

            return this.Redirect(post.getEncodedLink());

        }
    }
}
