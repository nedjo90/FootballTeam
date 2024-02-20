namespace FootballTeam;

public static class Testor
{
    //Test d'affichage + test si moins de 11 joueurs disponible
    public static void TestMatchClubPaper()
    {
        Club real = DataManager.SearchClubByName("Real Madrid");
        real.ListOfPlayers[0].DaysOfSuspension = 3;
        real.ListOfPlayers[1].DaysOfSuspension = 3;
        real.ListOfPlayers[2].DaysOfSuspension = 3;
        real.ListOfPlayers[3].DaysOfSuspension = 3;
        real.ListOfPlayers[4].DaysOfSuspension = 3;
        real.ListOfPlayers[5].DaysOfSuspension = 3;
        real.ListOfPlayers[6].DaysOfSuspension = 3;
        real.ListOfPlayers[7].DaysOfSuspension = 3;
        real.ListOfPlayers[8].DaysOfSuspension = 3;
        real.ListOfPlayers[9].DaysOfSuspension = 3;
        real.ListOfPlayers[10].DaysOfInjury = 3;
        real.ListOfPlayers[11].DaysOfInjury = 3;
        real.ListOfPlayers[12].DaysOfInjury = 3;
        real.ListOfPlayers[13].DaysOfInjury = 3;
        real.ListOfPlayers[14].DaysOfInjury = 3;
        real.ListOfPlayers[15].DaysOfInjury = 3;
        real.ListOfPlayers[16].DaysOfInjury = 3;
        real.ListOfPlayers[17].DaysOfInjury = 3;
        real.ListOfPlayers[18].DaysOfInjury = 3;
        real.ListOfPlayers[19].DaysOfInjury = 3;
        real.ListOfPlayers[20].DaysOfInjury = 3;
        real.ListOfPlayers[21].DaysOfInjury = 3;
        real.ListOfPlayers[22].DaysOfSuspension = 3;
        
        MatchClub matchClub = new (real);
        Console.WriteLine(matchClub.MatchPaperStart());
    }

    //Test Match Paper with the two teams home vs visitor
    public static void TestClassico()
    {
        Club real = DataManager.SearchClubByName("real madrid");
        Club barcelone = DataManager.SearchClubByName("barcelona");
        Match classico = new Match(real, barcelone);
        classico.StartMatch();
        Console.WriteLine(classico.MatchPaperEnd());
    }

    public static void TestMiniLeague()
    {
        List<Club> listOfClub = new List<Club>();
        List<Match> season = new List<Match>();
        while (listOfClub.Count < 10)
        {
            Club randomClub = DataManager.RandomClub();
            if (!listOfClub.Contains(randomClub))
                listOfClub.Add(randomClub);
        }

        foreach (Club home in listOfClub)
        {
            foreach (Club away in listOfClub)
            {
                if (!home.Equals(away))
                {
                    season.Add(new Match(home, away));
                }
            }
        }

        foreach (Match match in season)
        {
            match.StartMatch();
            Console.WriteLine(match.MatchPaperEnd());
        }
    }
}