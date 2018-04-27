using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class AssignStatement : ParseStatement
    {
        string id;
        BooleanNode expression;

        public AssignStatement(string id, BooleanNode expression)
        {
            this.id = id;
            this.expression = expression;
        }

        public override bool Execute()
        {
            Console.WriteLine("Set " + id + " to " + expression.Evaluate());
            return false;
        }
    }
}
