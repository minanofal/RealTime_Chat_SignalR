using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Services
{
    public static class GenderServices
    {
        public static readonly List<string> Gender = new List<string> { "female", "male", "f","m" };

        public static char Services(string gender)
        {

            if (!Gender.Contains(gender.ToLower()))
                return 'W' ;
            if (gender.ToLower() == "male")
                return 'm';
            else if (gender.ToLower() == "female")
                return 'f';
            else
                return gender.ToLower().First();

        }
    }
}
