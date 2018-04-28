using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // queue up a show
    public class StateMachineNodeShow : StateMachineNode
    {
        public override ParseStatement ToStatement(List<Token> tokens)
        {
            // must be 3 tokens: show channel spr
            return new ShowStatement(tokens[1].contents, tokens[2].contents);
        }
    }
}
