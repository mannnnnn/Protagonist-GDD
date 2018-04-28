using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // label's node
    public class ListNode : ParseNode
    {
        public List<ParseNode> children = new List<ParseNode>();

        public IEnumerator<ParseStatement> GetEnumerator(int current)
        {
            // for every child
            foreach (ParseNode child in children)
            {
                // iterate through every statement in the children
                IEnumerator<ParseStatement> statements = child.GetEnumerator(current);
                while (statements.MoveNext())
                {
                    current++;
                    yield return statements.Current;
                }
            }
        }
    }
}
