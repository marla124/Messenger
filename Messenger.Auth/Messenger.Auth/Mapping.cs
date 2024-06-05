using AutoMapper;
using Messenger.Auth.Data.Entities;
using Messenger.Auth.Dto;
using Messenger.Auth.Models;
using Messenger.Auth.Models.RequestModel;
namespace Messenger.Auth;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<RegisterModel, UserDto>().ReverseMap();
        CreateMap<LoginModel, UserDto>().ReverseMap();
        CreateMap<UserDto, UserViewModel>().ReverseMap();
        CreateMap<UserDto, RegisterUserRequestViewModel>().ReverseMap();
        CreateMap<UserDto, UpdateUserRequestViewModel>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
    }
}
