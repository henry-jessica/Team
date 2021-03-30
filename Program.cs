using System;
using System.Threading;

/*******************************************************************************************************
* Jessica Henry                                                                                        *  
* CA3                                                                                                  *
* Lecturer  Vivion                                                                                     *                                                                                         
* Create Date: 26/03/2021                                                                              *
* Date last modification : 30/03/2021                                                                  *
********************************************************************************************************/

namespace CA2
{
    class Program
    {
        static Player[] players = new Player[5];
        static JuniorPlayer[] JuniorPlayers = new JuniorPlayer[5];

        const string TABLE_FORMAT = "{0,-5}{1,0}";
        const string TABLE_OUTPUT = "{0,-9}{1,-11}{2,-11}{3,-11}{4,-11}{5,-11}";

        static int count = 0;
        static int CheckEmpty = 0;
        static int position = 0;
        static void Main(string[] args)
        {

            players = GetPlayersDetailsFromUser();

        }

        //Methods Menu and Read prompt from the user 

        public static Player[] GetPlayersDetailsFromUser()
        {
            string choice = "";
            do
            {
                choice = PrintMenu();

                switch (choice)
                {
                    case "1":
                        players = AddPlayers();
                        break;
                    case "2":
                        ModifyDetails();
                        break;
                    case "3":
                        PrintAllPlayers();
                        break;
                    case "4":
                        PrintPlayerDetails();
                        break;
                    case "5":
                        PrintPlayerBonus();
                        break;
                    case "6":
                        PrintHighestScoring();
                        break;
                    case "7":
                        PrintAmountBonus();
                        break;
                    case "8":
                        break;
                    default:
                        Console.WriteLine("*** Wrong value, please try again");
                        Thread.Sleep(1500);
                        Console.Clear();
                        break;
                }

            } while (choice != "8");
            return players;
        }
        public static Player[] AddPlayers()
        {
            
            string choice = "";
            bool result = false;
            string PlayerType = "";
            do
            {
                do
                {
                    Console.Write(" Player (P) -  Junior (J)    | 'H' to Home | ");
                    PlayerType = Console.ReadLine().ToLower();

                    if (PlayerType == "h" || PlayerType == "home")
                    {
                        break;
                    }

                    result = CheckFormat(PlayerType, "p", "j", "player", "junior", "Player Type");


                } while (!result);


                if (PlayerType == "h" || PlayerType == "home")
                {
                    break;
                }

                if (PlayerType == "j")
                {
                    JuniorPlayers[count] = new JuniorPlayer();
                    players[count] = JuniorPlayers[count];
                }
                else
                {
                    players[count] = new Player();
                }

                do
                {
                    Console.Write("Insert Play's name : ");
                    players[count].PlayerName = Console.ReadLine();
                    result = IsPresent(players[count].PlayerName, "Play's name");

                } while (!result);


                do
                {
                    int goalsScoreAux = 0;
                    Console.Write("Goals Score    : ");
                    string goalsScore = Console.ReadLine();
                    result = IsPresent(goalsScore, "Goals Score") && IsInteger(goalsScore, "Goals Score", ref goalsScoreAux);
                    players[count].GoalsScored = goalsScoreAux;

                } while (!result);

                do
                {
                    int matchesPlayedAux = 0;
                    Console.Write("Maches Played  : ");
                    string matchesPlayed = Console.ReadLine();
                    result = IsPresent(matchesPlayed, "Maches Played") && IsInteger(matchesPlayed, "Maches Played", ref matchesPlayedAux);
                    players[count].MatchesPlayed = matchesPlayedAux;

                } while (!result);


                if (PlayerType == "j")
                {
                    do
                    {
                        int ageAux = 0;
                        Console.Write("Enter Age of the Player Junior: ");
                        string age = Console.ReadLine();
                        result = IsPresent(age, "Player Age") && IsInteger(age, "Maches Played", ref ageAux) && CheckPositive(ageAux, "Age");
                        ((JuniorPlayer)players[count]).Age = ageAux;

                    } while (!result);
                }
                //players[count] = player; ------> this part is for second option, in case you would like to insert in the array in the end 
                count++;
                CheckEmpty = 1;

                if (count < 5)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Player successfully added!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(1500);
                    Console.Clear();
                    Console.Write("Add a new Player  (y) - Yes or (n) - No ");
                    choice = Console.ReadLine().ToLower();

                }
                else
                {
                    Console.WriteLine("Maximum 5 Players cadastrated, main menu");
                    Thread.Sleep(1500);
                }

            } while (count < 5 && (choice == "yes" || choice == "y"));


