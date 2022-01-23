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
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<IActionResult> GetArticleById([FromBody] NewsRequestModel request)
        {
            BaseModel<object> rspList = new BaseModel<object>();
            try
            {
                var handler = new JwtSecurityTokenHandler();
                string authHeader = Request.Headers["Authorization"];
                authHeader = authHeader.Replace("Bearer ", "");
                var jsonToken = handler.ReadToken(authHeader);
                var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                var reporterId = tokenS.Claims.First(claim => claim.Type == "id").Value;
                var roleId = tokenS.Claims.First(claim => claim.Type == "roleId").Value;

                var data = await newsService.GetDataById(request.artikelId);
                if (data != null)
                {
                    rspList.data = data;
                    foreach (var item in data)
                    {
                        if (item.reporterId.ToString() != reporterId)
                        {
                            rspList.data = null;
                            break;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                rspList.status = StatusEnum.error.ToString();
                rspList.data = ex.Message;
            }
            return Ok(rspList);
        }

        [Route("[action]")]
        [HttpPost, Authorize]
        public async Task<IActionResult> PostNews([FromBody] NewsModel request)
        {
            BaseModel<object> rspList = new BaseModel<object>();
            try
            {
                var handler = new JwtSecurityTokenHandler();
                string authHeader = Request.Headers["Authorization"];
                authHeader = authHeader.Replace("Bearer ", "");
                var jsonToken = handler.ReadToken(authHeader);
                var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                var reporterId = tokenS.Claims.First(claim => claim.Type == "id").Value;
                var roleId = tokenS.Claims.First(claim => claim.Type == "roleId").Value;
                
                request.reporterId = Convert.ToInt32(reporterId);
                var data = await newsService.PostData(request);
                if (data == null)
                {
                    rspList.status = StatusEnum.fail.ToString();
                }

                rspList.data = request;
            }
            catch (System.Exception ex)
            {
                rspList.status = StatusEnum.error.ToString();
                rspList.data = ex.Message;
            }
            return Ok(rspList);
        }
    }
}
