using System;
using Algorithms.ConsoleApp.Attributes;
using Algorithms.ConsoleApp.Interfaces;
using Algorithms.Core.Helpers;

namespace Algorithms.ConsoleApp.Workers.Helpers
{
    [ConsoleCommand("Date", "Data type to encapsulate a date (day, month, and year).")]
    public class DateWorker : IWorker
    {
        public void Run()
        {

            var today = new Date(2, 25, 2004);
            Console.WriteLine(today);
            for (var i = 0; i < 10; i++)
            {
                today = today.Next();
                Console.WriteLine(today);
            }


            Console.WriteLine(today.IsAfter(today.Next()));
            Console.WriteLine(today.IsAfter(today));
            Console.WriteLine(today.Next().IsAfter(today));

            var birthday = new Date(10, 16, 1971);
            Console.WriteLine(birthday);
            for (var i = 0; i < 10; i++)
            {
                birthday = birthday.Next();
                Console.WriteLine(birthday);
            }


            Console.ReadLine();
        }
    }
}
