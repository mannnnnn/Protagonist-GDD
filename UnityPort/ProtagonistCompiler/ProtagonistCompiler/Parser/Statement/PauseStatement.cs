using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class PauseStatement : ParseStatement
    {
        public override bool Execute()
        {
            Console.WriteLine("Pause dialogue");
            return true;
        }
    }
}
