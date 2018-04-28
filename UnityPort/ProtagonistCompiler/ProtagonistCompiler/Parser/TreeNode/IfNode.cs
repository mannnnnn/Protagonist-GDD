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
            List<JumpStatement> endBr = new List<JumpStatement>();
            for (int i = 0; i < contents.Count; i++)
            {
                // create the branch statement
                BranchStatement br = new BranchStatement(conditions[i]);
                current++;
                yield return br;
                br.branchIf = current;
                // iterate through every statement in the children
                IEnumerator<ParseStatement> statements = contents[i].GetEnumerator(current);
                while (statements.MoveNext())
                {
                    current++;
                    yield return statements.Current;
                }
                br.branchElse = current;
                // add an jump to the end of the if statement to the end of each if statement
                JumpStatement jmp = new JumpStatement(int.MinValue);
                endBr.Add(jmp);
                current++;
                yield return jmp;
            }
            // set the end-br jumps to the right location
            foreach (JumpStatement jmp in endBr)
            {
                jmp.location = current;
            }
        }
    }
}
