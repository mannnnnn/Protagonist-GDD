using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class OrBooleanNode : AndBooleanNode
    {
        public OrBooleanNode(BooleanNode right, BooleanNode left) : base(right, left)
        {
        }

        // take the or of the two variables
        public override bool Evaluate()
        {
            return left.Evaluate() || right.Evaluate();
        }
    }
}
