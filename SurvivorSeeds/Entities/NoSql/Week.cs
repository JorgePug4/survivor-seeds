using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurvivorSeeds.Entities.NoSql
{
    internal class Week
    {

        [JsonPropertyName("weekId")]
        public string WeekId { get; set; }

        [JsonPropertyName("numberWeek")]
        public int NumberWeek { get; set; }

        [JsonPropertyName("season")]
        public string Season { get; set; }

        [JsonPropertyName("period")]
        public Period Period { get; set; }

        [JsonPropertyName("display")]
        public Display Display { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }

    public class Period
    {

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
    }

    public class Display
    {

        [JsonPropertyName("dateString")]
        public string DateString { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
    }

    public class Status
    {

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }

}
