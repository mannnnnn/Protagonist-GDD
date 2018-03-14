using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class Parser
    {
        public ParseNode Parse(List<Token> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                i = parseCharacters(tokens, i);
                // parse labels
                // parse text
            }
            return null;
        }

        // tries to parse a normal statement
        private int parseStatement(List<Token> tokens, int i)
        {
            return i;
        }

        // tries to parse token as a character definition
        private int parseCharacters(List<Token> tokens, int i)
        {
            // if not a character definition, move on
            if (tokens[i].type != TokenType.CHARACTER)
            {
                return i;
            }

            // parse character definition
            i = skip(tokens, i);
            CharacterDefinition ch = null;
            // get the in-code name of the character
            if (tokens[i].type == TokenType.NAME)
            {
                ch = new CharacterDefinition(tokens[i].contents, tokens[i].contents, Side.RIGHT);
            }
            else
            {
                throw new ParseError("");
            }
            // go to the next token
            i = skip(tokens, i);
            // if it's a bracket, then we have more character data
            if (tokens[i].type == TokenType.BRACK_OPEN)
            {
                // get all tokens within the two brackets
                List<Token> characterInfo = getBracketContents(tokens, i);
                // read character info, ignoring the starting and ending braces
                // only assignment statements are allowed here
                for (int j = 1; j < characterInfo.Count - 1; j++)
                {
                    j = skip(characterInfo, j);
                    // get character field name
                    Token field;
                    if (characterInfo[j].type == TokenType.NAME)
                    {
                        field = characterInfo[j];
                    }
                    else
                    {
                        throw new ParseError("");
                    }
                    j = skip(characterInfo, j);
                    // make sure assignment operator is there
                    if (characterInfo[j].type != TokenType.ASSIGN)
                    {
                        throw new ParseError("");
                    }
                    j = skip(characterInfo, j);
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
                    j = skip(characterInfo, j);
                }
                // parsed many-statement character definition, so skip the tokens we just parsed
                return i + characterInfo.Count + 1;
            }
            // parsed one-line character definition, so move onto the next token
            else
            {
                return i + 1;
            }
        }

        // skips newlines, whitespace, and comments
        private int skip(List<Token> tokens, int i)
        {
            // go to next useful token
            i++;
            while (tokens[i].type == TokenType.WHITESPACE ||
                tokens[i].type == TokenType.NEWLINE || tokens[i].type == TokenType.COMMENT_FULL)
            {
                i++;
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

    public class ParseTree
    {
        public Dictionary<String, CharacterDefinition> characters = new Dictionary<String, CharacterDefinition>();
        public Dictionary<String, LabelDefinition> labels = new Dictionary<String, LabelDefinition>();
        public ParseNode root;
    }

    // holds data about a node of the parse tree
    public class ParseNode
    {
        public List<ParseNode> children = new List<ParseNode>();

        public ParseNode()
        {

        }
    }

    // holds data on a character definition
    public class CharacterDefinition
    {
        public string name;
        public string id;
        public Side side;

        public CharacterDefinition(string name, string id, Side side)
        {
            this.name = name;
            this.id = id;
            this.side = side;
        }
    }

    // holds data on a label definition
    public class LabelDefinition
    {
        public string id;
        public ParseNode node;
    }

    // which side of the screen a character is on
    public enum Side
    {
        LEFT, RIGHT
    }

    // exception that is thrown when an error in parsing occurs
    public class ParseError : Exception
    {
        public ParseError(string message) : base(message)
        {
        }
    }
}
