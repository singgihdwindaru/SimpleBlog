using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using simpleBlog.Api.BusinessLogic;
using simpleBlog.Api.DataAccess;
using simpleBlog.Api.Interfaces;
using simpleBlog.Api.Models;
using simpleBlog.Api.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace simpleBlog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserBusinessLogic userService;
        public UserController( IAppSettings configApi)
        {
            userService = new UserBusinessLogic(configApi);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserModel.Request request)
        {
            BaseModel<object> rspList = new BaseModel<object>();
            try
            {
                var data = await userService.Authenticate(request.username, request.password);
                if (data != null)
                {
                    List<UserModel.Response> result = new List<UserModel.Response>();
                    foreach (var item in data)
                    {
                        var Claims = new List<Claim>
                        {
                            new Claim("id", item.id.ToString()),
                            new Claim("roleId",item.roleId.ToString())
                        };

                        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

                        var Token = new JwtSecurityToken(
                            claims: Claims,
                            expires: DateTime.Now.AddHours(1), 
                            signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
                        );
                        item.token = new JwtSecurityTokenHandler().WriteToken(Token);
                        result.Add(item);
                    }

                    rspList.data = result;

                }
                else
                {
                    rspList.data = null;
                }
            }
            catch (Exception ex)
            {
                rspList.status = StatusEnum.error.ToString();
                rspList.data = ex.Message;
            }
            return Ok(rspList);
        }
        
        
    }
}
