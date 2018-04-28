using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // nameless text display
    public class DisplayStatement : ParseStatement
    {
        string text = "";
        public DisplayStatement(string text)
        {
            this.text = text;
        }

        public override bool Execute()
        {
            Console.WriteLine(text);
            return true;
        }

        public override string ToString()
        {
            return text;
        }
    }
}
