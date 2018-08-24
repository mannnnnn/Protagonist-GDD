using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Libraries.ProtagonistDialog
{
    /**
     * This and all classes below are just simple classes to make boolean expression evaluation easier.
     * Basically, Shunting Yard algorithm is used, and then it is made into a tree using these classes.
     * At some point I may remove all of these and do immediate execution of boolean expressions on the output of Shunting Yard.
     * See DialogBoolParser, which contains the Shunting Yard algorithm whose output is turned into a tree of these.
     */
    internal interface DialogBoolExpression
    {
        bool Run();
    }

    internal class DialogBoolVar : DialogBoolExpression
    {
        string expr;
        public DialogBoolVar(Token token)
        {
            expr = token.contents;
        }

        public bool Run()
        {
            if (expr == "true")
            {
                return true;
            }
            else if (expr == "false")
            {
                return false;
            }
            else if (Dialog.flags.ContainsKey(expr))
            {
                return Dialog.flags[expr];
            }
            return false;
        }
    }

    internal class DialogBoolUnary : DialogBoolExpression
    {
        Token token;
        DialogBoolExpression expr;
        public DialogBoolUnary(Token token, DialogBoolExpression expr)
        {
            this.token = token;
            this.expr = expr;
        }

        public bool Run()
        {
            switch (token.type)
            {
                case TokenType.NOT:
                    return !expr.Run();
            }
            return false;
        }
    }

    internal class DialogBoolBinary : DialogBoolExpression
    {
        Token token;
        DialogBoolExpression exprA;
        DialogBoolExpression exprB;
        public DialogBoolBinary(Token token, DialogBoolExpression exprA, DialogBoolExpression exprB)
        {
            this.token = token;
            this.exprA = exprA;
            this.exprB = exprB;
        }

        public bool Run()
        {
            switch (token.type)
            {
                case TokenType.AND:
                    return exprA.Run() && exprB.Run();
                case TokenType.OR:
                    return exprA.Run() || exprB.Run();
                case TokenType.EQ:
                    return exprA.Run() == exprB.Run();
                case TokenType.NOT_EQ:
                    return exprA.Run() != exprB.Run();
            }
            return false;
        }
    }
}
