using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.DTOS.Outputs.Authentication
{
    public class Auther
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
        public DateTime Expire { get; set; }
    }
}
