namespace FootballTeam;

public class Match
{
    public Club Home{ get; private set; }
    public Club Visitor { get; private set; }
    public List<Player> ListOfPlayingTeamHome { get; private set; }
    public List<Player> ListOfReserveHome { get; private set; }
    public List<Player> ListOfPlayerOutHome { get; private set; }
    public List<Player> ListOfPlayingTeamVisitor { get; private set; }
    public List<Player> ListOfReserveVisitor { get; private set; }
    public List<Player> ListOfPlayerOutVisitor { get; private set; }

    public List<Player> ListOfHomeScorer { get; private set; }
    public List<Player> ListOfInjuriedHomePlayer { get; private set; }
    public List<Player> ListOfInjuriedVisitorPlayer { get; private set; }
    public List<Player> ListOfVisitorScorer { get; private set; }
    public List<Player> ListOfYellowCard { get; private set; }
    public List<Player> ListOfRedCard { get; private set; }
    private Random _random = new Random(); 

    public Match(Club home, Club visitor)
    {
        Home = home;
        Home.ListOfMatch.Add(this);
        foreach (Player player in Home.ListOfPlayers)
            player.MatchInClub.Add(this);
        Visitor = visitor;
        Visitor.ListOfMatch.Add(this);
        foreach (Player player in Visitor.ListOfPlayers)
            player.MatchInClub.Add(this);
        ListOfPlayingTeamHome = MatchManager.GenerateRandomTeamForGame(home);
        ListOfPlayingTeamVisitor = MatchManager.GenerateRandomTeamForGame(visitor);
        ListOfReserveHome = MatchManager.CreateListOfReserve(home, ListOfPlayingTeamHome);
        ListOfReserveVisitor = MatchManager.CreateListOfReserve(visitor, ListOfPlayingTeamVisitor);
        ListOfPlayerOutHome = new List<Player>();
        ListOfPlayerOutVisitor = new List<Player>();
        ListOfHomeScorer = new List<Player>();
        ListOfVisitorScorer = new List<Player>();
        ListOfInjuriedHomePlayer = new List<Player>();
        ListOfInjuriedVisitorPlayer = new List<Player>();
        ListOfYellowCard = new List<Player>();
        ListOfRedCard = new List<Player>();
    }
    public void PlayGame()
    {
        MatchManager.UpdatePlayerDataByMatch(Home);
        MatchManager.UpdatePlayerDataByMatch(Visitor);
        int probabilityOfGoalHome = DataManager.RatingMeanOfPlayerList(ListOfPlayingTeamHome);
        int probabilityOfGoalVisitor = DataManager.RatingMeanOfPlayerList(ListOfPlayingTeamVisitor);
        int probabilityOfCardHome = DataManager.AgressivityMeanOfPlayerList(ListOfPlayingTeamHome);
        int probabilityOfCardVisitor = DataManager.AgressivityMeanOfPlayerList(ListOfPlayingTeamVisitor);
        for (int time = 0; time < 90 && ListOfPlayingTeamHome.Count > 0 && ListOfPlayingTeamVisitor.Count > 0; time++)
        {
            int destiny = _random.Next(2000);
            if (destiny <= probabilityOfCardHome)
                GiveRandomCardRandomPlayer(ListOfPlayingTeamHome, time);
            if (destiny <= probabilityOfCardVisitor)
                GiveRandomCardRandomPlayer(ListOfPlayingTeamVisitor, time);
            if (destiny <= probabilityOfGoalHome * 1.2 && destiny % 2 == 0)
                ListOfHomeScorer.Add(MatchManager.GenerateRandomScorerFromTeam(ListOfPlayingTeamHome));
            if (destiny <= probabilityOfGoalVisitor && destiny % 2 != 0)
                ListOfVisitorScorer.Add(MatchManager.GenerateRandomScorerFromTeam(ListOfPlayingTeamVisitor));
            if ((probabilityOfCardHome + probabilityOfGoalHome) % _random.Next(1,10) == 0)
                ListOfInjuriedHomePlayer.Add(GiveRandomInjuryDaysRandomPlayer(ListOfPlayingTeamHome));
            if ((probabilityOfCardVisitor + probabilityOfGoalVisitor) % _random.Next(1, 10) == 0)
                ListOfInjuriedVisitorPlayer.Add(GiveRandomInjuryDaysRandomPlayer(ListOfPlayingTeamVisitor));
            destiny = destiny * 100 / 120;
            if(destiny <= probabilityOfCardHome)
                MakeChangeInPlayingTeam(ListOfPlayingTeamHome);
            if(destiny <= probabilityOfCardVisitor)
                MakeChangeInPlayingTeam(ListOfPlayingTeamVisitor);
        }
    }

