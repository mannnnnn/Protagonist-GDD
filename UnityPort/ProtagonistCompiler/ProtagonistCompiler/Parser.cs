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
                // parse characters
                if (tokens[i].type == TokenType.CHARACTER)
                {
                    i = skip(tokens, i);
                    // get the in-code name of the character
                    if (tokens[i].type == TokenType.NAME)
                    {
                        
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
                        // read character info

                    }
                }
                // parse labels
                // parse text
            }
            return null;
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
        public string side;
    }

    // holds data on a label definition
    public class LabelDefinition
    {
        public string id;
        public ParseNode node;
    }

    // exception that is thrown when an error in parsing occurs
    public class ParseError : Exception
    {
        public ParseError(string message) : base(message)
        {
        }
    }
}
