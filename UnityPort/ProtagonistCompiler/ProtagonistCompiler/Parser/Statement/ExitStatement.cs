using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class ExitStatement : ParseStatement
    {
        public override bool Execute()
        {
            Console.WriteLine("Exit dialogue");
            return true;
        }
    }
}
