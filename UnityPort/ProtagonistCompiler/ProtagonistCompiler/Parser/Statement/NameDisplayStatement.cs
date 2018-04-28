using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // display text with a name
    public class NameDisplayStatement : ParseStatement
    {
        string name = "";
        string text = "";
        public NameDisplayStatement(string name, string text)
        {
            this.name = name;
            this.text = text;
        }

        public override bool Execute()
        {
            Console.WriteLine(name + ": " + text);
            return true;
        }

        public override string ToString()
        {
            return name + ": " + text;
        }

    }
}
