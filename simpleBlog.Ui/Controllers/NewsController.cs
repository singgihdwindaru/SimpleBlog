using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpleBlog.Ui.Interface;
using simpleBlog.Ui.Models;
using simpleBlog.Ui.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
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

        [Route("/[controller]/MyPosts/")]
        [HttpGet, Authorize]
        public async Task<IActionResult> MyPosts()
        {
            string reporterName = ((ClaimsIdentity)User.Identity).Claims.ToList()[1].Value.ToString();

            var post = await newsRepo.GetAllAsync();
            post = post.Where(c => c.reporterName.ToString() == reporterName);
            return View(post);
        }

        [Route("/[controller]/Delete/{id}")]
        [HttpPost, Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await newsRepo.GetDataByIdAsync(id);
            if (existing is null)
            {
                return this.NotFound();
            }
            var token = ((ClaimsIdentity)User.Identity).Claims.ToList()[3].Value.ToString().Replace("Token: ", string.Empty);

            await newsRepo.DeleteNewsAsync(id, token);
            return RedirectToAction("MyPosts");
        }

        [Route("/[controller]/edit/{id?}")]
        [HttpGet, Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.View(new Post());
            }
            var token = ((ClaimsIdentity)User.Identity).Claims.ToList()[3].Value.ToString().Replace("Token: ", string.Empty);
            var post = await newsRepo.GetDataByIdAsync(id, token);
            return post is null ? this.NotFound() : this.View(post.FirstOrDefault());
        }
        [Route("/[controller]/edit/{id?}")]
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
            if (post.artikelId == null || post.artikelId <= 0)
            {
                isSuccess = await newsRepo.CreateNewsAsync(post, token, reporterName);
            }
            else
            {
                isSuccess = await newsRepo.UpdateNewsAsync(post, token, reporterName);
            }

            return RedirectToAction("Post", new { artikelId = post.artikelId });
        }

        [Route("/[controller]/Post/{artikelId}")]
        [HttpGet]
        public async Task<IActionResult> Post(int artikelId)
        {
            var post = await newsRepo.GetDataByIdAsync(artikelId);
            return post is null ? this.NotFound() : View("Post", post.FirstOrDefault());
        }
    }
}
