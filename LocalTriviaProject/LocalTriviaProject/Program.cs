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
        static BoardNode purpleStart;
        static BoardNode orangeStart;
        static BoardNode greenStart;
        static BoardNode pinkStart;
        static void Main(string[] args)
        {
            int color;
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

        private void CreateBoard()
        {
            board = new Dictionary<int, BoardNode>();

        }
    }
}
