namespace FootballTeam;

public class Club
{
    public string? Name { get; set; }
    public List<Player>? ListOfPlayers { get; set; }
    public List<Match> ListOfMatch { get; set; }
    

    public Club(string? name)
    {
        Name = name;
        ListOfPlayers = new List<Player>();
        ListOfMatch = new List<Match>();
    }

    public void Display()
    {
        ColorManager.BackBlackForeGreen();
        Console.WriteLine($"{Name}:\n");
        ColorManager.BackBlackForeCyan();
        foreach (Player player in ListOfPlayers)
        {
            Console.WriteLine($"->\t {player.Name}");

        }
        ColorManager.BackBlackForeGreen();
        Console.WriteLine("*****************************************************************************************\n");
    }
}