using AutoMapper;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;

namespace Polimer.App.Profilers
{
    internal class UserProfile : Profile, IAdminEntityProfile
    {
        public UserProfile()
        {
            MapEntityToModel();
            MapModelToEntity();
            MapModelToModel();
        }

        public void MapModelToModel()
        {
            CreateMap<UserModel, UserModel>()
                .ForMember(
                    dest => dest.Id,
                    opt
                        => opt.MapFrom<int>(src => src.Id!.Value)
                )
                .ForMember(
                    dest => dest.Login,
                    opt
                        => opt.MapFrom<string>(src => src.Login ?? string.Empty)
                )
                .ForMember(
                    dest => dest.Password,
                    opt
                        => opt.MapFrom<string>(src => src.Password ?? string.Empty)
                )
                .ForMember(
                    dest => dest.Role,
                    opt
                        => opt.MapFrom<string>(src => src.Role ?? string.Empty)
                );
        }

        public void MapEntityToModel()
        {
            CreateMap<UserEntity, UserModel>()
                .ForMember(
                    dest => dest.Id,
                    opt
                        => opt.MapFrom<int>(src => src.Id)
                )
                .ForMember(
                    dest => dest.Login,
                    opt
                        => opt.MapFrom<string>(src => src.Login ?? string.Empty)
                )
                .ForMember(
                    dest => dest.Password,
                    opt
                        => opt.MapFrom<string>(src => src.Password ?? string.Empty)
                )
                .ForMember(
                    dest => dest.Role,
                    opt
                        => opt.MapFrom<string>(src => src.Role ?? string.Empty)
                );
        }

        public void MapModelToEntity()
        {
            CreateMap<UserModel, UserEntity>()
                .ForMember(
                    dest => dest.Id,
                    opt
                        => opt.MapFrom<int>(src => src.Id!.Value)
                )
                .ForMember(
                    dest => dest.Login,
                    opt
                        => opt.MapFrom<string>(src => src.Login ?? string.Empty)
                )
                .ForMember(
                    dest => dest.Password,
                    opt
                        => opt.MapFrom<string>(src => src.Password ?? string.Empty)
                )
                .ForMember(
                    dest => dest.Role,
                    opt
                        => opt.MapFrom<string>(src => src.Role ?? string.Empty)
                );
        }

    }
}

    
