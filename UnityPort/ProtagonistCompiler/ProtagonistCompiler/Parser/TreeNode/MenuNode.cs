using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class MenuNode : ParseNode
    {
        List<MenuEntry> entries;
        List<ListNode> contents;

        public MenuNode(List<MenuEntry> entries, List<ListNode> contents)
        {
            this.entries = entries;
            this.contents = contents;
        }

        public IEnumerator<ParseStatement> GetEnumerator(int current)
        {
            // MenuBranch statement, jumping to one of the given blocks
            // then, each blocks jumps to the end
            List<JumpStatement> endBr = new List<JumpStatement>();
            MenuBranchStatement br = new MenuBranchStatement(entries);
            current++;
            yield return br;
            for (int i = 0; i < contents.Count; i++)
            {
                entries[i].location = current;
                // iterate through every statement in the children
                IEnumerator<ParseStatement> statements = contents[i].GetEnumerator(current);
                while (statements.MoveNext())
                {
                    current++;
                    yield return statements.Current;
                }
                // add an jump to the end of the menu choice body to the end of the menu
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

    // one entry in a menu branch
    public class MenuEntry
    {
        public string text;
        public int location;
        public MenuEntry(string text)
        {
            this.text = text;
            location = int.MinValue;
        }
    }
}