    public void GiveRandomCardRandomPlayer(List<Player> team, int time)
    {
        Player randomPlayer = team[_random.Next(team.Count)];
        bool isRedCard = false;
        if (time % 3 == 0)
            isRedCard = true;
        else
        {
            if (ListOfYellowCard.Contains(randomPlayer))
                isRedCard = true;
            ListOfYellowCard.Add(randomPlayer);
        }

        if (isRedCard)
        {
            team.Remove(randomPlayer);
            randomPlayer.DaysOfSuspension = 3;
            ListOfRedCard.Add(randomPlayer);
        }
    }
    
    public void MakeChangeInPlayingTeam(Player playerOut)
    {
        List<Player> listReserve;
        List<Player> listActuallyPlaying;
        List<Player> listOut;
        if (playerOut.Club.Equals(Home.Name))
        {
            listReserve = ListOfReserveHome;
            listActuallyPlaying = ListOfPlayingTeamHome;
            listOut = ListOfReserveHome;
        }
        else
        {
            listReserve = ListOfReserveVisitor;
            listActuallyPlaying = ListOfPlayingTeamVisitor;
            listOut = ListOfReserveVisitor;
        }
        listActuallyPlaying.Remove(playerOut);
        listOut.Add(playerOut);
        if (listReserve.Count != 0)
        {
            Player playerIn = listReserve[_random.Next(listReserve.Count())];
            listActuallyPlaying.Add(playerIn);
            listReserve.Remove(playerIn);
        }
    }
    
    public void MakeChangeInPlayingTeam(List<Player> listActuallyPlaying)
    {
        Player playerOut = listActuallyPlaying[_random.Next(listActuallyPlaying.Count)];
        List<Player> listReserve;
        List<Player> listOut;
        if (playerOut.Club.Equals(Home.Name))
        {
            listReserve = ListOfReserveHome;
            listActuallyPlaying = ListOfPlayingTeamHome;
            listOut = ListOfReserveHome;
        }
        else
        {
            listReserve = ListOfReserveVisitor;
            listActuallyPlaying = ListOfPlayingTeamVisitor;
            listOut = ListOfReserveVisitor;
        }
        listActuallyPlaying.Remove(playerOut);
        listOut.Add(playerOut);
        if (listReserve.Count != 0)
        {
            Player playerIn = listReserve[_random.Next(listReserve.Count())];
            listActuallyPlaying.Add(playerIn);
            listReserve.Remove(playerIn);
        }
    }
    public Player GiveRandomInjuryDaysRandomPlayer(List<Player> team)
    {
        Player injuredPlayer =  team[_random.Next(team.Count)];
        injuredPlayer.DaysOfInjury = _random.Next(10);
        MakeChangeInPlayingTeam(injuredPlayer);
        return injuredPlayer;
    }

    public string Scorer()
    {
        string toReturn = "";
        int i = 0;
        int width = 30;
        while(i < ListOfHomeScorer.Count || i < ListOfVisitorScorer.Count)
        {
            if (ListOfHomeScorer.Count > i)
            {
                toReturn += ListOfHomeScorer[i].Name;
                for (int j = ListOfHomeScorer[i].Name.Length; j < width; j++)
                {
                    toReturn += " ";
                }
            }
            else
            {
                for (int j = 0; j < width; j++)
                {
                    toReturn += " ";
                }
            }
            if (ListOfVisitorScorer.Count > i)
            {
                for (int j = ListOfVisitorScorer[i].Name.Length - 10; j < width; j++)
                {
                    toReturn += " ";
                }
                toReturn += ListOfVisitorScorer[i].Name;
            }
            toReturn += "\n";
            i++;
        }

        return toReturn;
    }
    public string Score()
    {
        string toReturn = "";
        int width = 30;
        toReturn += Home.Name;
        for (int i = Home.Name.Length; i < width; i++)
            toReturn += " ";
        toReturn += " " + ListOfHomeScorer.Count + " - " + ListOfVisitorScorer.Count + " ";
        for (int i = Visitor.Name.Length; i < width; i++)
            toReturn += " ";
        toReturn += Visitor.Name;
        return toReturn;
    }
    
    public void Display()
    {
        Console.WriteLine(Score());
        Console.WriteLine(Scorer());
    }
}