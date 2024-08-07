using System.IdentityModel.Tokens.Jwt;
using AiAnswerBackend.Common;
using AiAnswerBackend.Dtos.User;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Model;
using AiAnswerBackend.Utils;
using AiAnswerBackend.Vo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AiAnswerBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //用户注册
        [HttpPost("register")]
        public async Task<ActionResult<string>> UserRegister(UserRegisterRequest? userRegister)
        {
            string userAccount = userRegister.UserAccount;
            string userPassword = userRegister.UserPassword;
            string checkedPassword = userRegister.CheckedPassword;
            if (string.IsNullOrWhiteSpace(userAccount) || string.IsNullOrWhiteSpace(userPassword))
            {
                return BadRequest("用户名或密码不能为空");
            }

            if (userRegister.UserPassword != userRegister.CheckedPassword)
            {
                return BadRequest("两次输入的密码不一致");
            }
            var existingUser = await _userRepository.GetUserByUserAccountAsync(userRegister.UserAccount);

            if (existingUser != null)
            {
                return BadRequest("用户已存在");
            }

            var user = await _userRepository.UserRegisterAsync(userRegister);

            return Ok("注册成功");
        }
        
        //用户登录
        [HttpPost("login")]
        public async Task<ActionResult<string>> UserLogin(UserLoginRequest userLoginRequest)
        {
            if (String.IsNullOrEmpty(userLoginRequest.UserAccount)  || String.IsNullOrEmpty(userLoginRequest.UserPassword))
            {
                return BadRequest("请输入用户名或密码");
            }

            var user = await _userRepository.GetUserByUserAccountAsync(userLoginRequest.UserAccount);
            if (user == null)
            {
                return NotFound("用户不存在");
            }
            
            if (! userLoginRequest.UserPassword.Equals(user.UserPassword))
            {
                return NotFound("密码错误");
            }
            return JwtUtils.GenerateToken(user.Id,user.UserAccount, user.UserRole);
        }
        
        //获取当前登录用户信息
        [Authorize(Policy = "User&Admin")]
        [HttpGet("userInfo")]
        public async Task<ActionResult<UserVO>> GetLoginUser()
        {
            string userIdString = HttpContext.User.FindFirst("userId").Value;
            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                // 处理无法转换为 Guid 的情况
                return BadRequest("无效的用户 ID");
            } 
            var userVo= await _userRepository.GetUserInfoByIdAsync(userId);
            if (userVo == null)
            {
                return BadRequest("尚未登录");
            }
            return Ok(userVo);
        }
        [Authorize(Policy = "User")]
        [HttpGet("test")]
        public async Task<string> Test()
        {
            var userRole = User.FindFirst("userRole")?.Value;
            var userId = User.FindFirst("userId")?.Value;
            return "OK"+userRole+userId;
        }
        //管理员：查询所有用户信息
        [Authorize(Policy = "Admin")]
        [HttpPost("users")]
        public async Task<ActionResult<PageResponse<User>>> GetAllUser(UserQueryRequest userQueryRequest)
        {
            if (! string.IsNullOrWhiteSpace(userQueryRequest.Id))
            {
                if (! Guid.TryParse(userQueryRequest.Id,out Guid id))
                {
                    return BadRequest("Id格式错误");
                }
            }
            var pageUser =await _userRepository.GetUsersByQueryAsync(userQueryRequest);
            var page = new PageResponse<User>(pageUser.Total,pageUser.Records);
            return Ok(page);
        }
        //管理员:删除用户
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUserById([FromRoute] Guid id)
        {
            var result = await _userRepository.DeleteUserByIdAsync(id);
            if (result)
            {
                return Ok("删除成功！！！！");
            }
            return NoContent();
        }
    }
}
