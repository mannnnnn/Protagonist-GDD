using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class AccessBooleanNode : BooleanNode
    {
        string id;

        // tries to access the dialogue variable dictionary to get a boolean value
        public AccessBooleanNode(string id)
        {
            this.id = id;
        }

        // evaluate the value of one variable
        public override bool Evaluate()
        {
            Console.WriteLine("Attempt to access variable " + id);
            if (id == "true")
            {
                return true;
            }
            if (id == "false")
            {
                return false;
            }
            // TODO: access some dictionary somewhere
            return false;
        }
    }
}
