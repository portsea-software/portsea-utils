using System;

namespace Portsea.Utils.Date
{
    public static class DateTimeExtensions
    {
        public static int AgeInYears(this DateTime dateOfBirth, DateTime on)
        {
            int years = on.Year - dateOfBirth.Year;
            if (on.Month < dateOfBirth.Month)
            {
                years--;
            }
            else if (on.Month == dateOfBirth.Month && on.Day < dateOfBirth.Day)
            {
                years--;
            }

            return years;
        }
    }
}
