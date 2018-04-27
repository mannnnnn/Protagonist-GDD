using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // queue up a hide until the next with statement
    public class HideStatement : ParseStatement
    {
        string channel = "";
        string spr = "";
        public HideStatement(string channel, string spr)
        {
            this.channel = channel;
            this.spr = spr;
        }

        public override bool Execute()
        {
            Console.WriteLine("Queueing: hide sprite " + spr + " on channel " + channel);
            return false;
        }
    }
}
