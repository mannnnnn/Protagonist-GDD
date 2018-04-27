using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // the valid types of statements
    public enum StatementType
    {
        DISPLAY, NAME_DISPLAY, SHOW, HIDE, WITH, CONTROL, EVENT, PLAY, STOP, PAUSE, EXIT, JUMP, BRANCH, LABEL, NONE
    }

    // a ParseStatement is one, single executable statement. The fully compiled result is a List of these.
    // all statements are executable
    public abstract class ParseStatement : ParseNode
    {
        // returns true to pause, false to continue
        public virtual bool Execute()
        {
            return false;
        }

        public IEnumerator<ParseStatement> GetEnumerator(int current)
        {
            // return an enumerator only containing this statement
            return (new List<ParseStatement> { this }).GetEnumerator();
        }
    }
}
