using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // display text to screen
    public class StateMachineNodeDisplay : StateMachineNode
    {
        public override ParseStatement ToStatement(List<Token> tokens)
        {
            // must be one token long: string
            return new DisplayStatement(tokens[0].contents);
        }
    }
}
