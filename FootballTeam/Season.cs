namespace FootballTeam;

public class Season
{
    public List<Match> ListOfMatch{ get; private set; }
    public Mercato TransfertOfTheSeason { get; private set; }
    
    public List<Club> ListOfClub { get; private set; }

    public Season(List<Club> listOfClub)
    {
        ListOfMatch = new List<Match>();
        TransfertOfTheSeason = new Mercato(listOfClub);
        ListOfClub = listOfClub;
    }
    public void StartSeason()
    {
        DataManager.Date = DataManager.Date.AddYears(1);
        TransfertOfTheSeason.StartMercato();
        LaunchAllTheMatchs();
    }

    public void LaunchAllTheMatchs()
    {
        foreach (Club home in ListOfClub)
        {
            foreach (Club away in ListOfClub)
            {
                if (!home.Equals(away))
                {
                    ListOfMatch.Add(new Match(home, away));
                }
            }
        }
    }
}