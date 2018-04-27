using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // one node in the statement parser's state machine
    public class StateMachineNode
    {
        public Dictionary<TokenType, StateMachineNode> edges = new Dictionary<TokenType, StateMachineNode>();

        public virtual ParseStatement ToStatement(List<Token> tokens)
        {
            return null;
        }

        public bool hasNext(TokenType type)
        {
            return edges.ContainsKey(type);
        }

        public StateMachineNode next(TokenType type)
        {
            if (!hasNext(type))
            {
                throw new ParseError("Invalid token type " + type + ": Expected a token from " + edges.Keys);
            }
            return edges[type];
        }

        public StateMachineNode next(List<TokenType> types)
        {
            StateMachineNode current = this;
            foreach (TokenType type in types)
            {
                if (hasNext(type))
                {
                    current = current.next(type);
                }
                else
                {
                    throw new ParseError("Invalid token type " + type + ": Expected a token from " + current.edges.Keys);
                }
            }
            return current;
        }
    }
}
