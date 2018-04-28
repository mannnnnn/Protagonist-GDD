using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // queue up a show
    public class StateMachineNodeWith : StateMachineNode
    {
        public override ParseStatement ToStatement(List<Token> tokens)
        {
            // must be 2 tokens: with effects
            return new WithStatement(tokens[1].contents);
        }
    }
}
