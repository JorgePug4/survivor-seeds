using System.Text.Json.Serialization;

namespace SurvivorSeeds.Entities;

public class DataInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("year")]
    public int Year { get; set; }
    [JsonPropertyName("weeks")]
    public List<Weeks> Weeks { get; set; } =[];
}

public class Weeks
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("sequence")]
    public int Sequence { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("games")]
    public List<Games> Games { get; set; } =[];
}

public class Games
{

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("scheduled")]
    public DateTime Scheduled { get; set; }
    [JsonPropertyName("venue")]
    public Venue Venue { get; set; }
    [JsonPropertyName("home")]
    public Home Home { get; set; }
    [JsonPropertyName("away")]
    public Away Away { get; set; }
}

public class Venue
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;
}

public class Home
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("alias")]
    public string Alias { get; set; } = string.Empty;
}

public class Away
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("alias")]
    public string Alias { get; set; } = string.Empty;
}