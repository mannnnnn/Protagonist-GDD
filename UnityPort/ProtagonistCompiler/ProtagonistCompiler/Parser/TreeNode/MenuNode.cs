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
            return null;
        }
    }

    // one entry in a menu branch
    public struct MenuEntry
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
