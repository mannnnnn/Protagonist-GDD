using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class MenuBranchStatement : ParseStatement
    {
        List<MenuEntry> entries = new List<MenuEntry>();

        public MenuBranchStatement(List<MenuEntry> entries)
        {
            this.entries = entries;
        }

        public override bool Execute()
        {
            List<string> s = new List<string>();
            foreach (MenuEntry e in entries)
            {
                s.Add(e.text + " -> " + e.location);
            }
            Console.WriteLine(string.Join(", ", s));
            return true;
        }
    }
}
