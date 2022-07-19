using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.DTOS.Inputs.Authentication
{
    public class UpdateImageDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
