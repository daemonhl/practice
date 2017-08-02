using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupM.Content.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            var bannedWords = new List<string> { "horrible", "nasty", "bad", "swine" };
            string content = "The weather in London in August is bad. Is like winter, horrible";

            int badWords = 0;

            foreach (var bannedWord in bannedWords)
            {
                if (content.Contains(bannedWord))
                {
                    badWords = badWords + 1;
                }
            }

            Console.WriteLine("Text:");
            Console.WriteLine(content);
            Console.WriteLine("Total negative words: " + badWords);

            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }

    }
}
