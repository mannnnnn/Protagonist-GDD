using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProtagonistCompiler
{
    public class Tokenizer
    {
        // list of possible regexes that can produce token types
        TokenRegex[] tokenRegexes;

        public Tokenizer()
        {
            tokenRegexes = new TokenRegex[]
            {
                // matches any comment until the end of the line
                new TokenRegex("/?/+", TokenType.COMMENT_START),
                new TokenRegex("//+[^\\n]*", TokenType.COMMENT_FULL),
                // matches any newline character, whether it be \n or \r\n
                new TokenRegex("(\\n)", TokenType.NEWLINE),
                // matches any string of whitespace characters, including spaces and tabs
                new TokenRegex("\\s", TokenType.WHITESPACE),
                new TokenRegex(":", TokenType.COLON),
                // matches an unfinished string, e.g. "123
                new TokenRegex("(\\\")+[^\\n\\\"]*", TokenType.STRING_START),
                // matches a finished string, e.g. "123"
                new TokenRegex("(\\\")+[^\\n]*(\\\")+", TokenType.STRING_FULL),
                new TokenRegex("\\(", TokenType.PAREN_OPEN),
                new TokenRegex("\\)", TokenType.PAREN_CLOSE),
                new TokenRegex("\\{", TokenType.BRACK_OPEN),
                new TokenRegex("\\}", TokenType.BRACK_CLOSE),
                new TokenRegex("==", TokenType.COMPARE),
                new TokenRegex("=", TokenType.ASSIGN),
                // matches & or &&
                new TokenRegex("&?&", TokenType.AND),
                // matches | or ||
                new TokenRegex("\\|?\\|", TokenType.OR),
                new TokenRegex("!", TokenType.NOT),
                new TokenRegex(",", TokenType.COMMA),
                // matches any valid string of letters or a variable name
                new TokenRegex("[a-zA-Z_$][a-zA-Z_$0-9]*", TokenType.NAME),
                // matches any valid floating point number
                new TokenRegex("[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)", TokenType.NUM)
            };
        }

        public List<Token> Tokenize(StreamReader sr)
        {
            // list to store the tokens
            List<Token> tokens = new List<Token>();
            // string builder helps reduce garbage generated
            StringBuilder sb = new StringBuilder();
            // current token stores the last matched token, the one we will add
            TokenRegex current = default(TokenRegex);
            // index of the last match found
            int lastMatch = 0;
            bool skip = false;
            // read from stream, consuming input, and tokenize
            while (!sr.EndOfStream || skip)
            {
                // add a character
                if (!skip && !sr.EndOfStream)
                {
                    char c = (char)sr.Read();
                    if (c == '\r')
                    {
                        continue;
                    }
                    sb.Append(c);
                }
                skip = false;
                // check regexes
                bool matchFound = false;
                for (int i = lastMatch; i < tokenRegexes.Length; i++)
                {
                    TokenRegex tr = tokenRegexes[i];
                    // if we find a match
                    if (tr.re.IsMatch(sb.ToString()))
                    {
                        // store it
                        lastMatch = i;
                        current = tr;
                        matchFound = true;
                        break;
                    }
                }
                // if no match was found, use the last match found
                if (!matchFound)
                {
                    // if match was found previously
                    if (current.re != null)
                    {
                        // store and remove the last character, since this isn't part of the token
                        char last = sb[sb.Length - 1];
                        sb.Length--;
                        // check to see if adding the extra ending newline is necessary
                        // this check is due to how $ matches both end of string and end of line
                        bool extraNewline = false;
                        if (sb[sb.Length - 1] == '\n' && current.type != TokenType.NEWLINE)
                        {
                            extraNewline = true;
                            sb.Length--;
                        }
                        // tokenize the current string
                        tokens.Add(new Token(current.type, sb.ToString()));
                        if (extraNewline)
                        {
                            tokens.Add(new Token(TokenType.NEWLINE, "\n"));
                        }
                        // clear the string builder and add the last character back
                        sb.Clear();
                        sb.Append(last);
                        // don't consume input for one iteration, since we need to process the last character by itself
                        skip = true;
                        current = default(TokenRegex);
                        lastMatch = 0;
                    }
                    // if no match was found at all
                    else
                    {
                        // invalid token, so show error message
                        throw new SyntaxError("Error: SyntaxError: Unexpected token '" + sb + "'");
                    }
                }
            }
            // process the last token
            if (current.re != null)
            {
                // tokenize the current string
                tokens.Add(new Token(current.type, sb.ToString()));
            }
            // if no match was found for the last token
            else
            {
                // invalid token, so show error message
                throw new SyntaxError("Error: SyntaxError: Unexpected token '" + sb + "'");
            }
            return tokens;
        }
    }

    // links a regex to a TokenType
    public struct TokenRegex
    {
        public Regex re;
        public TokenType type;

        public TokenRegex(string re, TokenType type)
        {
            this.re = new Regex("^" + re + "$");
            this.type = type;
        }
    }

    public struct Token
    {
        public TokenType type;
        public string contents;

        public Token(TokenType type, string contents)
        {
            this.type = type;
            this.contents = contents;

            // convert name to reserved keyword in some cases
            if (this.type == TokenType.NAME)
            {
                switch (contents)
                {
                    case "jump":
                        this.type = TokenType.JUMP;
                        break;
                    case "label":
                        this.type = TokenType.LABEL;
                        break;
                    case "character":
                        this.type = TokenType.CHARACTER;
                        break;
                    case "show":
                        this.type = TokenType.SHOW;
                        break;
                    case "with":
                        this.type = TokenType.WITH;
                        break;
                    case "hide":
                        this.type = TokenType.HIDE;
                        break;
                    case "exit":
                        this.type = TokenType.EXIT;
                        break;
                    case "pause":
                        this.type = TokenType.PAUSE;
                        break;
                    case "if":
                        this.type = TokenType.IF;
                        break;
                    case "else":
                        this.type = TokenType.ELSE;
                        break;
                }
            }
        }

        // converts a token to its (type, content) string representation
        public override string ToString()
        {
            if (type == TokenType.NEWLINE || type == TokenType.COMMENT_FULL)
            {
                return "(" + type + ", " + contents.Replace("\n", "") + ")";
            }
            return "(" + type + ", " + contents + ")";
        }
    }

    // list of possible token types
    public enum TokenType
    {
        NEWLINE, WHITESPACE, COLON, STRING_START, STRING_FULL, PAREN_OPEN, PAREN_CLOSE,
        BRACK_OPEN, BRACK_CLOSE, LABEL, CHARACTER, JUMP, EXIT, PAUSE, SHOW, HIDE, NAME,
        NUM, ASSIGN, COMPARE, AND, OR, NOT, COMMA, COMMENT_START, COMMENT_FULL,
        CHARACTERNAME, LABELNAME, IF, ELSE, WITH
    }

    // exception that is thrown when an error in lexical analysis occurs
    public class SyntaxError : Exception
    {
        public SyntaxError(string message) : base(message)
        {
        }
    }
}