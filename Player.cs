using System;
using System.Collections.Generic;
using System.Text;

namespace CA2
{
    class Player
    {

        private string _playerName;
        private int _goalsScored;
        private int _matchesPlayed;
        private int _myPlayerIDnumber;  //track the ID 
        public static int playerID;    //this will the the ID - "key number of Player"

        public Player()
        {
            playerID++;
            PlayerID = playerID; //track number of people are creating  belongs the class 

        }

        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }
        public int GoalsScored
        {
            get { return _goalsScored; }
            set => _goalsScored = value < 0 ? _goalsScored = 0 : value;
        }
        public int MatchesPlayed
        {
            get { return _matchesPlayed; }
            set => _matchesPlayed = value < 0 ? _matchesPlayed = 0 : value;
        }
        public int PlayerID { get; private set; } //the number is unique and can be accessed outside of the class 

        public Player(string playerNameIn, int goalsScoredIn, int matchesPlayedIn)
        {
            PlayerName = playerNameIn;
            GoalsScored = goalsScoredIn;
            MatchesPlayed = matchesPlayedIn;

        }
        public Player(string playerNameIn)
        {
            PlayerName = playerNameIn;
        }

        public Player(int matchPlayedIn_)
        {
            MatchesPlayed = matchPlayedIn_;
        }

        public virtual int CalcBonus()
        {
            int scoredPayment = 0;
            const int MATCHES_PLAYED_VALUE = 300;

            //Alternatives to IF Statements
            scoredPayment = GoalsScored >= 3 ? 500 : 0;
            scoredPayment = GoalsScored >= 6 ? scoredPayment + 1000 : 0;
            scoredPayment = GoalsScored >= 7 ? scoredPayment + 2000 : 0;
            return scoredPayment + MATCHES_PLAYED_VALUE * MatchesPlayed;
        }


        public override string ToString()
        {
            return String.Format("\n{0,-9}{1,-11}{2,-11}{3,-11}{4,-11:C}", PlayerID, PlayerName, GoalsScored, MatchesPlayed, CalcBonus());
        }

        public virtual string ModifyPlayersName(string newPlayerName)
        {
            PlayerName = newPlayerName;
            return PlayerName;
        }
        public virtual int ModifyGoalsScored(int newGoalsScored)
        {
            GoalsScored = newGoalsScored;
            return GoalsScored;
        }
        public virtual int ModifyMatchesPlayed(int newMatchesPlayed)
        {
            MatchesPlayed = newMatchesPlayed;
            return MatchesPlayed;
        }
    }
}
