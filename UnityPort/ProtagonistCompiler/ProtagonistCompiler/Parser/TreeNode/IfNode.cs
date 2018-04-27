using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // an if statement consists of a conditional jump statement, and then two code blocks
    public class IfNode : ParseNode
    {
        public BooleanNode conditionNode;
        public ListNode ifNodes;
        public ListNode elseNodes;
        public IfNode(ListNode ifNodes, ListNode elseNodes)
        {
            this.ifNodes = ifNodes;
            this.elseNodes = elseNodes;
        }

        public IEnumerator<ParseStatement> GetEnumerator(int current)
        {
            // add the branch statement

            // set the branch statement true case to the if
            // add the if-statement branch
            // set the branch case false to the else case if it exists, otherwise set it to the end
            // add the else-statement branch
            return null;
        }
    }
}
