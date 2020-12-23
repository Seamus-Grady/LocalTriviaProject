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
        static int purpleStart;
        static int orangeStart;
        static int greenStart;
        static int pinkStart;
        static int blueStart;
        static int yellowStart;
        static void Main(string[] args)
        {
            int color;
            CreateBoard();
            Player currentPlayer = new Player();
            Console.Write("Hello Welcome to Trivia Pursuit please enter a username: ");
            currentPlayer.userName =  Console.ReadLine();
            Console.WriteLine(currentPlayer.userName);
            Console.WriteLine("What color would you like your piece?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            while(!int.TryParse(Console.ReadLine(), out color) || color < 1 || color > 6)
            {
                Console.WriteLine("Error incorrent input: Please enter a number 1-6");
                Console.WriteLine("What color would you like your piece?\n1.Pink\n2.Green\n3.Blue\n4.Orange" +
                "\n5.Yellow\n6.Purple");
            }
            currentPlayer.Color = color;
            Console.WriteLine(currentPlayer.Color);
            Console.ReadLine();
        }

        private static void CreateBoard()
        {
            int count = 0;
            board = new Dictionary<int, BoardNode>();
            board.Add(0, new CenterNode());
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
            CenterNode n = (CenterNode)board[0];
            n.BluePath = board[count];
            board[count].backwards = board[0];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count-1], Category = 5 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 1 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            blueStart = count;
        }

        private static void CreatePinkPath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 2 });
            CenterNode n = (CenterNode)board[0];
            n.PinkPath = board[count];
            board[count].backwards = board[0];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 5 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            pinkStart = count;
        }

        private static void CreateYellowPath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 5 });
            CenterNode n = (CenterNode)board[0];
            n.YellowPath = board[count];
            board[count].backwards = board[0];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 1 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            yellowStart = count;
        }

        private static void CreatePurplePath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 4 });
            CenterNode n = (CenterNode)board[0];
            n.PurplePath = board[count];
            board[count].backwards = board[0];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 5 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 1 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            purpleStart = count;
        }

        private static void CreateGreenPath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 1 });
            CenterNode n = (CenterNode)board[0];
            n.GreenPath = board[count];
            board[count].backwards = board[0];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 0 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 5 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            greenStart = count;
        }

        private static void CreateOrangePath(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { straight = board[0], Category = 0 });
            CenterNode n = (CenterNode)board[0];
            n.OrangePath = board[count];
            board[count].backwards = board[0];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 2 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 1 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 3 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 4 });
            board[count - 1].backwards = board[count];
            count++;
            board.Add(count, new BoardNode(0) { straight = board[count - 1], Category = 5 });
            board[count - 1].backwards = board[count];
            orangeStart = count;
        }
        private static void CreateRestofBoard(ref int count)
        {
            count++;
            board.Add(count, new BoardNode(0) { left = board[blueStart], Category = 1 });
            board[blueStart].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count -1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 4 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 5 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[purpleStart], Category = 2 });
            board[count - 1].right = board[count];

            count++;
            board.Add(count, new BoardNode(0) { left = board[purpleStart], Category = 2 });
            board[purpleStart].right = board[count];
            board[purpleStart].left = board[count - 1];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 1 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 0 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[greenStart], Category = 5 });
            board[count - 1].right = board[count];

            count++;
            board.Add(count, new BoardNode(0) { left = board[greenStart], Category = 5 });
            board[greenStart].right = board[count];
            board[greenStart].left = board[count - 1];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 2 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 3 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[pinkStart], Category = 0 });
            board[count - 1].right = board[count];

            count++;
            board.Add(count, new BoardNode(0) { left = board[pinkStart], Category = 0 });
            board[pinkStart].right = board[count];
            board[pinkStart].left = board[count - 1];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 5 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 4 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[yellowStart], Category = 3 });
            board[count - 1].right = board[count];

            count++;
            board.Add(count, new BoardNode(0) { left = board[yellowStart], Category = 3 });
            board[yellowStart].right = board[count];
            board[yellowStart].left = board[count - 1];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 0 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 1 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], right = board[orangeStart], Category = 4 });
            board[count - 1].right = board[count];
            
            count++;
            board.Add(count, new BoardNode(0) { left = board[orangeStart], Category = 4 });
            board[orangeStart].right = board[count];
            board[orangeStart].left = board[count - 1];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 3 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 2 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 6 });
            board[count - 1].right = board[count];
            count++;
            board.Add(count, new BoardNode(0) { left = board[count - 1], Category = 1 });
            board[count - 1].right = board[count];
            board[blueStart].left = board[count];
        }
    }
}
