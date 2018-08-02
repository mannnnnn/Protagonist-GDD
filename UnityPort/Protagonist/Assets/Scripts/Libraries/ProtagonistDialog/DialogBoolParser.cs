using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.Libraries.ProtagonistDialog
{
    internal class DialogBoolParser
    {
        public static DialogBoolExpression Parse(string s)
        {
            List<Token> tokens = Tokenize(s);
            // remove whitespace tokens
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].IsWhitespace)
                {
                    tokens.RemoveAt(i);
                    i--;
                }
            }
            // parse to boolean expression
            return Parse(tokens);
        }

        static List<TokenRegex> regexes = new List<TokenRegex>() {
            new TokenRegex(TokenType.WHITESPACE, "\\s+"),
            new TokenRegex(TokenType.LPAREN, "\\("),
            new TokenRegex(TokenType.RPAREN, "\\)"),
            new TokenRegex(TokenType.NOT, "!"),
            new TokenRegex(TokenType.AND, "&+"),
            new TokenRegex(TokenType.OR, "\\|+"),
            new TokenRegex(TokenType.EQ, "=+"),
            new TokenRegex(TokenType.NOT_EQ, "!="),
            new TokenRegex(TokenType.VAR, "[a-zA-Z_][a-zA-Z_0-9]*"),
        };
        private static List<Token> Tokenize(string s)
        {
            List<Token> tokens = new List<Token>();
            int match = -1;
            string input = "";
            for (int i = 0; i < s.Length; i++)
            {
                int oldMatch = match;
                match = -1;
                input += s[i];
                // try every regex, looking for a match
                for (int j = Math.Max(0, oldMatch); j < regexes.Count; j++)
                {
                    if (regexes[j].regex.IsMatch(input))
                    {
                        match = j;
                    }
                }
                // if previous one was a match and this isn't now, we completed a token
                if (oldMatch >= 0 && match < 0)
                {
                    tokens.Add(new Token(regexes[oldMatch].type, input.Substring(0, input.Length - 1)));
                    i--;
                    input = "";
                }
                // if both are invalid, we found an invalid token
                else if (oldMatch < 0 && match < 0)
                {
                    throw new SyntaxError("Error: SyntaxError: Unexpected Token " + input);
                }
            }
            // at the end, take what we have remaining as a token
            if (match < 0)
            {
                throw new SyntaxError("Error: SyntaxError: Unexpected Token " + input);
            }
            tokens.Add(new Token(regexes[match].type, input));
            return tokens;
        }

        // parse using the Shunting Yard algorithm
        private static DialogBoolExpression Parse(List<Token> tokens)
        {
            var output = new Queue<Token>();
            var operators = new Stack<Token>();
            // consume input
            foreach (Token token in tokens)
            {
                if (token.IsOperand)
                {
                    output.Enqueue(token);
                }
                else if (token.IsUnaryOp)
                {
                    operators.Push(token);
                }
                else if (token.IsBinaryOp)
                {
                    while (operators.Count > 0 && operators.Peek().type != TokenType.LPAREN
                        && (operators.Peek().IsUnaryOp || (int)token.type < (int)operators.Peek().type))
                    {
                        output.Enqueue(operators.Pop());
                    }
                    operators.Push(token);
                }
                else if (token.type == TokenType.LPAREN)
                {
                    operators.Push(token);
                }
                else if (token.type == TokenType.RPAREN)
                {
                    while (operators.Count > 0 && operators.Peek().type != TokenType.LPAREN)
                    {
                        output.Enqueue(operators.Pop());
                    }
                    if (operators.Count == 0)
                    {
                        throw new SyntaxError("Mismatched parentheses.");
                    }
                    operators.Pop();
                }
            }
            // push remaining operators to queue
            while (operators.Count > 0)
            {
                Token token = operators.Pop();
                if (token.type == TokenType.LPAREN)
                {
                    throw new SyntaxError("Mismatched parentheses.");
                }
                output.Enqueue(token);
            }

            // parse to DialogBoolExpression
            var expression = new Stack<DialogBoolExpression>();
            while (output.Count > 0)
            {
                Token token = output.Dequeue();
                if (token.IsOperand)
                {
                    expression.Push(new DialogBoolVar(token));
                }
                else if (token.IsUnaryOp)
                {
                    expression.Push(new DialogBoolUnary(token, expression.Pop()));
                }
                else if (token.IsBinaryOp)
                {
                    expression.Push(new DialogBoolBinary(token, expression.Pop(), expression.Pop()));
                }
            }
            if (expression.Count != 1)
            {
                throw new SyntaxError("Mismatched operators and operands.");
            }
            return expression.Pop();
        }
    }

    internal class Token
    {
        public string contents;
        public TokenType type;
        public Token(TokenType type, string contents)
        {
            this.contents = contents;
            this.type = type;
        }

        public bool IsWhitespace { get { return type == TokenType.WHITESPACE; } }
        public bool IsOperand { get { return type == TokenType.VAR; } }
        public bool IsUnaryOp { get { return type == TokenType.NOT; } }
        public bool IsBinaryOp { get { return type == TokenType.AND || type == TokenType.OR ||
                    type == TokenType.EQ || type == TokenType.NOT_EQ; } }
    }

    internal class TokenRegex
    {
        public Regex regex;
        public TokenType type;
        public TokenRegex(TokenType type, string regex)
        {
            this.regex = new Regex("^" + regex + "$");
            this.type = type;
        }
    }

    internal enum TokenType
    {
        LPAREN, RPAREN, NOT, AND, OR, EQ, NOT_EQ, VAR, WHITESPACE
    }

    public class SyntaxError : Exception
    {
        public SyntaxError(string message) : base(message)
        {
        }
    }

    public class ParseError : Exception
    {
        public ParseError(string message) : base(message)
        {
        }
    }
}
