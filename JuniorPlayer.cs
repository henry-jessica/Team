using System;
using System.Collections.Generic;
using System.Text;

namespace CA2
{
    // Class Child or Derived - Inherit

    class JuniorPlayer : Player 
    {
        private int age; 
     
        public int Age { get { return age; } set { age = value; } }

        //Constructors 
        public JuniorPlayer() : base()
        {

        }
        public JuniorPlayer(int ageIn)
        {
            Age = ageIn;
        }

        // my constructor - including age 
        public JuniorPlayer(string playerNameIn, int goalsScoredIn, int matchesPlayedIn, int ageIn) : base(playerNameIn, goalsScoredIn, matchesPlayedIn)
        {
            Age = ageIn;
        }

        public override int CalcBonus() 
        {
            const int GOALS_VALUE = 100;
            return MatchesPlayed >= 3 ? GoalsScored * GOALS_VALUE : 0;
        }

        public override string ToString()
        {
            return base.ToString() + Age;
        }

        public int ModifyJuniorPlayerAge(int newAge)
        {

            Age = newAge;

            return Age;
        }
    }

}

