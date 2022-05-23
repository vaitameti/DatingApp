using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year-dob.Year;
            if(dob.Date > today.AddYears(-age) ) age--; //add years will substract the age year and it will chechk if the date is bigger or smaller than days 
            return age;
        }
        
    }
}