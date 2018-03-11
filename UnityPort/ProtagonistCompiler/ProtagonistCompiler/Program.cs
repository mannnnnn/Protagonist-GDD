using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            m();
        }

        static void m()
        {
            Stopwatch stopwatch = new Stopwatch();
            Tokenizer tokenizer = new Tokenizer();
            List<Token> tokens = new List<Token>();
            Console.WriteLine("start.");
            int n = 1000;
            stopwatch.Start();
            for (int i = 0; i < n; i++)
            {
                using (StreamReader sr = new StreamReader("testcase.protd"))
                {
                    tokens = tokenizer.Tokenize(sr);
                }
            }
            stopwatch.Stop();
            foreach (Token token in tokens)
            {
                Console.WriteLine(token);
            }
            Console.WriteLine("complete in " + stopwatch.ElapsedMilliseconds / (double)n);
            Console.ReadLine();
        }
    }
}
