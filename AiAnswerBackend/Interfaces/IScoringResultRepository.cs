using AiAnswerBackend.Common;
using AiAnswerBackend.Dtos;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AiAnswerBackend.Interfaces;

public interface IScoringResultRepository
{
    //添加评分结果
    public Task<bool> AddScoringResultAsync(ScoringResult scoringResult);
    //根据ID查询评分结果
    public Task<ScoringResult?> GetScoringResultByIdAsync(Guid id);
    //修改评分结果
    public Task<bool> UpdateScoringResultAsync(ScoringResult newScoringResult);
    //根据查询条件查询
    public Task<PageResult<ScoringResult>> GetScoringResultListByQueryAsync(ScoringResultQueryRequest scoringResultQueryRequest);
}