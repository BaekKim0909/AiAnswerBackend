using AiAnswerBackend.Dtos.User;
using AiAnswerBackend.Model;

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
}