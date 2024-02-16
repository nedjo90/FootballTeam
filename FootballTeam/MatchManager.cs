namespace FootballTeam;

public static class MatchManager
{
    private static Random _random = new Random(); 
    public static Match GenerateRandomMatch()
    {
        Club home = DataManager.RandomClub();
        Club visitor = DataManager.RandomClub();
        while (visitor.Name.Equals(home.Name))
            visitor = DataManager.RandomClub();
        Match testMatch = new Match(home, visitor);
        testMatch.PlayGame();
        testMatch.Display();
        return testMatch;
    }
    public static List<Player> GenerateRandomTeamForGame(Club club)
    {
        List<Player> listOfTeam = new List<Player>();
        List<Player> listOfPlayerPlayable = CreateListOfPlayerPlayable(club);
        while (listOfTeam.Count <= 11 && listOfTeam.Count <= listOfPlayerPlayable.Count)
        {
            Player selectedPlayer = listOfPlayerPlayable[_random.Next(listOfPlayerPlayable.Count)];
            if (!listOfTeam.Contains(selectedPlayer))
                listOfTeam.Add(selectedPlayer);
        }
        return listOfTeam;
    }

    public static void UpdatePlayerDataByMatch(Club club)
    {
        foreach (Player player in club.ListOfPlayers)
        {
            if (player.DaysOfSuspension > 0)
                player.DaysOfSuspension--;
            if (player.DaysOfInjury > 0)
                player.DaysOfInjury--;
        }
    }
    public static List<Player> CreateListOfPlayerPlayable(Club club)
    {
        List<Player> listOfPlayerPlayable = new List<Player>();
        foreach (Player player in club.ListOfPlayers)
        {
            if (player.DaysOfSuspension == 0 && player.DaysOfInjury == 0)
                listOfPlayerPlayable.Add(player);
        }
        return listOfPlayerPlayable;
    }
    
    public static List<Player> CreateListOfReserve(Club club, List<Player> teamActuallyPlaying)
    {
        List<Player> listOfReserve = new List<Player>();
        foreach (Player player in club.ListOfPlayers)
        {
            if (player.DaysOfSuspension == 0 && player.DaysOfInjury == 0 && !teamActuallyPlaying.Contains(player))
                listOfReserve.Add(player);
        }
        return listOfReserve;
    }
    
    

    public static Player GenerateRandomScorerFromTeam(List<Player> team)
    {
        Player scorer = team[_random.Next(team.Count)];
        return scorer;
    }
    
    
    
}