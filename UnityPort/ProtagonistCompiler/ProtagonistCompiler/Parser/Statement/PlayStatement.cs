using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class PlayStatement : ParseStatement
    {
        public string channel;
        public string sound;
        public PlayStatement(string channel, string sound)
        {
            this.channel = channel;
            this.sound = sound;
        }

        public override bool Execute()
        {
            Console.WriteLine("Play sound " + sound + " on channel " + channel);
            return false;
        }
    }
}
