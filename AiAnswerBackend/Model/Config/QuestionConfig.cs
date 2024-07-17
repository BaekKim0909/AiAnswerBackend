using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiAnswerBackend.Model.Config;

public class QuestionConfig:IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        //设置默认值
        builder.Property(item => item.IsDelete).HasDefaultValue(0);
        //设置索引
        builder.HasIndex(item => item.AppId);
        
        //过滤器
        builder.HasQueryFilter(item => item.IsDelete == 0);
    }
}