            return players;
        }
        public static string PrintMenu()
        {
            string choice = "";
            Console.Clear();
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("                    Sligo Rovers System                      ");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine(TABLE_FORMAT, " 1.", "Add a player to the team");
            Console.WriteLine(TABLE_FORMAT, " 2.", "Modify a players details");
            Console.WriteLine(TABLE_FORMAT, " 3.", "Print all player details including their bonus");
            Console.WriteLine(TABLE_FORMAT, " 4.", "Print the details of a particular player ");
            Console.WriteLine(TABLE_FORMAT, " 5.", "Print a player’s bonus");
            Console.WriteLine(TABLE_FORMAT, " 6.", "Print the details of the highest scoring player");
            Console.WriteLine(TABLE_FORMAT, " 7.", "Print the total amount of bonus to be paid");
            Console.WriteLine(TABLE_FORMAT, " 8.", "Exit");
            Console.WriteLine("-------------------------------------------------------------");

            choice = CheckUserMenuInput();

            return choice;
        }

        //-----------------------------------------------------------
        //Print Methods, which will only be accessed if there is a registered player
        private static void PrintAmountBonus()
        {
            int amountBonus = 0;
            Console.WriteLine("   * Option 7:*   ");
            for (int i = 0; i < count; i++)
            {
                amountBonus += players[i].CalcBonus();
            }
            Console.WriteLine("Total Amount of bonus for all players is {0}", amountBonus);

            Console.ReadKey();
        }
        private static void PrintHighestScoring()
        {
            Console.WriteLine("* Option 6: ");
            int max = 0;
            int position = 0;

            for (int i = 0; i < count; i++)
            {
                if (players[i].GoalsScored > max)
                {
                    max = players[i].GoalsScored;
                    position = i;
                }
            }
            Console.WriteLine("Highest Scoring is ");
            
            Console.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "Junior Age");

            Console.WriteLine(players[position].ToString());

