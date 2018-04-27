using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // queue up a show until the next with statement
    public class ShowStatement : ParseStatement
    {
        string channel = "";
        string spr = "";
        public ShowStatement(string channel, string spr)
        {
            this.channel = channel;
            this.spr = spr;
        }

        public override bool Execute()
        {
            Console.WriteLine("Queueing: show sprite " + spr + " on channel " + channel);
            return false;
        }
    }
}
