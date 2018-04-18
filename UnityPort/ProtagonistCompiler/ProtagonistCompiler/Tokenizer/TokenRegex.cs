using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
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
}