            Console.ReadKey();
        }
        private static void PrintPlayerDetails()
        {
            string l_choice = "";
            bool result = false;
            int playerNumID = 0;

            Console.WriteLine("\t\t||   Details of a Player  ||");
            do
            {
                do
                {
                    Console.Write("\nEnter Player ID : ");
                    string userInput = Console.ReadLine();
                    result = IsPresent(userInput, "Player ID") && IsInteger(userInput, "Code", ref playerNumID) && CheckPlayerID(playerNumID, "ID Number");

                } while (!result);


                Console.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "Junior Age");

                for (int i = 0; i < count; i++)
                {
                    if (players[i].PlayerID == playerNumID)
                    {
                        Console.WriteLine(players[i].ToString());
                        break;
                    }
                }


                do
                {
                    Console.WriteLine("Would you like to consult another player?");
                    Console.Write("Yes  or  No: ");
                    l_choice = Console.ReadLine().ToLower();
                    result = CheckFormat(l_choice, "yes", "no", "y", "n", "Option");

                } while (!result);


            } while ((l_choice == "yes" || l_choice == "y"));

        }
        private static void PrintAllPlayers()
        {
            Console.WriteLine("\nOption - 3\n All Players: ");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("                  Sligo Rovers  - 2021                ");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "Junior Age");
            foreach (var playersInfo in players)
            {
                Console.WriteLine(playersInfo);
            }

            Console.WriteLine("press any key to continue");
            Console.ReadKey();
        }
        private static void ModifyDetails()
        {
            bool result = true;
            int playerNumID = 0;


            Console.WriteLine("\n Modify details of registered players ");

            do
            {
                Console.Write("Enter soccer player ID or press 'H' to Home : ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "h" || userInput == "home")
                {
                    break;
                }

                result = IsPresent(userInput, "Player ID") && IsInteger(userInput, "ID Number", ref playerNumID) && CheckPlayerID(playerNumID, "ID Number");
            } while (!result);

            for (int i = 0; i < count; i++)
            {

                if (players[i].PlayerID == playerNumID)
                {
                    Console.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "Junior Age");
                    Console.WriteLine(players[i].ToString());
                    Console.WriteLine("\nWhat would you like to change? ");

                    Console.WriteLine("1.  Name \n2.  Score Goals  \n3.  Matches Played ");

                    if (players[i].GetType() == typeof(JuniorPlayer))
                    {
                        Console.WriteLine("4.  Age");
                    }

                    string userChoice = Console.ReadLine();

                    if (userChoice == "1")
                    {
                        Console.Write("Insira a new name for the user: ");
                        string newName = Console.ReadLine();
                        players[i].PlayerName = players[i].ModifyPlayersName(newName);

                    }
                    else if (userChoice == "2")
                    {
                        Console.Write("Insira a new Score Goals ");
                        int newGoalsScored = int.Parse(Console.ReadLine());
                        players[i].GoalsScored = players[i].ModifyGoalsScored(newGoalsScored);
                    }
                    else if (userChoice == "3")
                    {
                        Console.Write("Insira a New Numbers of matches Played ");
                        int newMatchesPlayed = int.Parse(Console.ReadLine());
                        players[i].MatchesPlayed = players[i].ModifyMatchesPlayed(newMatchesPlayed);
                    }
                    else if ((players[i].GetType() == typeof(JuniorPlayer) && userChoice == "4"))
                    {
                        do
                        {
                            int newAgeJuniorPlayer = 0;
                            Console.Write("Enter Age of the Player Junior: ");
                            string age = Console.ReadLine();
                            result = IsPresent(age, "Junior Player Age") && IsInteger(age, "Junior Player Age", ref newAgeJuniorPlayer)&& CheckPositive(newAgeJuniorPlayer,"Age");
                            ((JuniorPlayer)players[i]).Age = ((JuniorPlayer)players[i]).ModifyJuniorPlayerAge(newAgeJuniorPlayer);


                        } while (!result);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Player details uccessfully modified!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(players[i].ToString());
                        Thread.Sleep(1500);
                        Console.Clear();

                    }

                }
            }
        }
        private static void PrintPlayerBonus()
        {
            bool result = false;
            int playerNumID = 0;
            //Console.Write("Enter Player ID ");
            // int.Parse(Console.ReadLine());

            do
            {
                Console.Write("\nEnter Player ID : ");
                string userInput = Console.ReadLine();
                result = IsPresent(userInput, "Player ID") && IsInteger(userInput, "Code", ref playerNumID) && CheckPlayerID(playerNumID, "ID Number");

            } while (!result);

            for (int i = 0; i < count; i++)
            {
                if (players[i].PlayerID == playerNumID)
                {
                    Console.WriteLine(players[i].CalcBonus());
                    break;
                }
            }

            Console.ReadKey();
        }

        //-----------------------------------------------------------
        //Methods for Validating User Input
        static bool IsPresent(string textIn, string name)
        {
            if (textIn == "")
            {
                Console.WriteLine(" {0} must be present", name);
                return false;
            }
            else
                return true;
        }
        static bool IsInteger(string textIn, string name, ref int userInt)
        {

            if (int.TryParse(textIn, out userInt))
                return true;
            else
            {
                Console.WriteLine("{0} must be of type integer", name);

                return false;
            }

        }
        static bool CheckPlayerID(int textIn, string message)
        {
            bool match = false;
            for (int i = 0; i < count; i++)
            {
                if (players[i].PlayerID == textIn)
                {
                    match = true;
                    position = i;
                    return match;
                }
            }
            Console.WriteLine(" {0}  {1} was not found", message, textIn);
            return match;

        }
        static bool CheckNoRegisteredPlayer()
        {
            if (CheckEmpty == 0)
            {
                Console.WriteLine(" | X | No Players Registered ");
                Thread.Sleep(1500);

                return false;
            }
            else
            {
                return true;
            }
        }
        static bool CheckFormat(string textIn, string value1, string value2, string value3, string value4, string name)
        {

            if (textIn == value1 || textIn == value2 || textIn == value3 || textIn == value4)
            {
                return true;
            }
            else
            {
                Console.WriteLine("{0} Must to be {1} or {2} ", name, value1, value2);
                return false;
            }

        }
        public static string CheckUserMenuInput()
        {
            string choice;
            bool result = false;
            int temporary = 0; //I'm not using this variable to sent values, created just as mandatory Ref in my method IsInteger.
            do
            {
                Console.Write("\nSelect an option from the menu above: ");
                result = true;
                choice = Console.ReadLine();
                if (choice != "1")
                {
                    result = IsPresent(choice, "Option") && IsInteger(choice, "Option", ref temporary) && CheckNoRegisteredPlayer();
                    // Thread.Sleep(1500);
                }
            } while (!result);

            return choice;
        }

        public static bool CheckPositive(int num, string name)
        {
            if(num > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("{0} must be positive", name);

                return false;
            }


        }

    }
}

