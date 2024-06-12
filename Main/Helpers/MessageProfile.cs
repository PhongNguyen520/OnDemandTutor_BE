using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageVM>()
                .ForMember(dst => dst.From, opt => opt.MapFrom(x => x.AccountId))
                .ForMember(dst => dst.Room, opt => opt.MapFrom(x => x.ConversationId))
                .ForMember(dst => dst.Avatar, opt => opt.MapFrom(x => x.AccountId))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(x => x.Time))
                .ForMember(dst => dst.Content, opt => opt.MapFrom(x => BasicEmijis.ParseEmojis(x.Description)));
        }
    }
}
