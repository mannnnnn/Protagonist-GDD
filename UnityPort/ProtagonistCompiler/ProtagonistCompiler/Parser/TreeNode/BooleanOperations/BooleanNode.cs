using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public abstract class BooleanNode : ParseNode
    {
        // boolean nodes can't be directly turned to statements, since they're expressions
        public IEnumerator<ParseStatement> GetEnumerator(int current)
        {
            return null;
        }
        // they're instead evaluated inside if statements
        public abstract bool Evaluate();
    }
}
