using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class AndBooleanNode : BooleanNode
    {
        public BooleanNode left;
        public BooleanNode right;

        public AndBooleanNode(BooleanNode right, BooleanNode left)
        {
            this.left = left;
            this.right = right;
        }

        // take the and of the two variables
        public override bool Evaluate()
        {
            return left.Evaluate() && right.Evaluate();
        }
    }
}
