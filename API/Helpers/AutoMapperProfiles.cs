using Core.DTOs;
using Core.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(
                    dest => dest.Photo,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url)
                )
                .ForMember(
                    dest => dest.AnalysisResultFile,
                    opt =>
                        opt.MapFrom(
                            src => src.AnalysisResultFiles.FirstOrDefault(x => x.IsMainPDF).Url
                        )
                )
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())
                );
            CreateMap<Photo, PhotoDto>();
            CreateMap<MedicalExpertise, MedicalExpertiseDTO>();
            CreateMap<Address, AddressDTO>();
            CreateMap<AnalysisResultFile, AnalysisResultFileDTO>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(
                    d => d.SenderPhotoUrl,
                    o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url)
                )
                .ForMember(
                    d => d.RecipientPhotoUrl,
                    o => o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
                CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
            CreateMap<DateTime?, DateTime?>().ConvertUsing(d => d.HasValue ? 
                DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);
                  
        }
    }
}
