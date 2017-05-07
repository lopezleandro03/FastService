using System;

namespace FastService.Helpers
{
    public static class DateHelper
    {
        public static DateTime ParseJSDate(string strDate)
        {
            var date = new DateTime(Convert.ToInt16(strDate.Split('/')[2])
                , Convert.ToInt16(strDate.Split('/')[0].TrimStart('0'))
                , Convert.ToInt16(strDate.Split('/')[1].TrimStart('0')));

            return date;
        }
    }
}