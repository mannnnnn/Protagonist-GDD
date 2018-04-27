using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class BranchStatement : ParseStatement
    {
        public BooleanNode condition;
        public int branchIf = 0;
        public int branchElse = 0;

        public override bool Execute()
        {
            if (condition.Evaluate())
            {
                Console.WriteLine("Jump to if branch: " + branchIf);
            }
            else
            {
                Console.WriteLine("Or jump to else branch: " + branchElse);
            }
            return false;
        }
    }
}
