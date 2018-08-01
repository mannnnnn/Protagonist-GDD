using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Libraries.ProtagonistDialog
{
    public class Dialog
    {
        // defined characters and labels
        Dictionary<string, DialogCharacter> characters = new Dictionary<string, DialogCharacter>();
        Dictionary<string, DialogLabel> labels = new Dictionary<string, DialogLabel>();

        // call stack for if statements and jumps
        Stack<int> callStack = new Stack<int>();
    }

    public class DialogCharacter
    {

    }

    public class DialogLabel
    {

    }
}
