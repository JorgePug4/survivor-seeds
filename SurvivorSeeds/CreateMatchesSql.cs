using SurvivorSeeds.Entities;
using SurvivorSeeds.Interfaces;
using System.Text;
using System.Text.Json;

namespace SurvivorSeeds;

public class CreateMatchesSql: ICreateMatches
{

    public static List<Team>? Teams  { get; set; }
    public void CrearPartidas()
    {
        // Definir la ruta del archivo JSON
        string rutaArchivo = "../../../Data/seasson-2025-api.json";
        string rutaArchivoMatches = "../../../Data/Matches.sql";
        string rutaArchivoPlays = "../../../Data/TeamMatches.sql";
        
        
        getTeams();

        // Leer el archivo JSON y deserializarlo a una lista de objetos Partida
        DataInfo dataSource = LeerJson<DataInfo>(rutaArchivo);
        int matchId = 1;

        foreach (var week in dataSource.Weeks)
        {
            foreach (var match in week.Games)
            {
                InsertMatch(match, rutaArchivoMatches, week.Sequence);
                InsertTeamMatches(rutaArchivoPlays, match.Home, match.Away, matchId);

                matchId++;
            }
        }
    }

    private static void InsertTeamMatches(string rutaArchivoPlays, Home matchHome, Away matchAway, int matchId)
    {
        var HomeMatch = new StringBuilder();
        var AwayMatch = new StringBuilder();
        var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        HomeMatch.Append($"INSERT INTO TeamMatches VALUES ({matchId}, {Teams.Find(x => x.Alias == matchHome.Alias).Id},1,0,1,'{now}', NULL,1)"+ Environment.NewLine);
        AwayMatch.Append($"INSERT INTO TeamMatches VALUES ({matchId}, {Teams.Find(x => x.Alias == matchAway.Alias).Id},1,0,0,'{now}', NULL,1)"+ Environment.NewLine);
        
        WriteOrUpdateFile(rutaArchivoPlays, HomeMatch.ToString(), true, Encoding.UTF8);
        WriteOrUpdateFile(rutaArchivoPlays, AwayMatch.ToString(), true, Encoding.UTF8);

    }

    private static void getTeams()
    {
        Teams =
        [
            new() { Id = 1, Name = "Buffalo Bills", Alias = "BUF" },
            new() { Id = 2, Name = "Miami Dolphins", Alias = "MIA" },
            new() { Id = 3, Name = "New England Patriots", Alias = "NE" },
            new() { Id = 4, Name = "New York Jets", Alias = "NYJ" },
            new() { Id = 5, Name = "Baltimore Ravens", Alias = "BAL" },
            new() { Id = 6, Name = "Cincinnati Bengals", Alias = "CIN" },
            new() { Id = 7, Name = "Cleveland Browns", Alias = "CLE" },
            new() { Id = 8, Name = "Pittsburgh Steelers", Alias = "PIT" },
            new() { Id = 9, Name = "Denver Broncos", Alias = "DEN" },
            new() { Id = 10, Name = "Kansas City Chiefs", Alias = "KC" },
            new() { Id = 11, Name = "Las Vegas Raiders", Alias = "LV" },
            new() { Id = 12, Name = "Los Angeles Chargers", Alias = "LAC" },
            new() { Id = 13, Name = "Houston Texans", Alias = "HOU" },
            new() { Id = 14, Name = "Jacksonville Jaguars", Alias = "JAC" },
            new() { Id = 15, Name = "Indianapolis Colts", Alias = "IND" },
            new() { Id = 16, Name = "Tennessee Titans", Alias = "TEN" },
            new() { Id = 17, Name = "Dallas Cowboys", Alias = "DAL" },
            new() { Id = 18, Name = "New York Giants", Alias = "NYG" },
            new() { Id = 19, Name = "Philadelphia Eagles", Alias = "PHI" },
            new() { Id = 20, Name = "Washington Commanders", Alias = "WAS" },
            new() { Id = 21, Name = "Chicago Bears", Alias = "CHI" },
            new() { Id = 22, Name = "Detroit Lions", Alias = "DET" },
            new() { Id = 23, Name = "Green Bay Packers", Alias = "GB" },
            new() { Id = 24, Name = "Minnesota Vikings", Alias = "MIN" },
            new() { Id = 25, Name = "Arizona Cardinals", Alias = "ARI" },
            new() { Id = 26, Name = "Los Angeles Rams", Alias = "LA" },
            new() { Id = 27, Name = "San Francisco 49ers", Alias = "SF" },
            new() { Id = 28, Name = "Seattle Seahawks", Alias = "SEA" },
            new() { Id = 29, Name = "Atlanta Falcons", Alias = "ATL" },
            new() { Id = 30, Name = "Carolina Panthers", Alias = "CAR" },
            new() { Id = 31, Name = "New Orleans Saints", Alias = "NO" },
            new() { Id = 32, Name = "Tampa Bay Buccaneers", Alias = "TB" }
        ];

    }

    private static void InsertMatch(Games match, string rutaArchivoMatches, int idWeek)
    {
        var insertQuery = new StringBuilder();
        var dateTimeScheduled = match.Scheduled.ToString("yyyy-MM-dd HH:mm:ss");
        var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        insertQuery.AppendLine($"INSERT INTO matches VALUES ({idWeek}, '{dateTimeScheduled}',1, '{match.Venue.Name.Replace($"'", "")}', '{now}', NULL, 1 )");
        
        WriteOrUpdateFile(rutaArchivoMatches, insertQuery.ToString(), true, Encoding.UTF8);
    }
    

    private static T LeerJson<T>(string rutaArchivo)
    {
        string json = File.ReadAllText(rutaArchivo);
        return JsonSerializer.Deserialize<T>(json);
    }
    
    public static void WriteOrUpdateFile(string filePath, string content, bool append = true, Encoding encoding = null)
    {
        // Validar parámetros
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("La ruta del archivo no puede ser nula o vacía", nameof(filePath));
        
        if (content == null)
            throw new ArgumentNullException(nameof(content), "El contenido no puede ser nulo");
        
        // Usar UTF-8 como codificación por defecto si no se especifica
        encoding = encoding ?? Encoding.UTF8;
        
        // Crear el directorio si no existe
        string directory = Path.GetDirectoryName(filePath)!;
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        // Escribir en el archivo
        using StreamWriter writer = new StreamWriter(filePath, append, encoding);
        writer.Write(content);
    }
}