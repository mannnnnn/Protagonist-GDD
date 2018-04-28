using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // queue up a show
    public class StateMachineNodeHide : StateMachineNode
    {
        public override ParseStatement ToStatement(List<Token> tokens)
        {
            // must be 3 tokens: hide channel
            return new HideStatement(tokens[1].contents);
        }
    }
}
