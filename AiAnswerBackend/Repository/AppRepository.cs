using AiAnswerBackend.Common;
using AiAnswerBackend.Data;
using AiAnswerBackend.Dtos.App;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AiAnswerBackend.Repository;

public class AppRepository:IAppRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AppRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> AddAppAsync(App app)
    {
        EntityEntry entityEntry =  await _applicationDbContext.Apps.AddAsync(app);
        if (entityEntry.State != EntityState.Added)
        {
            return false;
        }
        await _applicationDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<App?> GetAppByIdAsync(Guid id)
    {
        var app = await _applicationDbContext.Apps.FirstOrDefaultAsync(item => item.Id == id);
        return app;
    }

    public async Task<bool> DoReviewAsync(ReviewRequest reviewRequest,Guid reviewerId)
    {
        Guid id = new Guid(reviewRequest.Id);
        var app = await _applicationDbContext.Apps.FirstOrDefaultAsync(item => item.Id == id);
        app.ReviewStatus = reviewRequest.ReviewStatus;
        app.ReviewTime = DateTime.Now;
        app.ReviewMessage = reviewRequest.ReviewMessage;
        app.ReviewerId = reviewerId;
        await _applicationDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<PageResult<App>> GetAppsByQueryAsync(AppQueryRequest appQueryRequest)
    {
        IQueryable<App> apps = _applicationDbContext.Apps;
        apps = _applicationDbContext.Apps.Where(item => item.ReviewStatus == appQueryRequest.ReviewStatus);
        
        
        
        long total = await apps.LongCountAsync();
        apps = apps.Skip((appQueryRequest.PageIndex - 1) * appQueryRequest.PageSize).Take(appQueryRequest.PageSize);
        var appList = await apps.ToListAsync();
        return new PageResult<App>(total,appList);
    }

    public async Task<User> GetUserByCreateUserIdAsync(Guid id)
    {
        var user =  await _applicationDbContext.Users.FirstOrDefaultAsync(item => item.Id == id);
        return user;
    }

    public async Task<bool> UpdateAppByIdAsync(AppUpdateRequest app)
    {
        var appId = new Guid(app.Id);
        var oldApp = await _applicationDbContext.Apps.FirstOrDefaultAsync(item => item.Id == appId);
        if (oldApp == null)
        {
            return false;
        }

        oldApp.AppName = app.AppName;
        oldApp.AppDesc = app.AppDesc;
        oldApp.AppIcon = app.AppIcon;
        oldApp.AppType = app.AppType;
        oldApp.ScoreStrategy = app.ScoringStrategy;
        
        await _applicationDbContext.SaveChangesAsync();
        
        return true;
    }
}