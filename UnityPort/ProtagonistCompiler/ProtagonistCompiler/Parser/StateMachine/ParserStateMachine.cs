using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class ParserStateMachine
    {
        StateMachineNode start = new StateMachineNode();

        // Initialize the state machine
        public ParserStateMachine()
        {
            // initialize the start node, which can go anywhere
            // STR_FULL -> ENDL
            start.edges[TokenType.STRING_FULL] =
                CreateLinkedStateMachine(new List<TokenType> {},
                new StateMachineNodeDisplay());
            // NAME -> : -> STR_FULL -> ENDL
            start.edges[TokenType.NAME] =
                CreateLinkedStateMachine(new List<TokenType> { TokenType.COLON, TokenType.STRING_FULL },
                new StateMachineNodeNameDisplay());
            // NAME -> = -> SOME_BOOL_VALUE -> ENDL, needs to have everything for parsing a boolean expression
            StateMachineNode assign = CreateLinkedStateMachine(new List<TokenType> { }, new StateMachineNodeAssign());
            start.edges[TokenType.NAME].edges[TokenType.ASSIGN] = assign;
            assign.edges[TokenType.NAME] = assign;
            assign.edges[TokenType.PAREN_OPEN] = assign;
            assign.edges[TokenType.PAREN_CLOSE] = assign;
            assign.edges[TokenType.AND] = assign;
            assign.edges[TokenType.OR] = assign;
            assign.edges[TokenType.NOT] = assign;
            assign.edges[TokenType.COMPARE] = assign;
            assign.edges[TokenType.NOTCOMPARE] = assign;
            // SHOW -> CHANNEL -> SPR
            start.edges[TokenType.SHOW] =
                CreateLinkedStateMachine(new List<TokenType> { TokenType.NAME, TokenType.NAME },
                new StateMachineNodeShow());
            // HIDE -> CHANNEL
            start.edges[TokenType.HIDE] =
                CreateLinkedStateMachine(new List<TokenType> { TokenType.NAME },
                new StateMachineNodeHide());
            // JUMP -> LABEL
            start.edges[TokenType.JUMP] =
                CreateLinkedStateMachine(new List<TokenType> { TokenType.NAME },
                new StateMachineNodeJump());
            // WITH -> EFFECT
            start.edges[TokenType.WITH] =
                CreateLinkedStateMachine(new List<TokenType> { TokenType.NAME },
                new StateMachineNodeWith());
            // PLAY -> CHANNEL -> SND
            start.edges[TokenType.PLAY] =
                CreateLinkedStateMachine(new List<TokenType> { TokenType.NAME, TokenType.NAME },
                new StateMachineNodePlay());
            // STOP -> CHANNEL
            start.edges[TokenType.STOP] =
                CreateLinkedStateMachine(new List<TokenType> { TokenType.NAME },
                new StateMachineNodeStop());
            // PAUSE
            start.edges[TokenType.PAUSE] =
                CreateLinkedStateMachine(new List<TokenType> { },
                new StateMachineNodePause());
            // EXIT
            start.edges[TokenType.EXIT] =
                CreateLinkedStateMachine(new List<TokenType> { },
                new StateMachineNodeExit());
        }

        // process one statement
        public int Process(List<Token> tokens, int i, ListNode node)
        {
            // do nothing if it can't be parsed here
            if (!start.hasNext(tokens[i].type))
            {
                return i;
            }
            // start going along the state machine
            StateMachineNode current = start;
            List<Token> statement = new List<Token>();
            for (i = skip(tokens, i); i < tokens.Count; i = skip(tokens, i + 1))
            {
                statement.Add(tokens[i]);
                // advance in the state machine
                if (current.hasNext(tokens[i].type))
                {
                    current = current.next(tokens[i].type);
                    // if we're at the end of the state machine, finish
                    if (current.edges.Count == 0)
                    {
                        node.children.Add(current.ToStatement(statement));
                        return skip(tokens, i + 1);
                    }
                }
                else
                {
                    throw new ParseError("Invalid token type " + tokens[i].type + " while parsing " + string.Join(", ", statement) + " : Expected a token from [" + string.Join(", ", current.edges.Keys.ToArray()) + "]");
                }
            }
            throw new ParseError("Parse Error: Tried to parse statement " + statement + ", got unexpected end of stream.");
        }

        // slightly different from the Parser edition
        // newlines are NOT whitespace, but comments and whitespace are
        private bool IsWhitespace(Token token)
        {
            return (token.type == TokenType.COMMENT_FULL || token.type == TokenType.WHITESPACE);
        }

        // slightly different from the Parser edition
        // skips whitepace and comments, NOT newlines.
        private int skip(List<Token> tokens, int i)
        {
            while (IsWhitespace(tokens[i]))
            {
                i++;
                // if we exceed the list size, stop
                if (i >= tokens.Count)
                {
                    return tokens.Count;
                }
            }
            return i;
        }

        // generate a simple one-way linked ParserStateMachineNode list, requiring the given vertices
        // ends with the given node
        private StateMachineNode CreateLinkedStateMachine(List<TokenType> types, StateMachineNode last)
        {
            StateMachineNode start = new StateMachineNode();
            StateMachineNode current = start;
            foreach (TokenType type in types)
            {
                StateMachineNode next = new StateMachineNode();
                current.edges[type] = next;
                current = next;
            }
            // add the NEWLINE one at the end
            current.edges[TokenType.NEWLINE] = last;
            return start;
        }

        // run the Shunting-Yard algorithm to parse a boolean expression
        public static BooleanNode ParseBoolean(IEnumerable<Token> tokens)
        {
            List<Token> output = new List<Token>();
            Stack<Token> opStack = new Stack<Token>();
            foreach (Token token in tokens)
            {
                switch (token.type)
                {
                    // variable
                    case TokenType.NAME:
                        output.Add(token);
                        break;
                    // unary operator
                    case TokenType.NOT:
                        opStack.Push(token);
                        break;
                    // binary operator
                    case TokenType.AND:
                    case TokenType.OR:
                    case TokenType.COMPARE:
                    case TokenType.NOTCOMPARE:
                        // pop off operators with higher precedence, until an open paren is reached
                        while (opStack.Count >= 1 && opStack.Peek().type != TokenType.PAREN_OPEN
                            && ((opStack.Peek().type == TokenType.NOT)
                            || ((int)opStack.Peek().type < (int)token.type)))
                        {
                            output.Add(opStack.Pop());
                        }
                        // push the current token onto the stack
                        opStack.Push(token);
                        break;
                    // parens
                    case TokenType.PAREN_OPEN:
                        opStack.Push(token);
                        break;
                    case TokenType.PAREN_CLOSE:
                        // pop off operators until you reach the open paren
                        while (opStack.Peek().type != TokenType.PAREN_OPEN)
                        {
                            output.Add(opStack.Pop());
                        }
                        // pop off the opening paren
                        opStack.Pop();
                        break;
                }
            }
            // take opStack and push it to output
            while (opStack.Count > 0)
            {
                output.Add(opStack.Pop());
            }
            // turn the output of the Shunting-Yard algorithm into a parse tree
            Stack<BooleanNode> evalStack = new Stack<BooleanNode>();
            try
            {
                foreach (Token token in output)
                {
                    switch (token.type)
                    {
                        // variable: add a AccessBooleanNode to the stack
                        case TokenType.NAME:
                            evalStack.Push(new AccessBooleanNode(token.contents));
                            break;
                        // unary operator: take one from the stack, apply this operation, and put back the new one
                        case TokenType.NOT:
                            evalStack.Push(new NotBooleanNode(evalStack.Pop()));
                            break;
                        // binary operator: take two from the stack, apply this operation, and put back the new one
                        case TokenType.AND:
                            evalStack.Push(new AndBooleanNode(evalStack.Pop(), evalStack.Pop()));
                            break;
                        case TokenType.OR:
                            evalStack.Push(new OrBooleanNode(evalStack.Pop(), evalStack.Pop()));
                            break;
                        case TokenType.COMPARE:
                            evalStack.Push(new EqBooleanNode(evalStack.Pop(), evalStack.Pop()));
                            break;
                        case TokenType.NOTCOMPARE:
                            evalStack.Push(new NotEqBooleanNode(evalStack.Pop(), evalStack.Pop()));
                            break;
                    }
                }
                if (evalStack.Count < 1)
                {
                    throw new InvalidOperationException();
                }
            }
            catch (InvalidOperationException e)
            {
                throw new ParseError("Error parsing statement " + string.Join(", ", tokens) + ", invalid syntax.");
            }
            // return the final result
            BooleanNode first = evalStack.Peek();
            return first;
        }
    }
}