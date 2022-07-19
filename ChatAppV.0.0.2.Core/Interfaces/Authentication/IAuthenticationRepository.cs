using ChatAppV._0._0._2.Core.DTOS.Inputs;
using ChatAppV._0._0._2.Core.DTOS.Inputs.Authentication;
using ChatAppV._0._0._2.Core.DTOS.Outputs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Interfaces.Authentication
{
    public interface IAuthenticationRepository
    {
        Task<Auther> Register(RegisterDto dto);
        Task<Auther> Login(LoginDto dto);
        Task<string> UpdateImage(UpdateImageDto dto);
     

    }
}
