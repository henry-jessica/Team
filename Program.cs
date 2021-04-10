using System;
using System.Threading;
using System.Text.RegularExpressions;

/*******************************************************************************************************
* Jessica Henry                                                                                        *  
* CA3                                                                                                  *
* Lecturer  Vivion                                                                                     *                                                                                         
* Create Date: 26/03/2021                                                                              *
* Date last modification : 04/04/2021                                                                  *
* Modification by Jessica Henry @ Method - ModifyDetails - Line 284                                    *
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
        static int position = 0;
        static void Main(string[] args)
        {
            players = GetPlayersDetailsFromUser();
        }
        /// <summary>
        /// Read prompt from the user 
        /// </summary>
        /// <returns></returns>
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
                    Console.WriteLine("\n\n\t\tAdding a new player");
                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("Select your option");
                    Console.WriteLine("P - Player \nJ - Junior\nM - Menu");
                    PlayerType = Console.ReadLine().ToLower();
                    if (PlayerType == "m" || PlayerType == "menu")
                        break;
                    result = CheckFormat(PlayerType, "p", "j", "player", "junior", "Player Type");
                } while (!result);

                if (PlayerType == "m" || PlayerType == "menu")
                    break;

                if (PlayerType == "j")
                {
                    JuniorPlayers[count] = new JuniorPlayer();
                    players[count] = JuniorPlayers[count];
                }
                else
                    players[count] = new Player();
                do
                {
                    Console.Write("Insert Player's name : ");
                    players[count].PlayerName = Console.ReadLine();
                    result = IsPresent(players[count].PlayerName, "Player's name") && CheckString(players[count].PlayerName, "Player's name");

                } while (!result);
                do
                {
                    int goalsScoreAux = 0;
                    Console.Write("Goals Scored         : ");
                    string goalsScore = Console.ReadLine();
                    result = IsPresent(goalsScore, "Goals Scored") && IsInteger(goalsScore, "Goals Scored", ref goalsScoreAux);
                    players[count].GoalsScored = goalsScoreAux;

                } while (!result);
                do
                {
                    int matchesPlayedAux = 0;
                    Console.Write("Maches Played        : ");
                    string matchesPlayed = Console.ReadLine();
                    result = IsPresent(matchesPlayed, "Maches Played") && IsInteger(matchesPlayed, "Maches Played", ref matchesPlayedAux);
                    players[count].MatchesPlayed = matchesPlayedAux;

                } while (!result);
                if (PlayerType == "j")
                {
                    do
                    {
                        int ageTypeInt = 0;
                        Console.Write("Enter Age of the Player Junior: ");
                        string age = Console.ReadLine();
                        result = IsPresent(age, "Player Age") && IsInteger(age, "Maches Played", ref ageTypeInt) && CheckPositive(ageTypeInt, "Age") && CheckRange("Junior Age", 3, 17, ageTypeInt);
                        ((JuniorPlayer)players[count]).Age = ageTypeInt;
                        //Check Ranger Age

                    } while (!result);
                }
                //players[count] = player; ------> this part is for second option, in case you would like to insert in the array in the end 
                count++;
                if (count < 5)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("[SUCCESS] Player added!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(1500);
                    Console.Clear();
                    UserRedoAction("Add a new Player", out choice, out result);
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

        /// <summary>
        /// Print Bonus - which will only be accessed if there is a registered player
        /// </summary>
        private static void PrintAmountBonus()
        {
            int amountBonus = 0;
            for (int i = 0; i < count; i++)
            {
                amountBonus += players[i].CalcBonus();
            }
            Console.WriteLine("\nTotal Amount of bonus for all players is {0:C}", amountBonus);
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }
        private static void PrintHighestScoring()
        {
            Console.WriteLine("* Option 6: ");
            int max = 0;
            int l_position = 0;

            for (int i = 0; i < count; i++)
            {
                if (players[i].GoalsScored > max)
                {
                    max = players[i].GoalsScored;
                    l_position = i;
                }
            }
            Console.WriteLine("Highest Scoring is ");
            PrintHeader(l_position);
            Console.WriteLine(players[l_position].ToString());
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }
        private static void PrintPlayerDetails()
        {
            string l_choice = "";
            bool result = false;
            int playerNumID = 0;
            Console.WriteLine("\n\n\t\tDetails of Player");
            Console.WriteLine("-------------------------------------------------------------");
            do
            {
                do
                {
                    Console.Write("\nEnter soccer player ID: ");
                    string userInput = Console.ReadLine().ToLower();
                    result = IsPresent(userInput, "Player ID") && IsInteger(userInput, "Code", ref playerNumID) && CheckPlayerID(playerNumID, "ID Number");

                } while (!result);

                for (int i = 0; i < count; i++)
                {
                    if (players[i].PlayerID == playerNumID)
                    {
                        Console.WriteLine("\n");
                        PrintHeader(i);
                        Console.WriteLine(players[i].ToString());
                        break;
                    }
                }
                UserRedoAction("\nWould you like to consult another player? ", out l_choice, out result);
            } while ((l_choice == "yes" || l_choice == "y"));

        }
        private static void PrintAllPlayers()
        {
            Console.WriteLine("\n All Players: ");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("                      Sligo Rovers  - 2021                        ");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "Junior Age");

            foreach (var playersInfo in players)
            {
                Console.WriteLine(playersInfo);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static void ModifyDetails()
        {
            string l_choice = "";
            bool result = true;
            int playerNumID = 0;
            int EXIT = 0;
            string userChoice = "";
            int inputTypeInt = 0;
            string userInput;

            do
            {
                bool userMadeChanges = false;
                Console.WriteLine("\n\n\t\tModifying Player Information");
                Console.WriteLine("-------------------------------------------------------------");
                do
                {
                    Console.Write("Enter soccer player ID or press 'M' to Main Menu : ");
                    userInput = Console.ReadLine().ToLower();
                    if (userInput == "m" || userInput == "main") //I could have used a loop to control the exit,but for a quick solution and without much impact I found it easier to insert the if break.
                        break;

                    result = IsPresent(userInput, "Player ID") && IsInteger(userInput, "ID Number", ref playerNumID) && CheckPlayerID(playerNumID, "ID Number");
                } while (!result);

                if (userInput == "m" || userInput == "main")
                    break;
                for (int i = 0; i < count; i++)
                {
                    if (players[i].PlayerID == playerNumID)
                    {
                        PrintHeader(i);
                        Console.WriteLine(players[i].ToString());
                        do
                        {
                            EXIT = 4;
                            do
                            {
                                Console.WriteLine("\n\nWhat would you like to change? ");
                                Console.WriteLine("1.  Name \n2.  Goals Scored  \n3.  Matches Played");
                                if (players[i].GetType() == typeof(JuniorPlayer))
                                {
                                    Console.WriteLine("4.  Age");
                                    EXIT = 5;
                                }
                                Console.WriteLine("{0}.  Exit", EXIT);
                                userChoice = Console.ReadLine();
                                result = IsPresent(userChoice, "Input Choice") && IsInteger(userChoice, "Input Choice", ref inputTypeInt) && CheckRange("User Input", 1, EXIT, inputTypeInt);
                            } while (!result);
                            if (userChoice == "1")
                            {
                                Console.Write("Insira a new name for the user: ");
                                string newName = Console.ReadLine();
                                players[i].PlayerName = players[i].ModifyPlayersName(newName);
                                userMadeChanges = true;
                            }
                            else if (userChoice == "2")
                            {
                                do
                                {
                                    int newGoalsScored = 0;
                                    Console.Write("Insira a new Goals Scored: ");
                                    string goalsScore = Console.ReadLine();
                                    result = IsPresent(goalsScore, "Goals Scored") && IsInteger(goalsScore, "Goals Scored", ref newGoalsScored);
                                    players[i].GoalsScored = players[i].ModifyGoalsScored(newGoalsScored);
                                } while (!result);
                                userMadeChanges = true;
                            }
                            else if (userChoice == "3")
                            {
                                do
                                {
                                    int newMatchesPlayed = 0;
                                    Console.Write("Insert a New Numbers of matches Played: ");
                                    string matchesPlayed = Console.ReadLine();
                                    result = IsPresent(matchesPlayed, "Matches Played") && IsInteger(matchesPlayed, "Matches Played", ref newMatchesPlayed);
                                    players[i].MatchesPlayed = players[i].ModifyMatchesPlayed(newMatchesPlayed);
                                } while (!result);
                                userMadeChanges = true;
                            }
                            else if ((players[i].GetType() == typeof(JuniorPlayer) && userChoice == "4"))
                            {
                                do
                                {
                                    int newAgeJuniorPlayer = 0;
                                    Console.Write("Enter Age of the Junior Player: ");
                                    string age = Console.ReadLine();
                                    result = IsPresent(age, "Junior Player Age") && IsInteger(age, "Junior Player Age", ref newAgeJuniorPlayer) && CheckPositive(newAgeJuniorPlayer, "Age") && CheckRange("Junior Age", 3, 17, newAgeJuniorPlayer);
                                    ((JuniorPlayer)players[i]).Age = ((JuniorPlayer)players[i]).ModifyJuniorPlayerAge(newAgeJuniorPlayer);
                                } while (!result);
                                userMadeChanges = true;
                            }
                        } while (inputTypeInt != EXIT);
                        if (userMadeChanges)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("[SUCCESS] Player details modified!\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            PrintHeader(i);
                            Console.WriteLine(players[i].ToString());
                            UserRedoAction("\nWould you like to modify another player ?", out l_choice, out result);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("No information has been modified!");
                            Thread.Sleep(2000);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                }
            } while (l_choice == "yes" || l_choice == "y" && userInput != "e");
        }
        private static void UserRedoAction(string questionIn, out string l_choice, out bool result)
        {
            do
            {
                Console.Write(questionIn + " (y) - Yes or (n) - No ");
                l_choice = Console.ReadLine().ToLower();
                result = CheckFormat(l_choice, "yes", "no", "y", "n", "Option");
            } while (!result);
        }
        private static void PrintPlayerBonus()
        {
            bool result = false;
            int playerNumID = 0;
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
                    Console.WriteLine("Player's bonus is {0:C}", players[i].CalcBonus());
                    break;
                }
            }
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }
        /// <summary>
        /// Check Validate the user Inputs
        /// </summary>
        /// <param name="textIn"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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
            if (players[0] == null)
            {
                Console.WriteLine(" | X |No player is registered, you must register it first");
                return false;
            }
            else
                return true;
        }
        static bool CheckFormat(string textIn, string value1, string value2, string value3, string value4, string name)
        {
            if (textIn == value1 || textIn == value2 || textIn == value3 || textIn == value4)
                return true;
            else
            {
                Console.WriteLine("{0} Must to be {1} ({3})  or  {2} ({4})", name, value1, value2, value3, value4);
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
                    result = IsPresent(choice, "Option") && IsInteger(choice, "Option", ref temporary) && CheckNoRegisteredPlayer();
            } while (!result);
            return choice;
        }
        public static bool CheckPositive(int num, string name)
        {
            if (num > 0)
                return true;
            else
            {
                Console.WriteLine("{0} must be positive", name);
                return false;
            }
        }
        public static void PrintHeader(int index)
        {
            if (players[index].GetType() == typeof(JuniorPlayer))
                Console.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "Junior Age");
            else
                Console.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "");
        }
        private static bool CheckRange(string textIn, int min, int max, int UserIn)
        {
            if (UserIn >= min && UserIn <= max)
                return true;
            else
            {
                Console.WriteLine("{0} has to be between {1} and {2} ", textIn, min, max);
                return false;
            }
        }
        private static bool CheckString(string inputIn, string messageIn)
        {
            if (Regex.IsMatch(inputIn, @"[a-zA-Z]"))
                return true;
            else
            {
                Console.WriteLine("{0} must contain only letters ", messageIn);
                return false;
            }
        }
    }
}


