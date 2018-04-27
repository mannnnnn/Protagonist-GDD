using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class Parser
    {
        ParserStateMachine stateMachine = new ParserStateMachine();

        public ParseTree Parse(List<Token> tokens)
        {
            ParseTree parseTree = new ParseTree();
            ListNode root = new ListNode();
            for (int i = 0; i < tokens.Count; i += 0)
            {
                int start = i;

                // parse characters
                if (i > tokens.Count - 1)
                {
                    break;
                }
                i = parseCharacters(tokens, i, parseTree);

                // parse labels
                if (i > tokens.Count - 1)
                {
                    break;
                }
                i = parseLabels(tokens, i, parseTree);

                // parse statements
                if (i > tokens.Count - 1)
                {
                    break;
                }
                i = parseStatement(tokens, i, root);

                if (start == i)
                {
                    i++;
                }
            }
            // add all instructions
            IEnumerator<ParseStatement> instructions = root.GetEnumerator(0);
            while (instructions.MoveNext())
            {
                // check for goto label location. If so, don't add it and mark its location instead
                parseTree.instructions.Add(instructions.Current);
            }
            // exit at the end

            // add all label instructions at the bottom
            foreach (LabelDefinition ld in parseTree.labels.Values)
            {
                // only method call labels have contents
                if (ld.type != LabelType.CALL)
                {
                    continue;
                }
                // set label location
                ld.location = parseTree.instructions.Count;
                // add all label instructions
                IEnumerator<ParseStatement> labelInstr = ld.node.GetEnumerator(0);
                while (labelInstr.MoveNext())
                {
                    parseTree.instructions.Add(labelInstr.Current);
                }
                // add return at the end

            }
            // print results
            foreach (CharacterDefinition cd in parseTree.characters.Values)
            {
                Console.WriteLine(cd);
            }
            foreach (LabelDefinition ld in parseTree.labels.Values)
            {
                Console.WriteLine(ld);
            }
            return parseTree;
        }

        // tries to parse a normal statement, and appends it to the parseNode
        private int parseStatement(List<Token> tokens, int i, ListNode listNode)
        {
            // if it's whitespace, advance to non-whitespace token
            if (tokens[i].isWhitespace())
            {
                // skip to non-whitespace
                i = skip(tokens, i + 1);
                // if done, return list size
                if (checkDeclarationFinish(tokens, i))
                {
                    return tokens.Count;
                }
            }

            // first handle branching statements
            // parse if-else

            // parse menu

            
            // otherwise, parse the non-branching statement
            i = stateMachine.Process(tokens, i, listNode);
            return i;
        }

        // tries to parse token as a character definition, then add it to the parseTree
        private int parseCharacters(List<Token> tokens, int i, ParseTree parseTree)
        {
            // if not a character definition, move on
            if (tokens[i].type != TokenType.CHARACTER)
            {
                return i;
            }

            // parse character definition
            i = skip(tokens, i + 1);
            if (i >= tokens.Count)
            {
                throw new ParseError("Character definition must be followed by a name token, not by end of file");
            }
            CharacterDefinition ch = null;
            // get the in-code name of the character
            if (tokens[i].type == TokenType.NAME)
            {
                ch = new CharacterDefinition(tokens[i].contents, tokens[i].contents, Side.RIGHT);
                parseTree.characters.Add(ch.id, ch);
            }
            else
            {
                throw new ParseError("Character definition must be followed by a name token, not by " + tokens[i].contents + " which has type " + tokens[i].type);
            }
            // go to the next token
            i = skip(tokens, i + 1);
            // if there are no more tokens, then this is a one-liner. Done.
            if (i >= tokens.Count)
            {
                return tokens.Count;
            }
            // if it's a bracket, then we have more character data
            if (tokens[i].type == TokenType.BRACK_OPEN)
            {
                // get all tokens within the two brackets
                List<Token> characterInfo = getBracketContents(tokens, i);
                // read character info
                // only assignment statements are allowed here
                for (int j = 0; j < characterInfo.Count - 1; j++)
                {
                    // skip whitespace, expecting an assignment. If done, finish
                    j = skip(characterInfo, j + 1);
                    if (checkDeclarationFinish(characterInfo, j + 1))
                    {
                        break;
                    }
                    // get character field name
                    checkToken(characterInfo, j, TokenType.NAME, "Character definition must contain assignments.");
                    Token field = characterInfo[j];

                    // skip whitespace, expecting '=' to make sure assignment operator is there
                    j = skip(characterInfo, j + 1);
                    checkToken(characterInfo, j, TokenType.ASSIGN, "Character definition must contain assignments.");

                    // try to find value token next
                    j = skip(characterInfo, j + 1);
                    checkToken(characterInfo, j, TokenType.VALUE, "Character definition must contain assignments.");
                    // get field value
                    Token value = characterInfo[j];
                    switch (field.contents)
                    {
                        // if setting the name of the character
                        case "name":
                            switch (value.type)
                            {
                                // allow names and numbers
                                case TokenType.NAME:
                                case TokenType.NUM:
                                    ch.name = value.contents;
                                    break;
                                // allow strings, but remove surrounding quotation marks
                                case TokenType.STRING_FULL:
                                    ch.name = value.contents.Substring(1, value.contents.Length - 2);
                                    break;
                                default:
                                    throw new ParseError("Invalid character name: " + value.contents);
                            }
                            break;
                        case "side":
                            // must be either right or left
                            switch (value.contents)
                            {
                                case "left":
                                    ch.side = Side.LEFT;
                                    break;
                                case "right":
                                    ch.side = Side.LEFT;
                                    break;
                                default:
                                    throw new ParseError("Invalid character side: " + value.contents + ". Must be right or left.");
                            }
                            break;
                        default:
                            throw new ParseError("Unrecognized character field: " + field.contents);
                    }
                }
                // parsed many-statement character definition, so skip the tokens we just parsed
                return i + characterInfo.Count;
            }
            // parsed one-line character definition, so move onto the next token
            else
            {
                return i;
            }
        }

        // tries to parse token as a label definition
        private int parseLabels(List<Token> tokens, int i, ParseTree parseTree)
        {
            // if not a label definition, move on
            if (tokens[i].type != TokenType.LABEL)
            {
                return i;
            }

            // parse label definition
            i = skip(tokens, i + 1);
            checkToken(tokens, i, TokenType.NAME, "Label definition must be followed by a name token.");
            // get the in-code name of the label
            LabelDefinition label = new LabelDefinition(tokens[i].contents, new ListNode(), LabelType.GOTO);
            parseTree.labels.Add(label.id, label);

            // go to the next token
            i = skip(tokens, i + 1);
            // if it's a bracket, then we have a 'method-call' label
            if (tokens[i].type == TokenType.BRACK_OPEN)
            {
                // get all tokens within the two brackets
                label.type = LabelType.CALL;
                List<Token> labelContents = getBracketContents(tokens, i);
                // read label definition
                // parse each statement as usual and build its pseudo-parseTree
                for (int j = 1; j < labelContents.Count - 1; j += 0)
                {
                    int start = j;
                    // parse the statement
                    j = parseStatement(labelContents, j, label.node);
                    // advance if necessary
                    if (start == j)
                    {
                        j++;
                    }
                }
                // parsed many-statement label definition, so skip the tokens we just parsed
                return i + labelContents.Count;
            }
            // parsed one-line label definition, so move onto the next token
            else
            {
                return i;
            }
        }

        // check if a small segment is done
        private bool checkDeclarationFinish(List<Token> tokens, int i)
        {
            // if at end of stream or the last bracket, we're done
            return (i >= tokens.Count || (i == tokens.Count - 1 && tokens[i].type == TokenType.BRACK_CLOSE));
        }

        // makes sure target token is of correct type or else throws
        private void checkToken(List<Token> tokens, int i, TokenType type, string message)
        {
            if (type != TokenType.VALUE && tokens[i].type != type)
            {
                throw new ParseError(message + " Expected a " + type + " token, got " + tokens[i].contents + " of type " + tokens[i].type);
            }
            if (i >= tokens.Count)
            {
                throw new ParseError(message + " Expected a " + type + " token, got end of stream");
            }
        }

        // skips newlines, whitespace, and comments
        // does not skip anything if the current token is not whitepsace
        private int skip(List<Token> tokens, int i)
        {
            // go to next useful token
            while (tokens[i].isWhitespace())
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

        // get all contents of brackets, beginning with open bracket at index start
        private List<Token> getBracketContents(List<Token> tokens, int start)
        {
            List<Token> contents = new List<Token>();
            int depth = 0;
            for (int i = start; i < tokens.Count; i++)
            {
                contents.Add(tokens[i]);
                if (tokens[i].type == TokenType.BRACK_OPEN)
                {
                    depth++;
                }
                if (tokens[i].type == TokenType.BRACK_CLOSE)
                {
                    depth--;
                    if (depth == 0)
                    {
                        return contents;
                    }
                }
            }
            // if we've reached the end of the file, and no end bracket was found
            throw new ParseError("Error: SyntaxError: Mismatched brackets");
        }
    }

    // exception that is thrown when an error in parsing occurs
    public class ParseError : Exception
    {
        public ParseError(string message) : base(message)
        {
        }
    }
}
