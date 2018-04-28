using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class JumpLabelStatement : ParseStatement
    {
        public string id;
        public JumpLabelStatement(string id)
        {
            this.id = id;
        }

        public override bool Execute()
        {
            Console.WriteLine("Jump to location " + id);
            return false;
        }
    }
}
