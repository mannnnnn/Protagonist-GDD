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

        public BranchStatement(BooleanNode condition)
        {
            this.condition = condition;
        }

        public override bool Execute()
        {
            Console.WriteLine("if -> " + branchIf + ", " + "else -> " + branchElse);
            return false;
        }
    }
}
