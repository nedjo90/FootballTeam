namespace FootballTeam;

class Program
{
    static void Main(string[] args)
    {
        DataManager.Initializer();
        MatchManager.GenerateRandomMatch();
    }
}