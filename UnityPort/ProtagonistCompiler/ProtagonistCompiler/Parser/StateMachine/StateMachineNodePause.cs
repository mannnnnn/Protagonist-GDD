using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // pause dialogue
    public class StateMachineNodePause : StateMachineNode
    {
        public override ParseStatement ToStatement(List<Token> tokens)
        {
            // must be 1 token: pause
            return new PauseStatement();
        }
    }
}
