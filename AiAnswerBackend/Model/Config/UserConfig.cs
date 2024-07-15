using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiAnswerBackend.Model.Config;

public class UserConfig:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //设置默认值
        builder.Property(item => item.IsDelete).HasDefaultValue(0);
        builder.Property(item => item.CreateTime).HasDefaultValueSql("CURRENT_TIMESTAMP");
        //过滤器
        builder.HasQueryFilter(item => item.IsDelete == 0);
        
    }
}