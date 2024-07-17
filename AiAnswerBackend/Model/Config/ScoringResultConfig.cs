using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiAnswerBackend.Model.Config;

public class ScoringResultConfig:IEntityTypeConfiguration<ScoringResult>
{
    public void Configure(EntityTypeBuilder<ScoringResult> builder)
    {
        //设置默认值
        builder.Property(item => item.IsDelete).HasDefaultValue(0);
        //设置索引
        builder.HasIndex(item => item.AppId);
        //设置过滤期
        builder.HasQueryFilter(item => item.IsDelete == 0);
    }
}