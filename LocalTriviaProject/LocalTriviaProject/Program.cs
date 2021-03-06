﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalTriviaProject
{
    class Program
    {
        static Dictionary<int, BoardNode> board;
        static Dictionary<int, Card> deck = CardParser.parseAndCreateDictionary();
        static List<int> playingDeck = new List<int>();
        static Player currentPlayer;
        static int purpleStart;
        static int orangeStart;
        static int greenStart;
        static int pinkStart;
        static int blueStart;
        static int yellowStart;
        static bool currentPlayerTurn = false;
        static bool playerWin;
        static int lossCount;
        static void Main(string[] args)
        {
            int color;
            int position;
            CreateBoard();
            shuffleDeck();
            currentPlayer = new Player();
            currentPlayerTurn = true;
            playerWin = false;
            lossCount = 0;
            Console.Write("Hello Welcome to Trivia Pursuit please enter a username: ");
            currentPlayer.userName =  Console.ReadLine();
            ClearConsole();
            Console.WriteLine("What color would you like your piece?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            while(!int.TryParse(Console.ReadLine(), out color) || color < 1 || color > 6)
            {
                Console.WriteLine("Error incorrent input: Please enter a number 1-6");
                Console.WriteLine("What color would you like your piece?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            }
            ClearConsole();
            Console.WriteLine("What color position would you like to start at?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            while (!int.TryParse(Console.ReadLine(), out position) || position < 1 || position > 6)
            {
                Console.WriteLine("Error incorrent input: Please enter a number 1-6");
                Console.WriteLine("What color would you like your piece?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            }
            switch(position)
            {
                case 1:
                    currentPlayer.CurrentPosition = pinkStart;
                    break;
                case 2:
                    currentPlayer.CurrentPosition = greenStart;
                    break;
                case 3:
                    currentPlayer.CurrentPosition = blueStart;
                    break;
                case 4:
                    currentPlayer.CurrentPosition = orangeStart;
                    break;
                case 5:
                    currentPlayer.CurrentPosition = yellowStart;
                    break;
                case 6:
                    currentPlayer.CurrentPosition = purpleStart;
                    break;         
            }
            while(!playerWin)
            {
                ClearConsole();
                PlayerTurn();
                if(!currentPlayerTurn)
                {
                    currentPlayerTurn = true;
                    lossCount++;
                }
            }
            Console.ReadLine();
        }

        private static void PlayerTurn()
        {
            int diceRoll;
            if (currentPlayer.Geography == 1 && currentPlayer.Entertainment == 1 && currentPlayer.History == 1 && currentPlayer.Art == 1
                && currentPlayer.Science == 1 && currentPlayer.Sports == 1 && currentPlayer.CurrentPosition == 0)
            {
                finalRound();
            }
            Console.WriteLine(currentPlayer.userName + "'s Turn");
            Console.WriteLine("Rolling Dice");
            Thread.Sleep(1000);
            diceRoll = new Random().Next(1, 6);
            Console.WriteLine("You rolled a " + diceRoll);
            Thread.Sleep(1000);
            MovePlayer(diceRoll, currentPlayer.CurrentPosition);
            if(board[currentPlayer.CurrentPosition].Category != 6)
            {
                if(currentPlayer.CurrentPosition == blueStart && currentPlayer.Geography != 1 || currentPlayer.CurrentPosition == pinkStart && currentPlayer.Entertainment != 1 
                    || currentPlayer.CurrentPosition == yellowStart && currentPlayer.History != 1 
                    || currentPlayer.CurrentPosition == purpleStart && currentPlayer.Art != 1 || 
                    currentPlayer.CurrentPosition == greenStart && currentPlayer.Science != 1 || currentPlayer.CurrentPosition == orangeStart && currentPlayer.Sports != 1)
                {
                    QuestionRound(currentPlayer.CurrentPosition, true);
                }
                else
                {
                    QuestionRound(currentPlayer.CurrentPosition, false);
                }
            }
        }

        private static void finalRound()
        {
            Random rng = new Random();
            ClearConsole();
            Console.WriteLine("The Final Round");
            int category = rng.Next(0, 5);
            string categoryS = "";
            switch (category)
            {
                case 0:
                    categoryS = "Geography";
                    break;
                case 1:
                    categoryS = "Entertainment";
                    break;
                case 2:
                    categoryS = "History";
                    break;
                case 3:
                    categoryS = "Art";
                    break;
                case 4:
                    categoryS = "Science";
                    break;
                case 5:
                    categoryS = "Sports/Leisure";
                    break;
            }
            Console.WriteLine("Your question category is " + categoryS + "\nGood Luck");
            string realAnswer = AskQuestion(category);
            answerQuestion(realAnswer, false, category);
            if(currentPlayerTurn)
            {
                ClearConsole();
                Console.WriteLine("Congratulations " + currentPlayer.userName + " You won!");
                playerWin = true;
            }
            else
            {
                currentPlayerTurn = false;
            }
        }

        private static void QuestionRound(int currentPostition, bool isAPiece)
        {         
            int category;
            string realAnswer;
            if (currentPostition != 0)
            {
                realAnswer = AskQuestion(board[currentPostition].Category);
            }
            else
            {
                ClearConsole();
                Console.WriteLine("You are currently on the center which category would you like to play? Please enter the number" +
                    "\n1.Geography\n2.Entertainment\n3.History\n4.Art\n5.Science\n6.Sports\\Leisure");
                string userchoice = Console.ReadLine();
                while (!int.TryParse(userchoice, out category))
                {
                    Console.WriteLine("Incorrect Input please enter the number associated with the category");
                    Console.WriteLine("You are currently on the center which category would you like to play?" +
                    "\n1.Geography\n2.Entertainment\n3.History\n4.Art\n5.Science\n6.Sports\\Leisure");
                    userchoice = Console.ReadLine();
                }
                realAnswer = AskQuestion(category);
            }
            answerQuestion(realAnswer, isAPiece, board[currentPostition].Category);
        }
        private static void answerQuestion(string realAnswer, bool isAPiece, int category)
        {
            string userAnswser;
            string[] userAnswerA;
            string[] realAnswerA;
            int correctWords = 0;
            userAnswser = Console.ReadLine();
            char[] charsToTrim = { '.', '\"',',' };
            userAnswerA = userAnswser.ToLower().Trim(charsToTrim).Split();
            realAnswerA = realAnswer.ToLower().Trim(charsToTrim).Split();
            if (realAnswerA.Length < userAnswerA.Length)
            {
                ClearConsole();
                Console.WriteLine("Incorrect Answer, The correct answer is:" + realAnswer);
                currentPlayerTurn = false;
                Thread.Sleep(1000);
                return;
            }
            for (int i = 0; i < userAnswerA.Length; i++)
            {
                for(int j = 0; j < realAnswerA.Length; j++)
                {
                    if (userAnswerA[i].Equals(realAnswerA[j]))
                    {
                        correctWords++;
                        break;
                    }
                }
            }
            if (correctWords < realAnswerA.Length / 2)
            {
                ClearConsole();
                Console.WriteLine("Incorrect Answer, The correct answer is " + realAnswer);
                currentPlayerTurn = false;
                Thread.Sleep(1000);
                return;
            }
            else
            {
                ClearConsole();
                Console.WriteLine("Correct Good Job!");
                Console.WriteLine("The Trivia Pursuit answer was " + realAnswer);
                if (isAPiece)
                {
                    Console.WriteLine("Congratulation " + currentPlayer.userName + " You gain a piece as well");
                    switch (category)
                    {
                        case 0:
                            currentPlayer.Geography = 1;
                            currentPlayerTurn = false;
                            break;
                        case 1:
                            currentPlayer.Entertainment = 1;
                            currentPlayerTurn = false;
                            break;
                        case 2:
                            currentPlayer.History = 1;
                            currentPlayerTurn = false;
                            break;
                        case 3:
                            currentPlayer.Art = 1;
                            currentPlayerTurn = false;
                            break;
                        case 4:
                            currentPlayer.Science = 1;
                            currentPlayerTurn = false;
                            break;
                        case 5:
                            currentPlayer.Sports = 1;
                            currentPlayerTurn = false;
                            break;
                    }
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
            }
        }

        private static string AskQuestion(int category)
        {
            Card currentCard;
            if(playingDeck.Count == 0)
            {
                shuffleDeck();
            }
            currentCard = deck[playingDeck[playingDeck.Count - 1]];
            playingDeck.RemoveAt(playingDeck.Count - 1);
            ClearConsole();
            switch (category)
            {
                case 0:
                    Console.WriteLine(currentCard.Geography);
                    return currentCard.GeographyA;
                case 1:
                    Console.WriteLine(currentCard.Entertainment);
                    return currentCard.EntertainmentA;
                case 2:
                    Console.WriteLine(currentCard.History);
                    return currentCard.HistoryA;
                case 3:
                    Console.WriteLine(currentCard.Art);
                    return currentCard.ArtA;
                case 4:
                    Console.WriteLine(currentCard.Science);
                    return currentCard.ScienceA;
                case 5:
                    Console.WriteLine(currentCard.Sports);
                    return currentCard.SportsA;
            }
            return "";
        }

        private static void MovePlayer(int spaces, int currentPosition)
        {
            if(currentPosition != 0)
            {
                BoardNode left;
                BoardNode right;
                BoardNode straight;
                BoardNode backwards;
                string choice;
                left = traverseLeft(spaces, currentPosition);
                right = traverseRight(spaces, currentPosition);
                straight = traverseStraight(spaces, currentPosition);
                backwards = traverseBackwards(spaces, currentPosition);
                Console.WriteLine("Choose a direction to move: Left, Right, Straight, Backwards or if their is that option Center");
                choice = Console.ReadLine();
                while (choice.ToLower().Equals("center") && straight.Category != -1 || choice.ToLower().Equals("left") && left == null || choice.ToLower().Equals("right") && right == null
                    || choice.ToLower().Equals("straight") && straight.Category == -1 || choice.ToLower().Equals("backwards") && backwards == null || choice.ToLower() != "left"
                    && choice.ToLower() != "right" && choice.ToLower() != "straight" && choice.ToLower() != "backwards" && choice.ToLower() != "center")
                {
                    Console.WriteLine("Incorrect Input please make sure you are choose a direction with a valid tile");
                    Console.WriteLine("Choose a direction to move: Left, Right, Straight, Backwards or if their is that option Center");
                    choice = Console.ReadLine();
                }
                switch (choice.ToLower())
                {
                    case "straight":
                        currentPlayer.CurrentPosition = straight.position;
                        break;
                    case "left":
                        currentPlayer.CurrentPosition = left.position;
                        break;
                    case "right":
                        currentPlayer.CurrentPosition = right.position;
                        break;
                    case "backwards":
                        currentPlayer.CurrentPosition = backwards.position;
                        break;
                    case "center":
                        if (straight.position != 0)
                        {
                            currentPlayer.CurrentPosition = traversePaths(straight.position).position;
                        }
                        break;
                }
            }
            else
            {
                currentPlayer.CurrentPosition = traversePaths(spaces).position;
            }   
        }
        private static BoardNode traversePaths(int spaces)
        {
            Console.WriteLine("You are at the Center");
            BoardNode blue = traverseAllPaths(spaces, 1);
            BoardNode pink = traverseAllPaths(spaces, 7);
            BoardNode yellow = traverseAllPaths(spaces, 13);
            BoardNode purple = traverseAllPaths(spaces, 19);
            BoardNode green = traverseAllPaths(spaces, 25);
            BoardNode orange = traverseAllPaths(spaces, 31);
            Console.WriteLine("Would you like to go down the Blue, Pink, Yellow, Purple, Green, or Orange Path?");
            string choice = Console.ReadLine();
            while (!choice.ToLower().Equals("blue") || !choice.ToLower().Equals("pink") || !choice.ToLower().Equals("yellow") || !choice.ToLower().Equals("green") || !choice.ToLower().Equals("orange"))
            {
                ClearConsole();
                Console.WriteLine("Incorrect input");
                Console.WriteLine("You are at the center would you like to go down the Blue, Pink, Yellow, Purple, Green, or Orange Path?");
                choice = Console.ReadLine();
            }
            switch (choice.ToLower())
            {
                case "blue":
                    return blue;
                case "pink":
                    return pink;
                case "yellow":
                    return yellow;
                case "purple":
                    return purple;
                case "green":
                    return green;
                case "orange":
                    return orange;
            }
            return null;
        }
        private static BoardNode traverseAllPaths(int spaces, int startposition)
        {
            string category = "";
            string path = "";
            switch (board[startposition + (spaces - 1)].Category)
            {
                case 0:
                    category = "Geography";
                    break;
                case 1:
                    category = "Entertainment";
                    break;
                case 2:
                    category = "History";
                    break;
                case 3:
                    category = "Art";
                    break;
                case 4:
                    category = "Science";
                    break;
                case 5:
                    category = "Sports/Leisure";
                    break;
                case 6:
                    category = "roll again";
                    break;
            }
            switch(startposition)
            {
                case 1:
                    path = "Blue Path";
                    break;
                case 7:
                    path = "Pink Path";
                    break;
                case 13:
                    path = "Yellow Path";
                    break;
                case 19:
                    path = "Purple Path";
                    break;
                case 25:
                    path = "Green Path";
                    break;
                case 31:
                    path = "Orange Path";
                    break;
            }
            Console.WriteLine("Moving Down the " + path + " " + spaces + " spaces will land you on the tile with " + category);
            return board[startposition + (spaces - 1)];
        }
        private static BoardNode traverseLeft(int spaces, int currentPosition)
        {
            BoardNode currentNode = board[currentPosition];
            string category = "";
            for(int i = 0; i < spaces; i++)
            {
                if(currentNode.left == null)
                {
                    Console.WriteLine("You can't traverse Left");
                    return null;
                }
                else
                {
                    currentNode = currentNode.left;
                }
            }
            switch(currentNode.Category)
            {
                case 0:
                    category = "Geography";
                    break;
                case 1:
                    category = "Entertainment";
                    break;
                case 2:
                    category = "History";
                    break;
                case 3:
                    category = "Art";
                    break;
                case 4:
                    category = "Science";
                    break;
                case 5:
                    category = "Sports/Leisure";
                    break;
                case 6:
                    category = "roll again";
                    break;
            }
            if(currentNode.position == blueStart || currentNode.position == pinkStart || currentNode.position == yellowStart || currentNode.position == purpleStart || currentNode.position == greenStart || currentNode.position == orangeStart)
            {
                Console.WriteLine("Moving Left " + spaces + " spaces will land you on the tile for a piece with " + category);
            }
            else
            {
                Console.WriteLine("Moving Left " + spaces + " spaces will land you on the tile with " + category);
            }
            return currentNode;
        }

        private static BoardNode traverseRight(int spaces, int currentPosition)
        {
            BoardNode currentNode = board[currentPosition];
            string category = "";
            for (int i = 0; i < spaces; i++)
            {
                if (currentNode.right == null)
                {
                    Console.WriteLine("You can't traverse Right");
                    return null;
                }
                else
                {
                    currentNode = currentNode.right;
                }
            }
            switch (currentNode.Category)
            {
                case 0:
                    category = "Geography";
                    break;
                case 1:
                    category = "Entertainment";
                    break;
                case 2:
                    category = "History";
                    break;
                case 3:
                    category = "Art";
                    break;
                case 4:
                    category = "Science";
                    break;
                case 5:
                    category = "Sports/Leisure";
                    break;
                case 6:
                    category = "roll again";
                    break;
            }
            if (currentNode.position == blueStart || currentNode.position == pinkStart || currentNode.position == yellowStart || currentNode.position == purpleStart || currentNode.position == greenStart || currentNode.position == orangeStart)
            {
                Console.WriteLine("Moving Right " + spaces + " spaces will land you on the tile for a piece with " + category);
            }
            else
            {
                Console.WriteLine("Moving Right " + spaces + " spaces will land you on the tile with " + category);
            }
            return currentNode;
        }

        private static BoardNode traverseStraight(int spaces, int currentPosition)
        {
            BoardNode currentNode = board[currentPosition];
            string category = "";
            for (int i = 0; i < spaces; i++)
            {
                if (currentNode.myType() == 1)
                {
                    Console.WriteLine("You can move " + i + " spaces to the Center");
                    return new BoardNode(0) { position = spaces - i, Category = -1 };
                }
                if (currentNode.straight == null)
                {
                    Console.WriteLine("You can't traverse Straight");
                    return null;
                }
                currentNode = currentNode.straight;
            }
            switch (currentNode.Category)
            {
                case 0:
                    category = "Geography";
                    break;
                case 1:
                    category = "Entertainment";
                    break;
                case 2:
                    category = "History";
                    break;
                case 3:
                    category = "Art";
                    break;
                case 4:
                    category = "Science";
                    break;
                case 5:
                    category = "Sports/Leisure";
                    break;
                case 6:
                    category = "roll again";
                    break;
            }
            if(currentNode.position == 0)
            {
                Console.WriteLine("Moving Straight " + spaces + " spaces will land you on the Center tile");
            }
            else
            {
                Console.WriteLine("Moving Straight " + spaces + " spaces will land you on the tile with " + category);
            }
            
            return currentNode;
        }

        private static BoardNode traverseBackwards(int spaces, int currentPosition)
        {
            BoardNode currentNode = board[currentPosition];
            string category = "";
            for (int i = 0; i < spaces; i++)
            {
                if (currentNode.backwards == null)
                {
                    Console.WriteLine("You can't traverse Backwards");
                    return null;
                }
                else
                {
                    currentNode = currentNode.backwards;
                }
            }
            switch (currentNode.Category)
            {
                case 0:
                    category = "Geography";
                    break;
                case 1:
                    category = "Entertainment";
                    break;
                case 2:
                    category = "History";
                    break;
                case 3:
                    category = "Art";
                    break;
                case 4:
                    category = "Science";
                    break;
                case 5:
                    category = "Sports/Leisure";
                    break;
                case 6:
                    category = "roll again";
                    break;
            }
            if (currentNode.position == blueStart || currentNode.position == pinkStart || currentNode.position == yellowStart || currentNode.position == purpleStart || currentNode.position == greenStart || currentNode.position == orangeStart)
            {
                Console.WriteLine("Moving Backwards " + spaces + " spaces will land you on the tile for a piece with " + category);
            }
            else
            {
                Console.WriteLine("Moving Backwards " + spaces + " spaces will land you on the tile with " + category);
            }
            return currentNode;
        }

        private static void CreateBoard()
        {
            int count = 0;
            board = new Dictionary<int, BoardNode>();
            board.Add(0, new CenterNode());
            board[0].position = 0;
            CreateBluePath(ref count);
            CreatePinkPath(ref count);
            CreateYellowPath(ref count);
            CreatePurplePath(ref count);
            CreateGreenPath(ref count);
            CreateOrangePath(ref count);
            CreateRestofBoard(ref count);
        }
        private static void CreateBluePath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 3 });
            board[count].position = count;
            CenterNode n = (CenterNode)board[0];
            n.BluePath = board[count];
            board[count].backwards = board[0];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count-1], Category = 5 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 1 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            blueStart = count;
        }

        private static void CreatePinkPath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 2 });
            CenterNode n = (CenterNode)board[0];
            n.PinkPath = board[count];
            board[count].backwards = board[0];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 5 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            pinkStart = count;
        }

        private static void CreateYellowPath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 5 });
            CenterNode n = (CenterNode)board[0];
            n.YellowPath = board[count];
            board[count].backwards = board[0];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 1 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            yellowStart = count;
        }

        private static void CreatePurplePath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 4 });
            CenterNode n = (CenterNode)board[0];
            n.PurplePath = board[count];
            board[count].backwards = board[0];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 5 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 1 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            purpleStart = count;
        }

        private static void CreateGreenPath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 1 });
            CenterNode n = (CenterNode)board[0];
            n.GreenPath = board[count];
            board[count].backwards = board[0];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 5 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            greenStart = count;
        }

        private static void CreateOrangePath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 0 });
            CenterNode n = (CenterNode)board[0];
            n.OrangePath = board[count];
            board[count].backwards = board[0];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 1 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 5 });
            board[count - 1].backwards = board[count];
            board[count].position = count;
            orangeStart = count;
        }
        private static void CreateRestofBoard(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { left = board[blueStart], Category = 1 });
            board[blueStart].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count -1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 4 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 5 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[purpleStart], Category = 2 });
            board[count - 1].right = board[count];
            board[count].position = count;

            count++;
            board.Add(count, new BoardNode(0) { left = board[purpleStart], Category = 2 });
            board[purpleStart].right = board[count];
            board[purpleStart].left = board[count - 1];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 1 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 0 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[greenStart], Category = 5 });
            board[count - 1].right = board[count];
            board[count].position = count;

            count++;
            board.Add(count, new BoardNode(0) { left = board[greenStart], Category = 5 });
            board[greenStart].right = board[count];
            board[greenStart].left = board[count - 1];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 2 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 3 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[pinkStart], Category = 0 });
            board[count - 1].right = board[count];
            board[count].position = count;

            count++;
            board.Add(count, new BoardNode(0) { left = board[pinkStart], Category = 0 });
            board[pinkStart].right = board[count];
            board[pinkStart].left = board[count - 1];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 5 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 4 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[yellowStart], Category = 3 });
            board[count - 1].right = board[count];
            board[count].position = count;

            count++;
            board.Add(count, new BoardNode(0) { left = board[yellowStart], Category = 3 });
            board[yellowStart].right = board[count];
            board[yellowStart].left = board[count - 1];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 0 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 1 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[orangeStart], Category = 4 });
            board[count - 1].right = board[count];
            board[count].position = count;

            count++;
            board.Add(count, new BoardNode(0) { left = board[orangeStart], Category = 4 });
            board[orangeStart].right = board[count];
            board[orangeStart].left = board[count - 1];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 3 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 2 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            board[count].position = count;
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[blueStart], Category = 1 });
            board[count - 1].right = board[count];
            board[blueStart].left = board[count];
            board[count].position = count;
        }
        private static void ClearConsole()
        {
            Console.Clear();
            Console.WriteLine(currentPlayer.userName);
            Console.WriteLine("Geography:" + currentPlayer.Geography + " Entertainment:" + currentPlayer.Entertainment + " History:" + currentPlayer.History +
                " Art:" + currentPlayer.Art + " Science:" + currentPlayer.Science + " Sports/Leisure:" + currentPlayer.Sports);
        }
        private static void shuffleDeck()
        {
            Random rng = new Random();
            playingDeck = Enumerable.Range(1, 94).OrderBy(i => rng.Next()).Take(94).ToList();
        }
    }
}
