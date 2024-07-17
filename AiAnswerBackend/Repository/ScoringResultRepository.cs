using AiAnswerBackend.Common;
using AiAnswerBackend.Data;
using AiAnswerBackend.Dtos;
using AiAnswerBackend.Interfaces;
using AiAnswerBackend.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AiAnswerBackend.Repository;

public class ScoringResultRepository:IScoringResultRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ScoringResultRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> AddScoringResultAsync(ScoringResult scoringResult)
    {
        await _applicationDbContext.ScoringResults.AddAsync(scoringResult);
        int result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return true;
        }

        return false;
    }

    public async Task<ScoringResult?> GetScoringResultByIdAsync(Guid id)
    {
        var scoringResult = await _applicationDbContext.ScoringResults.FirstOrDefaultAsync(item => item.Id == id);
        return scoringResult;
    }

    public async Task<bool> UpdateScoringResultAsync(ScoringResult newScoringResult)
    {
        var scoringResult =  await _applicationDbContext.ScoringResults.FirstOrDefaultAsync(item => item.Id == newScoringResult.Id);
        if (scoringResult == null)
        {
            return false;
        }
        
        scoringResult.ResultName = newScoringResult.ResultName;
        scoringResult.ResultDesc = newScoringResult.ResultDesc;
        scoringResult.ResultPicture = newScoringResult.ResultPicture;
        scoringResult.ResultProp = newScoringResult.ResultProp;
        scoringResult.ResultScoreRange = newScoringResult.ResultScoreRange;
        scoringResult.UpdateTime = DateTime.Now;

        int result =  await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return true;
        }

        return false;
    }

    public async Task<PageResult<ScoringResult>> GetScoringResultListByQueryAsync(
        ScoringResultQueryRequest scoringResultQueryRequest)
    {
        IQueryable<ScoringResult> scoringResults = _applicationDbContext.ScoringResults;
        if (scoringResultQueryRequest.AppId != null)
        {
            scoringResults = scoringResults.Where(item => item.AppId == scoringResultQueryRequest.AppId);
        }

        if (scoringResultQueryRequest.UserId != null)
        {
            scoringResults = scoringResults.Where(item => item.UserId == scoringResultQueryRequest.UserId);
        }

        //查询到的总数
        long total = await scoringResults.LongCountAsync();
        List<ScoringResult> list = await scoringResults
            .Skip((scoringResultQueryRequest.PageIndex - 1) * scoringResultQueryRequest.PageSize)
            .Take(scoringResultQueryRequest.PageSize).ToListAsync();
        return new PageResult<ScoringResult>(total, list);
    }
}