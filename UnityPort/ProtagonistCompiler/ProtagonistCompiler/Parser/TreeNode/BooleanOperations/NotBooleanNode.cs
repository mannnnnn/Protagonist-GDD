using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class NotBooleanNode : BooleanNode
    {
        BooleanNode node;

        public NotBooleanNode(BooleanNode node)
        {
            this.node = node;
        }

        // Given x, find !x
        public override bool Evaluate()
        {
            return !node.Evaluate();
        }
    }
}
