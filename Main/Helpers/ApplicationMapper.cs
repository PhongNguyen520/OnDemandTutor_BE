﻿using AutoMapper;
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
            #endregion
        }
    }
}
