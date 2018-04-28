﻿using System;
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
        public HideStatement(string channel)
        {
            this.channel = channel;
        }

        public override bool Execute()
        {
            Console.WriteLine("Queueing: hide sprite on channel " + channel);
            return false;
        }
    }
}
