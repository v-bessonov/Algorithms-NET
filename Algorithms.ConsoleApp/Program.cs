using System;
using Algorithms.ConsoleApp.Entities;
using Algorithms.ConsoleApp.Enums;
using Algorithms.ConsoleApp.Ioc;

namespace Algorithms.ConsoleApp
{
    class Program
    {
        private static Arguments _arguments;

        static void Main(string[] args)
        {
            LoadArguments(args);

            if (_arguments.CommandType == CommandType.Help)
            {
                ShowHelp();
                return;
            }

            LoadSettings();

            if (_arguments.CommandType == CommandType.Test)
            {
                RunTest();
                return;
            }

            if (_arguments.CommandType == CommandType.All)
            {
                RunAll();
                return;
            }
        }

        private static void RunAll()
        {
            

        }

        private static void RunTest()
        {
            var workersBuilder = new WorkersBuilder();
            var worker = workersBuilder.Resolve(_arguments.Parameter);
            worker.Run();

        }

        private static void LoadSettings()
        {
        }


        private static void LoadArguments(string[] args)
        {
            _arguments = new Arguments();
            if (args.Length == 0)
            {
                _arguments.CommandType = CommandType.Help;
                return;
            }
            for (var i = 0; i < args.Length; i++)
            {
                var value = args[i];
                if (i == 0)
                {
                    if (value == "-help")
                    {
                        _arguments.CommandType = CommandType.Help;
                    }
                    if (value == "-test")
                    {
                        _arguments.CommandType = CommandType.Test;
                    }
                }

                if (!string.IsNullOrWhiteSpace(value) && i == 1)
                    _arguments.Parameter = value;
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine(string.Format("Usage: Algorithms.ConsoleApp [-help] [-test testname>]"));
            Console.WriteLine(string.Format("Examples:"));
            Console.WriteLine(string.Format("   -help"));
            Console.WriteLine(string.Format("   -test <testname>"));
        }


    }
}
