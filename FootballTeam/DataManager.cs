using System.Globalization;

namespace FootballTeam;

public static class DataManager
{
    public static  List<Player> ListOfPlayer = new List<Player>();
    public static List<Club> ListOfClub = new List<Club>();
    public static string Path = "../../../../player.csv";
    public static DateTime Date { get; set; }
    
    
    public static void Initializer()
    {
        string[] importCsv = File.ReadAllLines(Path);
        Date = DateTime.Now;
        foreach (string data in importCsv)
        {
            Player newPlayer = new Player();
            string[] playerDataSplit = data.Split(",");
             newPlayer.Name = playerDataSplit[0];
             newPlayer.Nationality = playerDataSplit[1];
             newPlayer.NationalPosition = playerDataSplit[2];
             newPlayer.NationalKit = ConvertToDouble(playerDataSplit[3]);
             newPlayer.ListOfClub = new List<Club>();
             if (ClubExist(playerDataSplit[4]))
             {
                Club club =  SearchClubByName(playerDataSplit[4]);
                newPlayer.ListOfClub.Add(club); 
                club.ListOfPlayers.Add(newPlayer);
             }
            else
            {
                Club newClub = new Club(playerDataSplit[4]);
                ListOfClub.Add(newClub);
                newPlayer.ListOfClub.Add(newClub);
                newClub.ListOfPlayers.Add(newPlayer);
            }
             newPlayer.ClubPosition = playerDataSplit[5];
             newPlayer.ClubKit = ConvertToDouble(playerDataSplit[6]);
             newPlayer.ClubJoining = playerDataSplit[7];
             if (DateTime.TryParse(playerDataSplit[8], CultureInfo.InvariantCulture,
                     out DateTime result))
                 newPlayer.ContractExpiry = result;
             else
             {
                 newPlayer.ContractExpiry = DateTime.Now;
             }
             newPlayer.Rating = ConvertToInt(playerDataSplit[9]);
             newPlayer.Height = ConvertToInt(playerDataSplit[10]);
             newPlayer.Weight = ConvertToInt(playerDataSplit[11]);
             newPlayer.PreferredFoot = playerDataSplit[12];
             newPlayer.BirthDate = ConvertToDateTime(playerDataSplit[13]);
             newPlayer.Age = ConvertToInt(playerDataSplit[14]);
             newPlayer.PreferredPosition = playerDataSplit[15];
             newPlayer.WorkRate = playerDataSplit[16];
             newPlayer.WeakFoot = ConvertToInt(playerDataSplit[17]);
             newPlayer.SkillMoves = ConvertToInt(playerDataSplit[18]);
             newPlayer.BallControl = ConvertToInt(playerDataSplit[19]);
             newPlayer.Dribbling = ConvertToInt(playerDataSplit[20]);
             newPlayer.Marking = ConvertToInt(playerDataSplit[21]);
             newPlayer.SlidingTackle = ConvertToInt(playerDataSplit[22]);
             newPlayer.StandingTackle = ConvertToInt(playerDataSplit[23]);
             newPlayer.Aggression = ConvertToInt(playerDataSplit[24]);
             newPlayer.Reactions = ConvertToInt(playerDataSplit[25]);
             newPlayer.AttackingPosition = ConvertToInt(playerDataSplit[26]);
             newPlayer.Interceptions = ConvertToInt(playerDataSplit[27]);
             newPlayer.Vision = ConvertToInt(playerDataSplit[28]);
             newPlayer.Composure = ConvertToInt(playerDataSplit[29]);
             newPlayer.Crossing = ConvertToInt(playerDataSplit[30]);
             newPlayer.ShortPass = ConvertToInt(playerDataSplit[31]);
             newPlayer.LongPass = ConvertToInt(playerDataSplit[32]);
             newPlayer.Acceleration = ConvertToInt(playerDataSplit[33]);
             newPlayer.Speed = ConvertToInt(playerDataSplit[34]);
             newPlayer.Stamina = ConvertToInt(playerDataSplit[35]);
             newPlayer.Strength = ConvertToInt(playerDataSplit[36]);
             newPlayer.Balance = ConvertToInt(playerDataSplit[37]);
             newPlayer.Agility = ConvertToInt(playerDataSplit[38]);
             newPlayer.Jumping = ConvertToInt(playerDataSplit[39]);
             newPlayer.Heading = ConvertToInt(playerDataSplit[40]);
             newPlayer.ShotPower = ConvertToInt(playerDataSplit[41]);
             newPlayer.Finishing = ConvertToInt(playerDataSplit[42]);
             newPlayer.LongShots = ConvertToInt(playerDataSplit[43]);
             newPlayer.Curve = ConvertToInt(playerDataSplit[44]);
             newPlayer.FreekickAccuracy = ConvertToInt(playerDataSplit[45]);
             newPlayer.Penalties = ConvertToInt(playerDataSplit[46]);
             newPlayer.Volleys = ConvertToInt(playerDataSplit[47]);
             newPlayer.GkPositioning = ConvertToInt(playerDataSplit[48]);
             newPlayer.GkDiving = ConvertToInt(playerDataSplit[49]);
             newPlayer.GkKicking = ConvertToInt(playerDataSplit[50]);
             newPlayer.GkHandling = ConvertToInt(playerDataSplit[51]);
             newPlayer.GkReflexes = ConvertToInt(playerDataSplit[52]);
             newPlayer.DaysOfSuspension = 0;
             newPlayer.DaysOfInjury = 0;
             ListOfPlayer.Add(newPlayer);
             newPlayer.ListOfMatch = new List<Match>();
        }
    }


