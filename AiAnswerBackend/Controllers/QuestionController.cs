using System.Text.Json;
using AiAnswerBackend.Dtos.Question;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiAnswerBackend.Controllers
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;

        public QuestionController(IQuestionRepository questionRepository, IUserRepository userRepository)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }

        [Authorize(Policy = "User&Admin")]
        [HttpPost("add")]
        public async Task<ActionResult<Guid>> AddQuestion(QuestionAddRequest questionAddRequest)
        {
            var userId = HttpContext.User.FindFirst("userId").Value;
            if (!Guid.TryParse(userId, out Guid createUserId))
            {
                return BadRequest("当前用户信息异常");
            }

            if (!Guid.TryParse(questionAddRequest.AppId, out Guid appId))
            {
                return BadRequest("当前App数据异常");
            }
            //将选项集合转换为JSON
            var questionContent = JsonSerializer.Serialize(questionAddRequest.QuestionContent);
            Question question = new Question()
            {
                Id = Guid.NewGuid(),
                CreateUserId = createUserId,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                AppId = appId,
                QuestionContent = questionContent
            };
            var result = await _questionRepository.AddQuestionAsync(question);
            if (!result)
            {
                return BadRequest("添加失败");
            }
            return Ok(question.Id);
        }
        [HttpGet("get/vo/{id}")]
        public async Task<ActionResult<QuestionVO>> GetQuestionVoById([FromRoute] string id)
        {
            if(!Guid.TryParse(id, out Guid appId))
            {
                return BadRequest("请求参数异常");
            }

            var question = await _questionRepository.GetQuestionByAddIdAsync(appId);
            if (question == null)
            {
                return BadRequest("题目不存在");
            }
            QuestionVO questionVo = new QuestionVO()
            {
                Id = question.Id.ToString(),
                AppId = question.AppId.ToString(),
                CreateUserId = question.CreateUserId.ToString(),
                CreateTime = question.CreateTime,
                UpdateTime = question.UpdateTime,
                QuestionContent = JsonSerializer.Deserialize<List<QuestionContent>>(question.QuestionContent),
                UserVo = await _userRepository.GetUserInfoByIdAsync(question.CreateUserId)
            };
            return Ok(questionVo);
        }

        [HttpPost("edit")]
        public async Task<ActionResult<bool>> EditQuestion(QuestionEditRequest questionEditRequest)
        {
            if (string.IsNullOrWhiteSpace(questionEditRequest.Id))
            {
                return BadRequest("用户ID不能为空");
            }

            if (!Guid.TryParse(questionEditRequest.Id, out Guid questionId))
            {
                return BadRequest("用户ID格式错误");
            }

            Question question = new Question()
            {
                Id = questionId,
                QuestionContent = JsonSerializer.Serialize(questionEditRequest.QuestionContent)
            };
            //查询数据库，判断数据是否存在
            var oldQuestion = await _questionRepository.GetQuestionByIdAsync(questionId);

            if (oldQuestion == null)
            {
                return BadRequest("问题不存在，修改失败");
            }
            //发起请求用户信息
            var userId = HttpContext.User.FindFirst("userId").Value;
            var userRole = HttpContext.User.FindFirst("userRole").Value;
            //只有题目创建者和管理员才能修改
            if (oldQuestion.CreateUserId.ToString() != userId && !userRole.Equals("Admin"))
            {
                return BadRequest("当前用户无权限修改！！！");
            }

            var result = await _questionRepository.UpdateQuestionAsync(question);
            if (result == false)
            {
                return BadRequest("修改失败,请重试");
            }

            return Ok("修改成功");
        }
    }
}
