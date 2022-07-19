using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.DTOS.Inputs.Authentication
{
    public class LoginDto
    {
        [MaxLength(100)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
