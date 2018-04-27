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
        public ListNode node;
        public LabelType type;
        public int location = int.MinValue;

        public LabelDefinition(string id, ListNode node, LabelType type)
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
