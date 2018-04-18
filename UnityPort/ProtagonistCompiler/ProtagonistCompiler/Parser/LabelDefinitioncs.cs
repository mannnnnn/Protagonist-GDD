using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public enum LabelType
    {
        GOTO, CALL
    }

    // holds data on a label definition
    public class LabelDefinition
    {
        public string id;
        public ParseNode node;
        public LabelType type;

        public LabelDefinition(string id, ParseNode node, LabelType type)
        {
            this.id = id;
            this.node = node;
            this.type = type;
        }

        // label data to string
        public override string ToString()
        {
            return "{ labelID = " + id + ", type = " + type + " }";
        }
    }
}
