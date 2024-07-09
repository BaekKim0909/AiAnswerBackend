using AiAnswerBackend.Dtos.User;
using AiAnswerBackend.Model;
using AiAnswerBackend.Vo;

namespace AiAnswerBackend.Mappers;

public static class UserMapper
{
    public static User ToUserFromUserRegisterRequest(this UserRegisterRequest userRegisterRequest)
    {
        return new User()
        {
            UserAccount = userRegisterRequest.UserAccount,
            UserPassword = userRegisterRequest.UserPassword
        };
    }

    public static UserVO ToUserVOFromUser(this User user)
    {
        return new UserVO()
        {
            UserAccount = user.UserAccount,
            UserName = user.UserName,
            UserAvatar = user.UserAvatar,
            UserProfile = user.UserProfile,
            UserRole = user.UserRole
        };
    }
}