using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // list of possible token types
    public enum TokenType
    {
        NEWLINE, WHITESPACE, COLON, STRING_START, STRING_FULL, PAREN_OPEN, PAREN_CLOSE,
        BRACK_OPEN, BRACK_CLOSE, LABEL, CHARACTER, JUMP, EXIT, PAUSE, SHOW, HIDE, NAME,
        NUM, ASSIGN, COMPARE, NOTCOMPARE, AND, OR, NOT, COMMA, COMMENT_START, COMMENT_FULL,
        CHARACTERNAME, LABELNAME, IF, ELSE, WITH, VALUE, CONFIG, EVENT, PLAY, STOP, MENU
    }

    // one token
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
                    case "menu":
                        this.type = TokenType.MENU;
                        break;
                    case "if":
                        this.type = TokenType.IF;
                        break;
                    case "else":
                        this.type = TokenType.ELSE;
                        break;
                    case "text":
                        this.type = TokenType.CONFIG;
                        break;
                    case "event":
                        this.type = TokenType.EVENT;
                        break;
                    case "play":
                        this.type = TokenType.PLAY;
                        break;
                    case "stop":
                        this.type = TokenType.STOP;
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

        // check if Token is whitespace
        public bool isWhitespace()
        {
            return (type == TokenType.WHITESPACE ||
                type == TokenType.NEWLINE || type == TokenType.COMMENT_FULL);
        }
    }
}
