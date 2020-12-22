using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalTriviaProject
{
    public class BoardNode
    {
        public BoardNode left { get; set; }
        public BoardNode right { get; set; }
        public BoardNode straight { get; set; }
        public int Category { get; set; }
    }
    public class CenterNode : BoardNode
    {
        public BoardNode YellowPath { get; set; }
        public BoardNode GreenPath { get; set; }
        public BoardNode PinkPath { get; set; }
        public BoardNode OrangePath { get; set; }
        public BoardNode PurplePath { get; set; }
        public BoardNode BluePath { get; set; }
    }
}
