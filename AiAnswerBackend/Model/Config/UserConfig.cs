using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiAnswerBackend.Model.Config;

public class UserConfig:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //设置默认值
        builder.Property(item => item.CreateTime).HasDefaultValue(DateTime.Now);
        builder.Property(item => item.UpdateTime).HasDefaultValue(DateTime.Now);
    }
}