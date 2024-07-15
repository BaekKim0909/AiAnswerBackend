using AiAnswerBackend.Common;
using AiAnswerBackend.Dtos.App;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Mappers;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AiAnswerBackend.Controllers
{
    [Route("api/app")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IAppRepository _appRepository;

        public AppController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        [Authorize(Policy = "User&Admin")]
        [HttpPost("addApp")]
        //创建App
        public async Task<ActionResult<string>> AddAppAsync(AppAddRequest appAddRequest)
        {
            string appName = appAddRequest.AppName;
            if (string.IsNullOrWhiteSpace(appName))
            {
                return BadRequest("应用名不能为空");
            }

            string createUserId = User.FindFirst("userId").Value;
            App app = appAddRequest.ToAppFromAppAddRequest();
            app.CreateUserId = new Guid(createUserId);
            bool result = await _appRepository.AddAppAsync(app);
            if (!result)
            {
                return BadRequest("添加应用失败");
            }

            return Ok("添加成功");
        }
        
        [Authorize(Policy = "Admin")]
        [HttpPost("reviewApp")]
        //管理员审核
        public async Task<ActionResult<string>> ReviewAppAsync(ReviewRequest appReviewRequest)
        {
            if (string.IsNullOrWhiteSpace(appReviewRequest.Id))
            {
                return BadRequest("id不能为空");
            }

            if (!Guid.TryParse(appReviewRequest.Id,out Guid AppId))
            {
                return BadRequest("id格式不正确");
            }
            var oldApp = await _appRepository.GetAppByIdAsync(AppId);
            if (oldApp == null)
            {
                return BadRequest("应用不存在");
            }

            if (oldApp.ReviewStatus == appReviewRequest.ReviewStatus)
            {
                return BadRequest("请勿重复提交");
            }

            string id = HttpContext.User.FindFirst("userId").Value;
            Guid reviwerId = new Guid(id);
            bool result = await _appRepository.DoReviewAsync(appReviewRequest, reviwerId);
            if (!result)
            {
                return BadRequest("审核失败");
            }

            return Ok("审核成功");
        }
        //分页获取应用列表(封装类)
        [HttpPost("list/page/vo")]
        public async Task<ActionResult<PageResponse<AppVO>>> ListAppVOByPage(AppQueryRequest appQueryRequest)
        {
            //只能看到已过审的数据
            appQueryRequest.ReviewStatus = 1;
            //查询数据库
            var appPage = await _appRepository.GetAppsByQueryAsync(appQueryRequest);
            var appList = appPage.Records;
            //创建分页返回值
            PageResponse<AppVO> pageResponse = new PageResponse<AppVO>();
            pageResponse.Total = appPage.Total;
            foreach (var app in appList)
            {
                var appVo = app.ToAppVOFromApp();
                var user = await _appRepository.GetUserByCreateUserIdAsync(app.CreateUserId);
                var userVo = user.ToUserVOFromUser();
                appVo.UserVo = userVo;
                pageResponse.Records.Add(appVo);
            }

            return Ok(pageResponse);
        }
        //获取单个App的信息(VO)
        [HttpGet("getAppVo/{id}")]
        public async Task<ActionResult<AppVO>> GetAppVoById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("id不能为空");
            }

            if (!Guid.TryParse(id,out Guid appId))
            {
                return BadRequest("id格式错误");
            }
            var app = await _appRepository.GetAppByIdAsync(appId);
            if (app == null)
            {
                return BadRequest("应用不存在");
            }
            var appVo = app.ToAppVOFromApp();
            var user = await _appRepository.GetUserByCreateUserIdAsync(app.CreateUserId);
            var userVo = user.ToUserVOFromUser();
            appVo.UserVo = userVo;
            return Ok(appVo);
        }
    }
}
