using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class StateMachineNodeAssign : StateMachineNode
    {
        public override ParseStatement ToStatement(List<Token> tokens)
        {
            // must be of the form name = booleanExpressionTokens
            BooleanNode node = ParserStateMachine.ParseBoolean(tokens.Skip(2));
            return new AssignStatement(tokens[0].contents, node);
        }
    }
}
