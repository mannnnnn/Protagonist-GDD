using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class StopStatement : ParseStatement
    {
        public string channel;
        public StopStatement(string channel)
        {
            this.channel = channel;
        }

        public override bool Execute()
        {
            Console.WriteLine("Stop sound on channel " + channel);
            return false;
        }
    }
}
