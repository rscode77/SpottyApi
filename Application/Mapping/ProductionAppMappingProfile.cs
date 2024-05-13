using Application.ApplicationUser;
using ApplicationUser.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class ProductionAppMappingProfile : Profile
    {
        public ProductionAppMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<LoginDto, User>();
            CreateMap<RegisterDto, User>();
            CreateMap<ActivateUserAccountDto, User>();
            CreateMap<UserActivity, UserActivityDto>().ReverseMap();
            CreateMap<AddEventDto, Event>();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.EventParticipants, opt => opt.MapFrom(src => src.EventParticipant));
            CreateMap<EventParticipant, EventParticipantDto>()
               .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
            CreateMap<UpdateUserDto, User>()
                .ForMember(
                    dest => dest.UserProfiles,
                    opt => opt.MapFrom(src => new UserProfiles { AvatarUrl = src.AvatarUrl })
                );
            CreateMap<UserActivity, UserActivityDto>().ReverseMap();

            CreateMap<User, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}