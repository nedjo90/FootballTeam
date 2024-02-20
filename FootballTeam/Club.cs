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
}