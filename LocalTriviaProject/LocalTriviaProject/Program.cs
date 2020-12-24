using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalTriviaProject
{
    class Program
    {
        static Dictionary<int, BoardNode> board;
        static Dictionary<int, Card> deck = CardParser.parseAndCreateDictionary();
        static List<int> playingDeck = new List<int>();
        static Player currentPlayer;
        static string ShuffledOrder;
        static int purpleStart;
        static int orangeStart;
        static int greenStart;
        static int pinkStart;
        static int blueStart;
        static int yellowStart;
        static void Main(string[] args)
        {
            int color;
            int position;
            CreateBoard();
            shuffleDeck();
            currentPlayer = new Player();
            Console.Write("Hello Welcome to Trivia Pursuit please enter a username: ");
            currentPlayer.userName =  Console.ReadLine();
            Console.WriteLine(currentPlayer.userName);
            Console.Clear();
            Console.WriteLine("What color would you like your piece?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            while(!int.TryParse(Console.ReadLine(), out color) || color < 1 || color > 6)
            {
                Console.WriteLine("Error incorrent input: Please enter a number 1-6");
                Console.WriteLine("What color would you like your piece?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            }
            currentPlayer.Color = color;
            Console.Clear();
            Console.WriteLine("What color position would you like to start at?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            while (!int.TryParse(Console.ReadLine(), out position) || position < 1 || position > 6)
            {
                Console.WriteLine("Error incorrent input: Please enter a number 1-6");
                Console.WriteLine("What color would you like your piece?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            }
            Console.Clear();
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
            Console.ReadLine();
        }

        private static void PlayerTurn()
        {
            int diceRoll;
            if (currentPlayer.Geography == 1 && currentPlayer.Entertainment == 1 && currentPlayer.History == 1 && currentPlayer.Art == 1
                && currentPlayer.Science == 1 && currentPlayer.Sports == 1 && currentPlayer.CurrentPosition == 0)
            {
                //finalRound();
            }
            diceRoll = new Random().Next(0, 6);
            Console.WriteLine("You rolled a " + diceRoll);
            MovePlayer(diceRoll, currentPlayer.CurrentPosition);

        }
        private static void MovePlayer(int spaces, int currentPosition)
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
            while (choice.ToLower().Equals("Center") && straight.straight == null || choice.ToLower().Equals("left") && left == null || choice.ToLower().Equals("right") && right == null
                || choice.ToLower().Equals("straight") && straight == null || choice.ToLower().Equals("backwards") && backwards == null || choice.ToLower() != "left" 
                && choice.ToLower() != "right" && choice.ToLower() != "straight" && choice.ToLower() != "backwards" && choice.ToLower() != "center")
            {
                Console.WriteLine("Incorrect Input please make sure you are choose a direction with a valid tile");
                Console.WriteLine("Choose a direction to move: Left, Right, Straight, Backwards or if their is that option Center");
                choice = Console.ReadLine();
            }
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
            Console.WriteLine("Moving Left " + spaces + " spaces will land you on the tile with " + category);
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
            Console.WriteLine("Moving Right " + spaces + " spaces will land you on the tile with " + category);
            return currentNode;
        }

        private static BoardNode traverseStraight(int spaces, int currentPosition)
        {
            BoardNode currentNode = board[currentPosition];
            string category = "";
            for (int i = 0; i < spaces; i++)
            {
                if (currentNode.straight == null)
                {
                    Console.WriteLine("You can't traverse Straight");
                    return null;
                }
                if (currentNode.myType() == 1)
                {
                    Console.WriteLine("You can move " + (spaces - i) + " spaces to the Center");
                    return new BoardNode(0) { position = spaces - i };
                }
                else
                {
                    currentNode = currentNode.straight;
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
            Console.WriteLine("Moving Straight " + spaces + " spaces will land you on the tile with " + category);
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
            Console.WriteLine("Moving Backwards " + spaces + " spaces will land you on the tile with " + category);
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
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 1 });
            board[count - 1].right = board[count];
            board[blueStart].left = board[count];
            board[count].position = count;
        }
        private static void shuffleDeck()
        {
            Random rng = new Random();
            playingDeck = Enumerable.Range(1, 94).OrderBy(i => rng.Next()).Take(94).ToList();
        }
    }
}
