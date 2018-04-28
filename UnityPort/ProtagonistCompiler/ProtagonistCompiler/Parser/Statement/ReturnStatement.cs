using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // queue up a show until the next with statement
    public class ReturnStatement : ParseStatement
    {
        public override bool Execute()
        {
            Console.WriteLine("Return to previous scope");
            return false;
        }
    }
}
