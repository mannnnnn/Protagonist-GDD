using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // an if statement consists of a conditional jump statement, and then one or more code blocks
    public class IfNode : ParseNode
    {
        public List<BooleanNode> conditions;
        public List<ListNode> contents;
        public IfNode(List<BooleanNode> conditions, List<ListNode> contents)
        {
            this.conditions = conditions;
            this.contents = contents;
        }

        public IEnumerator<ParseStatement> GetEnumerator(int current)
        {
            // for every branch
            foreach (ListNode content in contents)
            {

            }
            return null;
        }
    }
}
