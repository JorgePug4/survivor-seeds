using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivorSeeds.Extensions
{
    public static class Extensions
    {
        public static string ToDateString(DateTime dateStart, DateTime dateEnd)
        {
            var dates = new[] { dateStart, dateEnd };
            var months = dates.Select(d => d.Month).Distinct().ToArray();

            if (months.Length == 1)
            {
                return $"{dateStart.Day} {getMontStr(dateStart.Month)} - {dateEnd.Day}";
            }
            else
            {
                return $"{dateStart.Day} {getMontStr(dateStart.Month)} - {dateEnd.Day} {getMontStr(dateEnd.Month)}";
            }
        }


        private static string getMontStr
            (int month) => month switch
            {
                1 => "ENE.",
                2 => "FEB.",
                3 => "MAR.",
                4 => "ABR.",
                5 => "MAY.",
                6 => "JUN.",
                7 => "JUL.",
                8 => "AGO.",
                9 => "SEP.",
                10 => "OCT.",
                11 => "NOV.",
                12 => "DIC.",
                _ => throw new ArgumentOutOfRangeException(nameof(month), "Invalid month")
            };
    }
}
