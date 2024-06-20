using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;

namespace API.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            #region User
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, UserRolesVM>().ReverseMap();

            CreateMap<Account, UserRoles>().ReverseMap();
            CreateMap<UserRoles, UserRolesVM>().ReverseMap();

            CreateMap<Tutor, TutorDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            #endregion
        }
    }
}
