using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // display text to screen
    public class StateMachineNodeNameDisplay : StateMachineNode
    {
        public override ParseStatement ToStatement(List<Token> tokens)
        {
            // must be 3 tokens long: name : string
            return new NameDisplayStatement(tokens[0].contents, tokens[2].contents);
        }
    }
}
