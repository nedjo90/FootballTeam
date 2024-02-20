namespace FootballTeam;

class Program
{
    static void Main(string[] args)
    {
        DataManager.Initializer();
        List<Season> listOfSeason = new List<Season>();
        List<Club> listOfClub = new List<Club>();
        int numberOfClub = 0;
        while (numberOfClub < 20)
        {
            Club randomClub = DataManager.RandomClub();
            if (!listOfClub.Contains(randomClub))
                listOfClub.Add(randomClub);
            numberOfClub++;
        }

        League ligue1 = new League(listOfClub);
        ligue1.PlayNextSeason();
        ligue1.PlayNextSeason();
        ligue1.PlayNextSeason();
        ligue1.PlayNextSeason();
        ligue1.PlayNextSeason();
        ligue1.PlayNextSeason();
        ligue1.PlayNextSeason();
        ligue1.PlayNextSeason();
    }
    
    
}