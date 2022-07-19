using AutoMapper;
using ChatAppV._0._0._2.Core.DTOS.Inputs;
using ChatAppV._0._0._2.Core.DTOS.Inputs.FriendRequist;
using ChatAppV._0._0._2.Core.DTOS.Outputs.Authentication;
using ChatAppV._0._0._2.Core.DTOS.Outputs.User;
using ChatAppV._0._0._2.Core.Models.Auithentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>().ForMember(src => src.Gender, opt => opt.Ignore());
            CreateMap<ApplicationUser, DisplayUserDto>();
            CreateMap<ApplicationUser, DisplayUserFrindRequistDto>().ForMember(src => src.Id, opt => opt.Ignore()); ;
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
