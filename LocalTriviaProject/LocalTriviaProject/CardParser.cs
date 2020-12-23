using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LocalTriviaProject
{
    class CardParser
    {
        public static Dictionary<int, Card> parseAndCreateDictionary()
        {
            string[] line;
            Dictionary<int, Card> dic = new Dictionary<int, Card>();
            Boolean answers = false;
            int count = 1;
            Card c = new Card();
            // Open the text file using a stream reader.
            using (var sr = new StreamReader("TrivialService.txt"))
            {
                string line1;
                while ((line1 = sr.ReadLine()) != null)
                {
                    line = line1.Split('>');
                    if (answers)
                    {
                        c.GeographyA = line[0];
                        c.EntertainmentA = line[1];
                        c.HistoryA = line[2];
                        c.ArtA = line[3];
                        c.ScienceA = line[4];
                        c.SportsA = line[5];
                        dic.Add(count, c);
                        count++;
                        c = new Card();
                        answers = false;
                    }
                    else
                    {
                        c.Geography = line[0];
                        c.Entertainment = line[1];
                        c.History = line[2];
                        c.Art = line[3];
                        c.Science = line[4];
                        c.Sports = line[5];
                        answers = true;
                    }
                }
            }
            return dic;
        }
    }
}