    public static Club? SearchClubByName(string name)
    {
        foreach (Club club in ListOfClub)
        {
            if (club.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                return club;
        }
        return null;
    }

    public static bool ClubExist(string name)
    {
        if (SearchClubByName(name) == null)
            return false;
        return true;
    }
    

    public static int ConvertToInt(string valueToConvert)
    {
        if (int.TryParse(valueToConvert, out int integer))
            return integer;
        return 0;
    }
    public static double ConvertToDouble(string valueToConvert)
    {
        if (double.TryParse(valueToConvert, out double doubleNumber))
            return doubleNumber;
        return 0;
    }

    public static DateTime? ConvertToDateTime(string valueToConvert)
    {
        if (DateTime.TryParse(valueToConvert, out DateTime date))
            return date;
        return null;
    }

    
    public static int RatingMeanOfPlayerList(List<Player> listOfPlayer)
    {
        if (listOfPlayer.Count == 0)
            return 0;
        int sum = 0;
        foreach (Player player in listOfPlayer)
            sum += player.Rating;
        return sum / listOfPlayer.Count;
    }
    public static int AgressivityMeanOfPlayerList(List<Player> listOfPlayer)
    {
        if (listOfPlayer.Count == 0)
            return 0;
        int sum = 0;
        foreach (Player player in listOfPlayer)
            sum += player.Aggression;
        return sum / listOfPlayer.Count;
    }

    public static Club RandomClub()
    {
        Random random = new Random();
        Club randomClub = ListOfClub[random.Next(ListOfClub.Count)];
        return randomClub;
    }
    public static Club RandomClub(List<Club> listOfClub)
    {
        Random random = new Random();
        Club randomClub = listOfClub[random.Next(listOfClub.Count)];
        return randomClub;
    }

    public static int RandomNumber(int max)
    {
        Random random = new Random();
        return random.Next(max);
    }
    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public static Player RandomPlayer(List<Player> listOfPlayer)
    {
        int randomPlayerIndex = RandomNumber(listOfPlayer.Count);
        return listOfPlayer[randomPlayerIndex];
    }

    public static void DecrementDaysOfInjuryAndSuspension(Club club)
    {
        foreach (Player player in club.ListOfPlayers)
        {
            if (player.DaysOfInjury > 0)
                player.DaysOfInjury--;
            if (player.DaysOfSuspension > 0)
                player.DaysOfSuspension--;
            {
                
            }
        }
    }
}

