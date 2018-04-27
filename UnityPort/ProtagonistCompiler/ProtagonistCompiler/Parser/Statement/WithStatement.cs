using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // specifies a with statement
    public class WithStatement : ParseStatement
    {
        string effect = "";
        public WithStatement(string effect)
        {
            this.effect = effect;
        }

        public override bool Execute()
        {
            Console.WriteLine("Push queued show/hide statements with effect " + effect);
            return false;
        }
    }
}
