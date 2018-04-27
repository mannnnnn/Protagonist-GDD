using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class NotEqBooleanNode : AndBooleanNode
    {
        public NotEqBooleanNode(BooleanNode right, BooleanNode left) : base(right, left)
        {
        }

        // take the != of the two variables
        public override bool Evaluate()
        {
            return left.Evaluate() != right.Evaluate();
        }
    }
}
