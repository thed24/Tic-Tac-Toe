using System;

namespace TicTacToe
{
    internal class Game
    {
        private static void InitBoard(string[,] boardPos) //Sets each position in the board-array to empty, or a "."
        {
            for (int i = 0; i < 3; i++) //Iterates through first dimension of array
            {
                for (int j = 0; j < 3; j++) //Iterates through first dimension of array
                {
                    boardPos[i, j] = "."; //Initialize an empty space at the array position
                }
            }
        }

        private static void DrawBoard(string[,] boardPos) //Prints out every board-array position in a 3x3 grid with the axis coordinates attached
        {
            for (int i = 0; i < 3; i++) //Iterates through first dimension of array
            {
                for (int j = 0; j < 3; j++) //Iterates through second dimension of array
                {
                    Console.Write(boardPos[i, j] + " "); //Prints the array position to the console
                }
                Console.Write(i + 1 + "\n"); //Appends the appropriate Y-Axis to the side of the output
            }
            Console.Write("1 2 3\n\n"); //Appends the X-Axis to the bottom of the output
        }

        private static bool CheckWinConditions(string currentPlayerIcon, string[,] boardPos) //Returns true if the current player has a winning combination
        {
            if (boardPos[0, 0] == currentPlayerIcon && boardPos[0, 1] == currentPlayerIcon && boardPos[0, 2] == currentPlayerIcon)
                return true;
            if (boardPos[1, 0] == currentPlayerIcon && boardPos[1, 1] == currentPlayerIcon && boardPos[1, 2] == currentPlayerIcon)
                return true;
            if (boardPos[2, 0] == currentPlayerIcon && boardPos[2, 1] == currentPlayerIcon && boardPos[2, 2] == currentPlayerIcon)
                return true;
            if (boardPos[0, 0] == currentPlayerIcon && boardPos[1, 0] == currentPlayerIcon && boardPos[2, 0] == currentPlayerIcon)
                return true;
            if (boardPos[0, 1] == currentPlayerIcon && boardPos[1, 1] == currentPlayerIcon && boardPos[2, 1] == currentPlayerIcon)
                return true;
            if (boardPos[0, 2] == currentPlayerIcon && boardPos[1, 2] == currentPlayerIcon && boardPos[2, 2] == currentPlayerIcon)
                return true;
            if (boardPos[0, 0] == currentPlayerIcon && boardPos[1, 1] == currentPlayerIcon && boardPos[2, 2] == currentPlayerIcon)
                return true;
            if (boardPos[0, 2] == currentPlayerIcon && boardPos[1, 1] == currentPlayerIcon && boardPos[2, 0] == currentPlayerIcon)
                return true;
            return false; //If no winning combinations are found, return false
        }

        private static bool CheckDrawCondition(string[,] boardPos) //Returns true if every position on the board is filled without either player winning
        {
            int i, j;
            int total = 0;
            for (i = 0; i < 3; i++) //Iterates through first dimension of array
            {
                for (j = 0; j < 3; j++) //Iterates through second dimension of array
                {
                    if (boardPos[i, j] != ".") //For every non-empty position on the board-array, increment total
                        total++;
                }
            }

            if (total == 9) //If every position on the field is not empty, return true
                return true; 
            return false; //If draw condition is not met, return false
        }

