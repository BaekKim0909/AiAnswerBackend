using System.Text.Json;
using AiAnswerBackend.Dtos.UserAnswer;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Mappers;
using AiAnswerBackend.Model;
using AiAnswerBackend.Scoring;
using AiAnswerBackend.Vo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiAnswerBackend.Controllers
{
    [Route("api/user_answer")]
    [ApiController]
    public class UserAnswerController : ControllerBase
    {
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAppRepository _appRepository;
        private readonly ScoringStrategyContext _scoringStrategyContext;

        public UserAnswerController(IUserAnswerRepository userAnswerRepository, IAppRepository appRepository, ScoringStrategyContext scoringStrategyContext,IUserRepository userRepository)
        {
            _userAnswerRepository = userAnswerRepository;
            _appRepository = appRepository;
            _scoringStrategyContext = scoringStrategyContext;
            _userRepository = userRepository;
        }

        [Authorize(Policy = "User&Admin")]
        [HttpPost("add")]
        //答题功能
        public async Task<ActionResult<Guid>> AddUserAnswer(UserAnswerAddRequest userAnswerAddRequest)
        {
            UserAnswer userAnswer = new UserAnswer();
            //从请求中获取参数
            if(!Guid.TryParse(userAnswerAddRequest.AppId,out Guid appId))
            {
                return BadRequest("appId格式不正确");
            }

            List<string> choices = userAnswerAddRequest.Choices;
            Guid.TryParse(HttpContext.User.FindFirst("userId").Value, out Guid userId);
            userAnswer.Id = Guid.NewGuid();
            userAnswer.AppId = appId;
            userAnswer.Choices = JsonSerializer.Serialize(userAnswerAddRequest.Choices);
            userAnswer.UserId = userId;
            //判断App是否存在
            var app =await _appRepository.GetAppByIdAsync(appId);
            if (app == null)
            {
                return BadRequest("App不存在");
            }
            //判题
            var userAnswerJudged =  await _scoringStrategyContext.DoScore(choices, app);
            if (userAnswerJudged == null)
            {
                return BadRequest("判题失败");
            }
            userAnswerJudged.Id = userAnswer.Id;
            userAnswerJudged.UserId = userAnswer.UserId;
            userAnswerJudged.CreateTime = DateTime.Now;
            userAnswerJudged.UpdateTime = DateTime.Now;
            var result = await _userAnswerRepository.AddUserAnswerAsync(userAnswerJudged);
            if (!result)
            {
                return BadRequest("判题失败");
            }

            return Ok(userAnswerJudged.Id);
        }
        //根据id获取用户答案
        [Authorize(Policy = "User&Admin")]
        [HttpGet("getVo/{id}")]
        public async Task<ActionResult<UserAnswerVO>> GetUserAnswerVOById([FromRoute]string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("id为空");
            }

            if (!Guid.TryParse(id,out Guid usId))
            {
                return BadRequest("Id格式不正确");
            }

            var userAnswer = await _userAnswerRepository.GetUserAnswerByIdAsync(usId);

            if (userAnswer == null)
            {
                return BadRequest("用户答案不存在");
            }

            var userAnswerVo = userAnswer.ToVoFromUserAnswer();
            userAnswerVo.User = await _userRepository.GetUserInfoByIdAsync(userAnswer.UserId);
            return Ok(userAnswerVo);
        }
    }
}
