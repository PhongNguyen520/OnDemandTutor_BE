using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.TutorModel;

namespace API.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            #region User
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, SignUpModerator>().ReverseMap();
            CreateMap<Account, UserRolesVM>().ReverseMap();

            CreateMap<Account, UserRoles>().ReverseMap();
            CreateMap<UserRoles, UserRolesVM>().ReverseMap();

            CreateMap<Tutor, TutorDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();

            CreateMap<Tutor, TutorVM>()
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
                .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education))
                .ForMember(dest => dest.TypeOfDegree, opt => opt.MapFrom(src => src.TypeOfDegree))
                .ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.CardId))
                .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.HourlyRate))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.Headline, opt => opt.MapFrom(src => src.Headline))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Account.FullName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Account.Gender))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Account.Avatar))
                .ForMember(dest => dest.CreateDay, opt => opt.MapFrom(src => src.Account.CreateDay))
                .ReverseMap();

            CreateMap<Account, TutorVM>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))            
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
                .ReverseMap();


            CreateMap<Student,StudentVM>()
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.SchoolName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.IsParent, opt => opt.MapFrom(src => src.IsParent))
                .ForMember(dest => dest.CreateDay, opt => opt.MapFrom(src => src.Account.CreateDay))
                .ReverseMap();

            CreateMap<Account,StudentVM>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
                .ReverseMap();

            CreateMap<Tutor, TutorInterVM>()
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
                .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education))
                .ForMember(dest => dest.TypeOfDegree, opt => opt.MapFrom(src => src.TypeOfDegree))
                .ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.CardId))
                .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.HourlyRate))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.Headline, opt => opt.MapFrom(src => src.Headline))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Account.FullName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Account.Gender))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Account.Avatar))
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.CreateDay, opt => opt.MapFrom(src => src.Account.CreateDay))
                .ForMember(dest => dest.TutorId, opt => opt.MapFrom(src => src.TutorId))
                .ForMember(dest => dest.AccountIsActive, opt => opt.MapFrom(src => src.Account.IsActive))
                .ReverseMap();

            CreateMap<Student, Student10VM>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Account.FullName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Account.Gender))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Account.Avatar))
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.CreateDay, opt => opt.MapFrom(src => src.Account.CreateDay))
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.SchoolName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.IsParent, opt => opt.MapFrom(src => src.IsParent))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.AccountIsActive, opt => opt.MapFrom(src => src.Account.IsActive))
                .ReverseMap();

            CreateMap<Tutor, AccountTutorAdVM>()
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
                .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education))
                .ForMember(dest => dest.TypeOfDegree, opt => opt.MapFrom(src => src.TypeOfDegree))
                .ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.CardId))
                .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.HourlyRate))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.Headline, opt => opt.MapFrom(src => src.Headline))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Account.FullName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Account.Gender))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Account.Avatar))
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.CreateDay, opt => opt.MapFrom(src => src.Account.CreateDay))
                .ForMember(dest => dest.TutorId, opt => opt.MapFrom(src => src.TutorId))
                .ReverseMap();

            CreateMap<TutorAd, TutorAdsModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.Video))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image))
                .ReverseMap();

            CreateMap<TutorAd, TutorIsActiveVM>()
                .ForMember(dest => dest.AdsId, opt => opt.MapFrom(src => src.AdsId))
                .ForMember(dest => dest.CreateDay, opt => opt.MapFrom(src => src.CreateDay))
                .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Video))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.TutorId, opt => opt.MapFrom(src => src.TutorId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => src.IsActived))
                .ForMember(dest => dest.AccountTutorId, opt => opt.MapFrom(src => src.Tutor.AccountId))
                .ForMember(dest => dest.RejectReason, opt => opt.MapFrom(src => src.RejectReason))
                .ReverseMap();

            CreateMap<TutorAd, AdsVMPl>().ReverseMap();


            CreateMap<Complaint, ComplaintDTO>().ReverseMap();
            CreateMap<Complaint, ComplaintVM>().ReverseMap();
            CreateMap<Complaint, ComlaintClass>().ReverseMap();

            CreateMap<Class, ClassVMPhuc>().ReverseMap();

            CreateMap<Feedback, FeedbackVMPhuc>().ReverseMap();

            #endregion
        }
    }
}
