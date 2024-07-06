using System.IdentityModel.Tokens.Jwt;
using AiAnswerBackend.Dtos.User;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Utils;
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
            if (userRegister == null)
            {
                return BadRequest("用户为空");
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
            return JwtUtils.GenerateToken(user.Id, user.UserRole);
        }
        [Authorize(Policy = "user")]
        [HttpGet("test")]
        public async Task<string> Test()
        {
            var userRole = User.FindFirst("userRole").Value;
            var userId = User.FindFirst("userId").Value;
            return "OK"+userRole+userId;
        }
    }
}
