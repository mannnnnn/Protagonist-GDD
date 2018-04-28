using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class JumpStatement : ParseStatement
    {
        public int location;
        public JumpStatement(int location)
        {
            this.location = location;
        }

        public override bool Execute()
        {
            Console.WriteLine("Jump to location " + location);
            return false;
        }
    }
}
