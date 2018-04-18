using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // holds data about a node of the parse tree
    public class ParseNode
    {
        public List<ParseNode> children = new List<ParseNode>();

        public ParseNode()
        {

        }
    }
}
