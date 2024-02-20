namespace FootballTeam;

public class League
{
    public List<Club> ListOfClub { get; private set; }
    public List<Season> ListOfSeason { get; private set; }

    public League(List<Club> listOfClub)
    {
        ListOfClub = listOfClub;
        ListOfSeason = new List<Season>();
    }

    public void PlayNextSeason()
    {
        Season newSeason = new Season(ListOfClub);
        newSeason.StartSeason();
        Console.WriteLine(newSeason.TransfertOfTheSeason.ListOfPlayersTransfert.Count);
        ListOfSeason.Add(newSeason);
    }
}