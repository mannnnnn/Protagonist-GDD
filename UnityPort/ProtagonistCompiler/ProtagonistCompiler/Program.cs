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
            Tokenizer tokenizer = new Tokenizer();
            List<Token> tokens = new List<Token>();
            Console.WriteLine("start.");
            int n = 1;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < n; i++)
            {
                using (StreamReader sr = new StreamReader("testcase.protd"))
                {
                    tokens = tokenizer.Tokenize(sr);
                }
                Parser p = new Parser();
                ParseTree tree = p.Parse(tokens);
            }
            Console.WriteLine("time: " + timer.ElapsedMilliseconds / (double)n);
            Console.ReadLine();
        }
    }
}
