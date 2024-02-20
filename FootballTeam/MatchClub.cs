namespace FootballTeam;

public class MatchClub
{
    public Club Club { get; set; }
    public List<Player> ListOfAllPlayers { get; set; }
    public List<Player> ListOfStartingTeam { get; set; }
    public List<Player> ListOfUnavailablePlayers { get; set; }
    public List<Player> ListOnTheGround { get; set; }
    public List<Player> ListOfReserve { get; set; }
    public List<Player> ListOfOut { get; set; }
    public List<Player> ListOfGoals { get; set; }
    public List<Player> ListOfYellowCards { get; set; }
    public List<Player> ListOfRedCards { get; set; }
    public List<Player> ListOfInjuries { get; set; }

    public MatchClub(Club club)
    {
        Club = club;
        ListOfAllPlayers = CopyAllPlayersAtThatTime();
        ListOfUnavailablePlayers = new();
        ListOfStartingTeam = new();
        ListOnTheGround = new();
        ListOfReserve = new();
        ManageSelection();
        ListOfOut = new();
        ListOfGoals = new();
        ListOfYellowCards = new();
        ListOfRedCards = new();
        ListOfInjuries = new();
    }

    public List<Player> CopyAllPlayersAtThatTime()
    {
        List<Player> listOfPlayers = new List<Player>();
        foreach (Player player in Club.ListOfPlayers)
            listOfPlayers.Add(player);
        return listOfPlayers;
    }

    //The team at the start of the match is random
    public void ManageSelection()
    {
        List<Player> availablePlayers = new List<Player>();
        foreach (Player player in ListOfAllPlayers)
        {
            if (player.DaysOfSuspension == 0 && player.DaysOfInjury == 0)
                availablePlayers.Add(player);
            else
            {
                ListOfUnavailablePlayers.Add(player);
            }
        }

        Random random = new Random();
        while (ListOnTheGround.Count < 11 && availablePlayers.Count > 0)
        {
            Player randomPlayer = availablePlayers[random.Next(availablePlayers.Count)];
            ListOfStartingTeam.Add(randomPlayer);
            ListOnTheGround.Add(randomPlayer);
            availablePlayers.Remove(randomPlayer);
        }

        foreach (Player player in availablePlayers)
        {
            ListOfReserve.Add(player);
        }
    }

    public string MatchPaperEnd()
    {
        string paper = "";

        paper += Club.Name + "\n";
        paper += "Team on the ground\n";
        foreach (Player player in ListOnTheGround)
        {
            paper += player.Name;
            ListOfGoals.ForEach(goal =>
            {
                if (goal.Equals(player)) paper += "⚽️";
            });
            if (!ListOfStartingTeam.Contains(player)) paper += "⬆️";
            if (ListOfInjuries.Contains(player)) paper += "🚑";
            ListOfYellowCards.ForEach(card => { if (card.Equals(player)) paper += "📒"; });
            if (ListOfRedCards.Contains(player)) paper += "📕";
            paper += "\n";
        }

        paper += "Players out\n";
        foreach (Player player in ListOfOut)
        {
            paper += player.Name;
            ListOfGoals.ForEach(goal =>
            {
                if (goal.Equals(player)) paper += "⚽️";
            });
            paper += "⬇️";
            if (!ListOfStartingTeam.Contains(player)) paper += "⬆️";
            if (ListOfInjuries.Contains(player)) paper += "🚑";
            ListOfYellowCards.ForEach(card => { if (card.Equals(player)) paper += "📒"; });
            if (ListOfRedCards.Contains(player)) paper += "📕";
            paper += "\n";
        }

        paper += "Player stayed in reserve\n";
        foreach (Player player in ListOfReserve)
        {
            paper += player.Name += "\n";
        }
        return paper;
    }
    public string MatchPaperStart()
    {
        int n = 1;
        string paper = "";

        paper += Club.Name + "\n";

        paper += "All players of the team\n";
        foreach (Player player in ListOfAllPlayers)
        {
            paper += n + "-" + player.Name + "\n";
            n++;
        }

        n = 1;
        paper += "Team at the start of the match\n";
        
        foreach (Player player in ListOfStartingTeam)
        {
            paper += n + "-" + player.Name + "\n";
            n++;
        }

        n = 1;
        paper += "Team in the reserve\n";
        
        foreach (Player player in ListOfReserve)
        {
            paper += n + "-" + player.Name + "\n";
            n++;
        }

        n = 1;
        paper += "Players unavailable\n";
        
        foreach (Player player in ListOfUnavailablePlayers)
        {
            paper += n + "-" + player.Name + ": ";
            if (player.DaysOfSuspension > 0)
                paper += player.DaysOfSuspension + " day(s) of suspension 📕";
            if (player.DaysOfInjury > 0)
                paper += player.DaysOfInjury + " day(s) of injury 🏥";
            paper += "\n";
            n++;
        }
        return paper;
    }
}