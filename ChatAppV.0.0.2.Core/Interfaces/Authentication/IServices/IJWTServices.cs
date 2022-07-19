using ChatAppV._0._0._2.Core.Models.Auithentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Interfaces.Authentication.IServices
{
    public interface IJWTServices
    {
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    }
}
