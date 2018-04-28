using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // queue up a show
    public class StateMachineNodeJump : StateMachineNode
    {
        public override ParseStatement ToStatement(List<Token> tokens)
        {
            // must be 2 tokens: jump label
            return new JumpLabelStatement(tokens[1].contents);
        }
    }
}
