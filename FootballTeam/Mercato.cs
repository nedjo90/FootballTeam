namespace FootballTeam;

public class Mercato
{
    public List<Player> ListOfPlayersTransfert { get; private set; }
    public List<Player> ListOfPlayer { get; }
    public List<Club> ListOfClub { get; }

    public Mercato(List<Club> listOfClub)
    {
        ListOfPlayersTransfert = new List<Player>();
        ListOfPlayer = new List<Player>();
        foreach (Club club in listOfClub)
        {
            foreach (Player player in club.ListOfPlayers)
            {
                ListOfPlayer.Add(player);
            }
        }

        ListOfClub = listOfClub;
    }

    public void StartMercato()
    {
        foreach (Player player in ListOfPlayer)
        {
            if (DateTime.Compare(player.ContractExpiry, DataManager.Date) < 0)
            {
                TransfertPlayerToNewClub(player);        
            }
        }

    }

    public void TransfertPlayerToNewClub(Player player)
    {
        Club playerClub = player.ListOfClub[^1];
        Club randomClub = player.ListOfClub[^1];
        do
        {
            randomClub = DataManager.RandomClub(ListOfClub);
        } while (playerClub.Equals(randomClub));
        playerClub.ListOfPlayers.Remove(player);
        randomClub.ListOfPlayers.Add(player);
        player.ListOfClub.Add(randomClub);
        player.ContractExpiry = player.ContractExpiry.AddYears(DataManager.RandomNumber(1,10));
        ListOfPlayersTransfert.Add(player);
    }

    public override string ToString()
    {
        string transfert = "";
        foreach (Player player in ListOfPlayersTransfert)
        {
            int last = player.ListOfClub.Count - 1;
            transfert += $"{player.Name} From {player.ListOfClub[last - 1].Name} to {player.ListOfClub[last].Name}  contract expiry : {player.ContractExpiry}\n";
        }
        return transfert;
    }
}