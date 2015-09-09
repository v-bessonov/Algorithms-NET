using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Strings;

namespace Algorithms.ConsoleApp.Workers.Strings
{
    [ConsoleCommand("Alphabet", "A data type for alphabets")]
    public class AlphabetWorker : IWorker
    {
        public void Run()
        {
            var encoded1 = Alphabet.BASE64.ToIndices("NowIsTheTimeForAllGoodMen");
            var decoded1 = Alphabet.BASE64.ToChars(encoded1);
            Console.WriteLine(decoded1);
            Console.WriteLine("----------------------------------------------");

            var encoded2 = Alphabet.DNA.ToIndices("AACGAACGGTTTACCCCG");
            var decoded2 = Alphabet.DNA.ToChars(encoded2);
            Console.WriteLine(decoded2);
            Console.WriteLine("----------------------------------------------");

            var encoded3 = Alphabet.DECIMAL.ToIndices("01234567890123456789");
            var decoded3 = Alphabet.DECIMAL.ToChars(encoded3);
            Console.WriteLine(decoded3);
            Console.WriteLine("----------------------------------------------");

            Console.ReadLine();
        }
    }
}
