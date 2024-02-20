namespace FootballTeam;

public class Match
{
    private MatchClub _home;
    private MatchClub _visitor;
    private bool _homeForfeit = false;
    private bool _visitorForfeit = false;

    public Match(Club home, Club visitor)
    {
        _home = new MatchClub(home);
        _visitor = new MatchClub(visitor);
    }

    public void StartMatch()
    {
        if (MatchCanContinue())
        {
            for (int i = 0; i < 90; i++)
            {
                GiveGoal();
                GiveYellowCard();
                GiveRedCard();
                if (!MatchCanContinue()) break;
                MakeChange();
                MakeHurt();                
            }
        }
        DataManager.DecrementDaysOfInjuryAndSuspension(_home.Club);
        DataManager.DecrementDaysOfInjuryAndSuspension(_visitor.Club);
    }
    
    public void GiveGoal()
    {
        int homeMean = DataManager.RatingMeanOfPlayerList(_home.ListOnTheGround);
        int visitorMean = DataManager.RatingMeanOfPlayerList(_visitor.ListOnTheGround);
        int randomNumber = DataManager.RandomNumber(MatchProbability.GoalProbability);
        if (randomNumber % 2 == 0 && randomNumber <= homeMean)
            _home.ListOfGoals.Add(DataManager.RandomPlayer(_home.ListOnTheGround));
        else if (randomNumber % 2 != 0 && randomNumber <= visitorMean)
            _visitor.ListOfGoals.Add(DataManager.RandomPlayer(_visitor.ListOnTheGround));
    }
    
    public bool OutPlayer(MatchClub playerClub, Player player)
    {
        if (playerClub.ListOfOut.Count - playerClub.ListOfRedCards.Count < 5 && playerClub.ListOfReserve.Count > 0)
        {
            playerClub.ListOnTheGround.Remove(player);
            playerClub.ListOfOut.Add(player);
            return true;
        }
        return false;
    }

    public void MakeChange()
    {
        int homeRandom = DataManager.RandomNumber(100);
        int visitorRandom = DataManager.RandomNumber(100);
        int randomNumber = DataManager.RandomNumber(MatchProbability.ChangeProbability);
        if (randomNumber % 2 == 0 && randomNumber <= homeRandom && OutPlayer(_home, DataManager.RandomPlayer(_home.ListOnTheGround)))
            _home.ListOnTheGround.Add(DataManager.RandomPlayer(_home.ListOfReserve));
        else if (randomNumber % 2 != 0 && randomNumber <= visitorRandom && OutPlayer(_visitor, DataManager.RandomPlayer(_visitor.ListOnTheGround)))
                _visitor.ListOnTheGround.Add(DataManager.RandomPlayer(_visitor.ListOfReserve));
    }
    public void GiveRedCard(MatchClub playerClub, Player player)
    {
        player.DaysOfSuspension = 3;
        playerClub.ListOfOut.Add(player);
        playerClub.ListOnTheGround.Remove(player);
        playerClub.ListOfRedCards.Add(player);
    }
    public void GiveRedCard()
    {
        int homeMean = DataManager.AgressivityMeanOfPlayerList(_home.ListOnTheGround);
        int visitorMean = DataManager.AgressivityMeanOfPlayerList(_visitor.ListOnTheGround);
        int randomNumber = DataManager.RandomNumber(MatchProbability.RedCardProbability);
        if (randomNumber % 2 == 0 && randomNumber <= homeMean)
            GiveRedCard(_home, DataManager.RandomPlayer(_home.ListOnTheGround));            
        else if (randomNumber % 2 != 0 && randomNumber <= visitorMean)
            GiveRedCard(_visitor, DataManager.RandomPlayer(_visitor.ListOnTheGround));     
    }

    public void MakeHurt(MatchClub playerClub, Player player)
    {
        player.DaysOfInjury = DataManager.RandomNumber(1,10);
        playerClub.ListOfInjuries.Add(player);
        playerClub.ListOfOut.Add(player);
        playerClub.ListOnTheGround.Remove(player);
        if (playerClub.ListOfReserve.Count< 5)
        {
            Player playerIn = DataManager.RandomPlayer(playerClub.ListOfReserve);
            playerClub.ListOnTheGround.Add(playerIn);
            playerClub.ListOfReserve.Remove(playerIn);
        }
    }

    public void MakeHurt()
    {
        int homeMean = DataManager.AgressivityMeanOfPlayerList(_home.ListOnTheGround);
        int visitorMean = DataManager.AgressivityMeanOfPlayerList(_visitor.ListOnTheGround);
        int randomNumber = DataManager.RandomNumber(MatchProbability.HurtProbability);
        if (randomNumber % 2 == 0 && randomNumber <= visitorMean)
            MakeHurt(_home, DataManager.RandomPlayer(_home.ListOnTheGround));
       else if (randomNumber % 2 != 0 && randomNumber <= homeMean)
           MakeHurt(_visitor, DataManager.RandomPlayer(_visitor.ListOnTheGround));
    }

    public void GiveYellowCard()
    {
        int homeMean = DataManager.AgressivityMeanOfPlayerList(_home.ListOnTheGround);
        int visitorMean = DataManager.AgressivityMeanOfPlayerList(_visitor.ListOnTheGround);
        int randomNumber = DataManager.RandomNumber(MatchProbability.YellowCardProbability);
        if (randomNumber % 2 == 0 && randomNumber <= homeMean)
            GiveYellowCard(_home, DataManager.RandomPlayer(_home.ListOnTheGround));            
        else if (randomNumber % 2 != 0 && randomNumber <= visitorMean)
            GiveYellowCard(_visitor, DataManager.RandomPlayer(_visitor.ListOnTheGround));
    }

    public void GiveYellowCard(MatchClub playerClub, Player player)
    {
        playerClub.ListOfYellowCards.Add(player);
        int numberOfYellowCard = 0;
        foreach (Player yellowed in playerClub.ListOfYellowCards)
        {
            if (yellowed.Equals(player))
                numberOfYellowCard++;
        }
        if (numberOfYellowCard == 2)
            GiveRedCard(playerClub, player);
    }

    public bool MatchCanContinue()
    {
        if (_home.ListOnTheGround.Count == 0)
        {
            _homeForfeit = true;
            return false;
        }
        if (_visitor.ListOnTheGround.Count == 0)
        {
            _visitorForfeit = true;
            return false;
        }
        return true;
    }

    
    public string MatchPaperStart()
    {
        string paper = "";
        paper += "Home :";
        paper += _home.MatchPaperStart();
        paper += "Visitor :";
        paper += _visitor.MatchPaperStart();
        return paper;
    }
    public string MatchPaperEnd()
    {
        string paper = "";
        if (_homeForfeit)
            paper += $"{_home.Club.Name} 0 - 3 {_visitor.Club.Name} Forfeit";
        else if (_visitorForfeit)
            paper += $"{_home.Club.Name} 3 - 0 {_visitor.Club.Name} Forfeit";
        else
        {
            paper += $"{_home.Club.Name} {_home.ListOfGoals.Count} - {_visitor.ListOfGoals.Count} {_visitor.Club.Name}";
        }
        // paper += "Home :";
        // paper += _home.MatchPaperEnd();
        // paper += "Visitor :";
        // paper += _visitor.MatchPaperEnd();
        return paper;
    }
}