        private static void MainGame(ref string[,] boardPos, bool currentPlayerX)
        {
            bool winX = false; //Set player X win check to false by default
            bool winO = false; //Set player O win check to false by default
            bool draw = false; //Set draw check to false by default
            int posX, posY;
            string playerMove, currentPlayerIcon, newGame;

            while (!winX && !winO && !draw) //Loop the game until a player wins, or both players draw
            {
                if (currentPlayerX) //If the current player is X, the following appropriate code runs
                {
                    Console.Write("Player X, please enter your coordinate points (x,y) to place your X, or enter 'q' to give up: \n\n");
                    currentPlayerIcon = "X"; //Sets the players "icon" or "game piece" as X
                }
                else
                {
                    Console.Write("Player O, please enter your coordinate points (x,y) to place your O, or enter 'q' to give up: \n\n");
                    currentPlayerIcon = "O"; //Sets the players "icon" or "game piece" as O
                }

                DrawBoard(boardPos); //Prints out the current board

                playerMove = Console.ReadLine(); 
                Console.Write("\n"); //Spaces for presentation 

                if (playerMove == "q") //Check if current player has selected "q"
                {
                    Console.Write("Good game! Press any button to exit: \n\n"); 
                    Console.Read(); //Requests player input so that they are able to read the console before exiting it
                    Environment.Exit(0); //Exits console
                }

                try //Try catch clause to account for possible exceptions regarding user input and the array
                {
                    posX = playerMove[2] - '0' - 1; //Translate user input into X-Axis coordinate (1,[2])
                    posY = playerMove[0] - '0' - 1; //Translate user input into Y-Axis coordinate ([1],2)
                                                    //Subtracting one to work with array

                    while (playerMove.Length != 3 || playerMove[1].ToString() != ",") //Loops while user attempts to place piece on non-empty space
                    {
                        Console.Clear();
                        Console.Write("Please use the format as follows (1,2) and try again: \n\n");
                        MainGame(ref boardPos, currentPlayerX); //Pass important variables and recall game function so that player has chance to input correct data
                    }

                    while (boardPos[posX, posY] != ".") //Loops while user attempts to place piece on non-empty space
                    {
                        Console.Clear();
                        Console.Write("That spot has already been used, try another one: \n\n");
                        MainGame(ref boardPos, currentPlayerX); //Pass important variables and recall game function so that player has chance to input correct data
                    }

                    boardPos[posX, posY] = currentPlayerIcon; //Once user input is correct and fits onto an empty space on the board
                                                       //Inputs it into appropriate array position
                }
                catch (Exception e) //If error found, catch it and execute following code
                {
                    //Console.Write(e); Unused, for debugging
                    Console.Clear();
                    Console.Write("Please enter a coordinate between 1 and 3 (EG: 1,2) and try again! \n\n");
                    MainGame(ref boardPos, currentPlayerX); //Pass important variables and recall game function so that player has chance to input correct data
                }

                Console.Clear(); //Clear screen for presentation
                Console.Write("Move accepted! \n\n");

                //DrawBoard(boardPos); //Write current board

                if (CheckWinConditions(currentPlayerIcon, boardPos) && currentPlayerX)
                    winX = true; //Check if Player X won at end of turn
                if (CheckWinConditions(currentPlayerIcon, boardPos) && currentPlayerX == false)
                    winO = true; //Check if Player Y won at end of turn
                if (CheckDrawCondition(boardPos) && !winX && !winO)
                    draw = true; //Check for draw at end of turn

                currentPlayerX = !currentPlayerX; //Swap players
            }

            if (winX) //If Player X wins
                Console.WriteLine("Congratulations Player X, you have won! Type 'y' to play again, or 'q' to quit: \n\n");
            if (winO) //If Player O wins
                Console.WriteLine("Congratulations Player O, you have won! Type 'y' to play again, or 'q' to quit: \n\n");
            if (draw) //If Draw occurs
                Console.WriteLine("Aww, it's a draw! Type 'y' to play again, or 'q' to quit: \n\n");
            newGame = Console.ReadLine(); //Read in players option regarding new game
            while (newGame != "y" && newGame != "q") //Loop until player enters "y" or "q"
            {
                Console.WriteLine("Error! Please type either 'y' to play again, or 'q' to quit: \n\n");
                newGame = Console.ReadLine(); //Request new player input 
            }

            if (newGame == "y")
            {
                InitBoard(boardPos); //Reinitialize board-array with empty spaces
                MainGame(ref boardPos, true); //Restart game with Player X first
            }

            else if (newGame == "q")
                Environment.Exit(0); //Exit console
        }

        private static void Main(string[] args)
        {
            string[,] boardPos = new string[3, 3]; //Create board array

            InitBoard(boardPos); //Initialize board-array with empty spaces

            Console.Write("Welcome to Tic Tac Toe! \n\n");

            MainGame(ref boardPos, true); //Create game with reference to board array and starting player as X
        }
    }
}