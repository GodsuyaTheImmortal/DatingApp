using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Create a mapping between the AppUser class and the MemberDto class
            CreateMap<AppUser, MemberDto>()
                // Map the PhotoUrl property in MemberDto to the URL of the main photo (IsMain) from the Photos collection in AppUser
                .ForMember(dest => dest.PhotoUrl,  
                        opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                // Map the Age property in MemberDto to be calculated from the DateOfBirth in AppUser using a CalculateAge() method
                .ForMember(dest => dest.Age, 
                        opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            // Create a simple mapping between the Photo class and the PhotoDto class, where all matching properties will be mapped automatically
            CreateMap<Photo, PhotoDto>();
            
            CreateMap<MemberUpdateDto, AppUser>();

            CreateMap<RegisterDto, AppUser>();
        }

    }
}