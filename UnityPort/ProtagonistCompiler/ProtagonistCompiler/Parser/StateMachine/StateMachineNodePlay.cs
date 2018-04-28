using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // queue up a show
    public class StateMachineNodePlay : StateMachineNode
    {
        public override ParseStatement ToStatement(List<Token> tokens)
        {
            // must be 2 tokens: play channel snd
            return new PlayStatement(tokens[1].contents, tokens[2].contents);
        }
    }
}
