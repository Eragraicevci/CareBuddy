using Core.DTOs;
using Core.Entities;
using API.Extensions;
using AutoMapper;
using Core.Dtos;
using Core.Entities.BookingAggregate;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Service, ServiceDto>()
                .ForMember(d => d.Hospital, o => o.MapFrom(s => s.Hospital.Name))
                .ForMember(d => d.ServiceType, o => o.MapFrom(s => s.ServiceType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ServiceUrlResolver>());

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
            CreateMap<PatientInfoDto, Core.Entities.BookingAggregate.PatientInfo>();
            // CreateMap<PatientInfoDto, PatientInfo>();
            CreateMap<Booking, BookingToReturnDto>()
                .ForMember(d => d.AppointmentType, o => o.MapFrom(s => s.AppointmentType.ShortName))
                .ForMember(
                    d => d.AppointmentTypePrice,
                    o => o.MapFrom(s => s.AppointmentType.Price)
                );

            CreateMap<BookingItem, BookingItemDto>()
                .ForMember(d => d.ServiceId, o => o.MapFrom(s => s.ItemBooked.ServiceItemId))
                .ForMember(d => d.ServiceName, o => o.MapFrom(s => s.ItemBooked.ServiceName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemBooked.PictureUrl));
//  .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

            CreateMap<Message, MessageDto>()
                .ForMember(
                    d => d.SenderPhotoUrl,
                    o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url)
                )
                .ForMember(
                    d => d.RecipientPhotoUrl,
                    o => o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url)
                );
            CreateMap<DateTime, DateTime>()
                .ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
            CreateMap<DateTime?, DateTime?>()
                .ConvertUsing(
                    d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null
                );
        }
    }
}
