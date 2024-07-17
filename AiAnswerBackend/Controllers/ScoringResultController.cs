using AiAnswerBackend.Common;
using AiAnswerBackend.Dtos;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Mappers;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AiAnswerBackend.Controllers
{
    [Route("api/scoring_result")]
    [ApiController]
    public class ScoringResultController : ControllerBase
    {
        private readonly IScoringResultRepository _scoringResultRepository;
        private readonly IUserRepository _userRepository;

        public ScoringResultController(IScoringResultRepository scoringResultRepository,IUserRepository userRepository)
        {
            _scoringResultRepository = scoringResultRepository;
            _userRepository = userRepository;
        }
        [Authorize(Policy = "User&Admin")]
        [HttpPost("add")]
        public async Task<ActionResult<Guid>> AddScoringResult(ScoringResultAddRequest scoringResultAddRequest)
        {
            if (! Guid.TryParse(scoringResultAddRequest.AppId,out Guid appId))
            {
                return BadRequest("当前AppId无效");
            }
            var userId = HttpContext.User.FindFirst("userId").Value;
            if (!Guid.TryParse(userId, out Guid loginUserId))
            {
                return BadRequest("当前用户ID无效");
            }

            ScoringResult scoringResult = new ScoringResult()
            {
                Id = Guid.NewGuid(),
                ResultName = scoringResultAddRequest.ResultName,
                ResultDesc = scoringResultAddRequest.ResultDesc,
                ResultPicture = scoringResultAddRequest.ResultPicture,
                ResultProp = JsonSerializer.Serialize(scoringResultAddRequest.ResultProp),
                ResultScoreRange = scoringResultAddRequest.ResultScoreRange,
                AppId = appId,
                UserId = loginUserId,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            var result = await _scoringResultRepository.AddScoringResultAsync(scoringResult);
            if (! result)
            {
                return BadRequest("添加失败");
            }
            return Ok(scoringResult.Id);
        }

        [Authorize(Policy = "User&Admin")]
        [HttpPost("edit")]
        public async Task<ActionResult<string>> UpdateScoringResult(ScoringResultUpdateRequest scoringResultUpdateRequest)
        {
            if (!Guid.TryParse(scoringResultUpdateRequest.Id, out Guid id))
            {
                return BadRequest("当前Id无效");
            }

            var oldScoringResult =await _scoringResultRepository.GetScoringResultByIdAsync(id);
            if (oldScoringResult == null)
            {
                return BadRequest("当前得分结果不存在");
            }
            ScoringResult newScoringResult = new ScoringResult()
            {
                Id = id,
                ResultName = scoringResultUpdateRequest.ResultName,
                ResultDesc = scoringResultUpdateRequest.ResultDesc,
                ResultPicture = scoringResultUpdateRequest.ResultPicture,
                ResultProp = JsonSerializer.Serialize(scoringResultUpdateRequest.ResultProp),
                ResultScoreRange = scoringResultUpdateRequest.ResultScoreRange,
            };
            var result = await _scoringResultRepository.UpdateScoringResultAsync(newScoringResult);
            if (!result)
            {
                return BadRequest("添加失败");
            }
            return Ok("添加成功");
        }

        [Authorize(Policy = "User&Admin")]
        [HttpPost("list/page/vo")]
        public async Task<ActionResult<PageResponse<ScoringResultVO>>> ListUserScoringResultVoByPage(ScoringResultQueryRequest scoringResultQueryRequest)
        {
            var userId = HttpContext.User.FindFirst("userId").Value;
            Guid.TryParse(userId, out Guid loginUserId);
            //只能查询当前用户的
            scoringResultQueryRequest.UserId = loginUserId;
            var result = await _scoringResultRepository.GetScoringResultListByQueryAsync(scoringResultQueryRequest);
            List<ScoringResultVO> list = new List<ScoringResultVO>();
            foreach (var item in result.Records)
            {
                var vo = item.ToScoringResultVoFromScoringResult();
                vo.User =await _userRepository.GetUserInfoByIdAsync(item.UserId);
                list.Add(vo);
            }

            return Ok(new PageResponse<ScoringResultVO>(result.Total, list));
        }
    }
}
