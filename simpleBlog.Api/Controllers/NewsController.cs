using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpleBlog.Api.BusinessLogic;
using simpleBlog.Api.Interfaces;
using simpleBlog.Api.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace simpleBlog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsBusinessLogic newsService;
        public NewsController(IAppSettings appSettings)
        {
            newsService = new NewsBusinessLogic(appSettings);
        }
        // GET: api/<NewsController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetArticleById([FromBody] NewsModel artikel)
        {
            BaseModel<object> rspList = new BaseModel<object>();
            try
            {
                var handler = new JwtSecurityTokenHandler();
                string authHeader = Request.Headers["Authorization"];
                authHeader = authHeader.Replace("Bearer ", "");
                var jsonToken = handler.ReadToken(authHeader);
                var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                var id = tokenS.Claims.First(claim => claim.Type == "id").Value;

                var data = await newsService.GetDataById(artikel.artikelId);
                if (data.reporterId.ToString() == id)
                {
                    rspList.data = data;
                }
            }
            catch (Exception ex)
            {
                rspList.status = StatusEnum.error.ToString();
                rspList.data = ex.Message;// Here is one return type
            }
            return Ok(rspList);
        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NewsController>
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

        // PUT api/<NewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
