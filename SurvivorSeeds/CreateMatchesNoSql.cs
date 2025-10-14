using SurvivorSeeds.Entities;
using SurvivorSeeds.Entities.NoSql;
using SurvivorSeeds.Interfaces;
using System.Text;
using System.Text.Json;

namespace SurvivorSeeds
{
    public class CreateMatchesNoSql : ICreateMatches
    {
        public static List<Entities.Team>? Teams { get; set; }
        public void CrearPartidas()
        {
            string rutaArchivo = "../../../Data/seasson-2025-api.json";
            string rutaArchivoMatches = "../../../Data/NoSql/Matches.json";
            string rutaArchivoWeeks = "../../../Data/NoSql/Weeks.json";

            DataInfo dataSource = LeerJson<DataInfo>(rutaArchivo);

            //CreateWeekJson(rutaArchivoWeeks);
            CreateMatchesJson(rutaArchivoMatches, dataSource);

        }

        private static T LeerJson<T>(string rutaArchivo)
        {
            string json = File.ReadAllText(rutaArchivo);
            return JsonSerializer.Deserialize<T>(json);
        }


        public void CreateWeekJson(string rutaArchivoWeeks)
        {
            var startDate = new DateTime(2025, 9, 4);

            var weeks = Enumerable.Range(0, 18)
                .Select(i =>
                {
                    var dateStart = startDate.AddDays(i * 7);
                    var dateEnd = dateStart.AddDays(6);
                    return new Week
                    {
                        WeekId = $"W_{(i + 1).ToString().PadLeft(2, '0')}",
                        NumberWeek = (i + 1),
                        Season = "2024-2025",
                        Period = new Period { EndDate = dateEnd, StartDate = dateStart },
                        Display = new Display { DateString = Extensions.Extensions.ToDateString(dateStart, dateEnd), ShortName = $"Semana {i}", FullName = $"Semana {i}" },
                        Status = new Status { IsActive = true }
                    };
                })
                .ToList();

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonWees = JsonSerializer.Serialize(weeks, options);

            WriteOrUpdateFile(rutaArchivoWeeks, jsonWees);

        }

        public void CreateMatchesJson(string rutaArchivoMatches, DataInfo dataSource)
        {
            getTeams();
            var matches = new List<Match>();
            int countGame = 1;
            foreach (var week in dataSource.Weeks)
            {
                
                foreach (var game in week.Games)
                {
                    var match = new Match
                    {
                        MatchId = $"Match_{countGame++.ToString().PadLeft(3, '0')}",
                        Week = week.Sequence,
                        Season = dataSource.Year.ToString(),
                        IsQualified = false,
                        Location = new Location
                        {
                            Venue = game.Venue.Name,
                            City = game.Venue.City,
                            State = game.Venue.State, // Assuming state is not provided in the data source
                            Country = "USA" // Assuming all matches are in the USA, adjust as necessary
                        },
                        Schedule = new Schedule
                        {
                            DateTime = game.Scheduled,
                            FormattedTime = game.Scheduled.ToString("HH:mm"),
                            FormattedDate = game.Scheduled.ToString("yyyy-MM-dd")
                        },
                        Teams = new List<Entities.NoSql.Team>
                        {
                           new Entities.NoSql.Team()
                           {
                               TeamMatchId = game.Home.Id,
                               IsLocal = true,
                               Result = 0,
                               Score = 0,
                               Status = "scheduled",
                               TeamId = Teams.Find(x => x.Alias == game.Home.Alias)!.Id,
                               TeamName = Teams.Find(x => x.Alias == game.Home.Alias)!.Name,

                           },
                           new Entities.NoSql.Team()
                           {
                               TeamMatchId = game.Away.Id,
                               IsLocal = false,
                               Result = 0,
                               Score = 0,
                               Status = "scheduled",
                               TeamId = Teams.Find(x => x.Alias == game.Away.Alias)!.Id,
                               TeamName = Teams.Find(x => x.Alias == game.Away.Alias)!.Name,

                           }
                        }

                    };
                    matches.Add(match);
                }
            }
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonMatches = JsonSerializer.Serialize(matches, options);
            WriteOrUpdateFile(rutaArchivoMatches, jsonMatches);
        }


        public static void WriteOrUpdateFile(string filePath, string content, bool append = false, Encoding encoding = null)
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
    }
}