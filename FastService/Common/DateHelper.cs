using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace FastService.Common
{
    public class DateHelper
    {
        public string GetDayName(int i)
        {
            CultureInfo spanish = new CultureInfo("es-AR");

            return spanish.DateTimeFormat.DayNames[i];
        }

        internal string GetMonthName(int i)
        {
            if (i == 1)
                return "Enero";

            if (i == 2)
                return "Febrero";

            if (i == 3)
                return "Marzo";

            if (i == 4)
                return "Abril";

            if (i == 5)
                return "Mayo";

            if (i == 6)
                return "Junio";

            if (i == 7)
                return "Julio";

            if (i == 8)
                return "Agosto";

            if (i == 9)
                return "Septiembre";

            if (i == 10)
                return "Octubre";

            if (i == 11)
                return "Noviembre";

            if (i == 12)
                return "Diciembre";

            return "error";
        }
    }
}