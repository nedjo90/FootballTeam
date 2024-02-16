namespace FootballTeam;

public class Player
{
    public string Name {get; set;}
    public string Nationality {get; set;}
    public string NationalPosition {get; set;}
    public double NationalKit {get; set;}
    public string Club {get; set;}
    public string ClubPosition {get; set;}
    public double ClubKit {get; set;}
    public string ClubJoining {get; set;}
    public DateTime? ContractExpiry {get; set;}
    public int Rating {get; set;}
    public int Height {get; set;}
    public int Weight {get; set;}
    public string PreferredFoot {get; set;}
    public DateTime? BirthDate {get; set;}
    public int Age {get; set;}
    public string PreferredPosition {get; set;}
    public string WorkRate {get; set;}
    public int WeakFoot {get; set;}
    public int SkillMoves {get; set;}
    public int BallControl {get; set;}
    public int Dribbling {get; set;}
    public int Marking {get; set;}
    public int SlidingTackle {get; set;}
    public int StandingTackle {get; set;}
    public int Aggression {get; set;}
    public int Reactions {get; set;}
    public int AttackingPosition {get; set;}
    public int Interceptions {get; set;}
    public int Vision {get; set;}
    public int Composure {get; set;}
    public int Crossing {get; set;}
    public int ShortPass {get; set;}
    public int LongPass {get; set;}
    public int Acceleration {get; set;}
    public int Speed {get; set;}
    public int Stamina {get; set;}
    public int Strength {get; set;}
    public int Balance {get; set;}
    public int Agility {get; set;}
    public int Jumping {get; set;}
    public int Heading {get; set;}
    public int ShotPower {get; set;}
    public int Finishing {get; set;}
    public int LongShots {get; set;}
    public int Curve {get; set;}
    public int FreekickAccuracy {get; set;}
    public int Penalties {get; set;}
    public int Volleys {get; set;}
    public int GkPositioning {get; set;}
    public int GkDiving {get; set;}
    public int GkKicking {get; set;}
    public int GkHandling {get; set;}
    public int GkReflexes {get; set;}
    public List<Match> MatchInClub { get; set; }
    public int DaysOfSuspension { get;  set; }
    public int DaysOfInjury { get; set; }
    
    public void Display()
    {
        ColorManager.BackBlackForeBlue();
        Console.WriteLine(
            $"<{Name} :" +
            $"{Nationality}\t" + 
            $"{NationalPosition}\t" +
            $"{Club}\t" + 
            $"{ClubPosition}\t" +
            $"{Age} years>" 
            );
        Console.ResetColor();
    }
}