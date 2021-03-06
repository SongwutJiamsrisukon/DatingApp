using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //<src, dest>
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
                .ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(src => src.DateOfBirth.CalculateAge());
            });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>{
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            }).ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(src => src.DateOfBirth.CalculateAge());
            });
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto,User>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();

            CreateMap<User, LocalUserData>().
            ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            });

            CreateMap<UserForRegisterDto,User>();

            CreateMap<MessageForCreationAndReturnDto,Message>().ReverseMap();

            CreateMap<Message,MessageAndUserDataForReturnDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => {
                opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
                .ForMember(dest => dest.RecipientPhotoUrl, opt => {
                opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url);
            });
        }
    }
}