using System;
using System.IO;
using System.IO.Compression;

namespace Algorithms.Core.InOut
{
    public sealed class In
    {
        private readonly string _file;

        /// <summary>
        /// Create an input stream from a filename
        /// </summary>
        /// <param name="s"></param>
        public In(String s)
        {
            try
            {
                // first try to read file from local file system
                var path = $"{Directory.GetCurrentDirectory()}\\{s}";
                //var path = s;
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine("file dosn't exists");
                    return;
                }
                _file = path;

            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine("Could not open {0}", ioe.Message);
            }
        }

        public int[] ReadAllInts()
        {
            if (string.IsNullOrWhiteSpace(_file))
            {
                Console.Error.WriteLine("file name is empty");
                throw new Exception();
            }
            var lines = File.ReadAllLines(_file);
            var vals = new int[lines.Length];

            for (var i = 0; i < lines.Length; i++)
            {
                vals[i] = Int32.Parse(lines[i]);
            }
            return vals;
        }

        public string[] ReadAllStrings()
        {
            if (string.IsNullOrWhiteSpace(_file))
            {
                Console.Error.WriteLine("file name is empty");
                throw new Exception();
            }
            var text = File.ReadAllText(_file);
            return text.Split(new[]{" ", "\t", Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
        }

        public string ReadAll()
        {
            if (string.IsNullOrWhiteSpace(_file))
            {
                Console.Error.WriteLine("file name is empty");
                throw new Exception();
            }
            return File.ReadAllText(_file);
        }

        public string[] ReadAllLines()
        {
            if (string.IsNullOrWhiteSpace(_file))
            {
                Console.Error.WriteLine("file name is empty");
                throw new Exception();
            }
            var text = File.ReadAllText(_file);
            return text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] ReadAllLinesFromZip()
        {
            if (string.IsNullOrWhiteSpace(_file))
            {
                Console.Error.WriteLine("file name is empty");
                throw new Exception();
            }
            using (var archive = ZipFile.OpenRead(_file))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.Name.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) || entry.Name.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    {

                        using var reader = new StreamReader(entry.Open());
                        // Just read to the end.
                        var text = reader.ReadToEnd();
                        return text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    }
                }
            }

            return null;

        }
    }
}
