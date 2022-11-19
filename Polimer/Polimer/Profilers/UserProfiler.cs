using AutoMapper;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;

namespace Polimer.App.Profilers
{
    internal class UserProfiler : Profile
    {
        public UserProfiler()
        {
            MapUserModelToUserentity();
            MapUserEntityToModel();
            MapModelToModel();
        }

        private void MapModelToModel()
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

        private void MapUserEntityToModel()
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

        private void MapUserModelToUserentity()
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
