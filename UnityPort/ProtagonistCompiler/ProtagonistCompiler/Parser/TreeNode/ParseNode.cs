using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // holds data about a node of the parse tree
    public interface ParseNode
    {
        IEnumerator<ParseStatement> GetEnumerator(int current);
    }
}
