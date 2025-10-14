
using System.Text.Json.Serialization;

namespace SurvivorSeeds.Entities.NoSql
{
    public class Match
    {
        [JsonPropertyName("matchId")]
        public string MatchId { get; set; }
        [JsonPropertyName("location")]
        public Location Location { get; set; }
        [JsonPropertyName("schedule")]
        public Schedule Schedule { get; set; }
        [JsonPropertyName("week")]
        public int Week { get; set; }
        [JsonPropertyName("season")]
        public string Season { get; set; }
        [JsonPropertyName("isQualified")]
        public bool IsQualified { get; set; }
        [JsonPropertyName("teams")]
        public List<Team> Teams { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("venue")]
        public string Venue { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

    public class Schedule
    {
        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; }
        [JsonPropertyName("formattedTime")]
        public string FormattedTime { get; set; }
        [JsonPropertyName("formattedDate")]
        public string FormattedDate { get; set; }
    }

    public class Team
    {
        [JsonPropertyName("teamMatchId")]
        public string TeamMatchId { get; set; }
        [JsonPropertyName("teamId")]
        public int TeamId { get; set; }
        [JsonPropertyName("teamName")]
        public string TeamName { get; set; }
        [JsonPropertyName("score")]
        public int Score { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("result")]
        public int Result { get; set; }
        [JsonPropertyName("isLocal")]
        public bool IsLocal { get; set; }
    }

}
