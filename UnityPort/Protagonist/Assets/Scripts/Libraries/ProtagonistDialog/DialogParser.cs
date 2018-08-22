using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Libraries.ProtagonistDialog
{
    /**
     * Master of dialog script execution. Takes in parsed json, and can execute it as protagonist dialog.
     * Handles all control flow, such as jumps and ifs, but cannot handle anything that accesses the Unity Engine.
     * That responsibility is passed to the given DialogTarget.
     * See Dialog, which is the DialogTarget this outputs to.
     * The constructor for DialogParser objects is directly accessible to the Unity scripts. To obtain a DialogParser object, see DialogLoader.
     */
    public class DialogParser
    {
        // defined characters and labels
        public Dictionary<string, DialogCharacter> characters = new Dictionary<string, DialogCharacter>();
        public Dictionary<string, DialogLabel> labels = new Dictionary<string, DialogLabel>();
        public static Dictionary<string, bool> flags = new Dictionary<string, bool>();

        // current position in execution
        int current = 0;
        // the current execution block. A 'block' is an executable list of objects.
        List<Dictionary<string, object>> currentBlock;

        // call stack for if statements, menus and jumps
        Stack<CallStackEntry> callStack = new Stack<CallStackEntry>();
        // main block
        List<Dictionary<string, object>> block;

        // handle menu
        public List<Dictionary<string, object>> menu { get; private set; }

        // represents an entry on the call stack
        struct CallStackEntry
        {
            internal int position;
            internal List<Dictionary<string, object>> block;
            public CallStackEntry(int position, List<Dictionary<string, object>> block)
            {
                this.position = position;
                this.block = block;
            }
        }
        
        internal DialogParser(List<object> json)
        {
            block = CastBlock(json);
            currentBlock = block;
            current = 0;
            // process current block for labels and characters
            PreprocessBlock(currentBlock);
        }

        // advance along the current dialog block
        public void Run(DialogTarget target)
        {
            // do nothing if a menu is open
            if (menu != null)
            {
                return;
            }
            bool run = true;
            while (run)
            {
                // check if we're complete with the current block
                while (current >= currentBlock.Count)
                {
                    // if the call stack is empty, finish
                    if (callStack.Count == 0)
                    {
                        target.Finish(this);
                        return;
                    }
                    // otherwise, jump to the previous context
                    CallStackEntry entry = callStack.Pop();
                    current = entry.position;
                    currentBlock = entry.block;
                }

                // fetch and run statement
                var statement = currentBlock[current];
                // check for control statements: 
                if (statement.ContainsKey("if"))
                {
                    RunIf(statement, target);
                    current--;
                }
                else if (statement.ContainsKey("jump"))
                {
                    RunJump(statement);
                    current--;
                }
                else if (statement.ContainsKey("var"))
                {
                    RunVar(statement, target);
                }
                else if (statement.ContainsKey("menu"))
                {
                    if (!IsBlock(statement["menu"]))
                    {
                        throw new ParseError("Statement 'menu' must be of type 'block'.");
                    }
                    string type = "Default";
                    if (statement.ContainsKey("menuType"))
                    {
                        type = (string)statement["menuType"];
                    }
                    menu = target.GetMenu(CastBlock(statement["menu"]), type);
                    run = false;
                }
                else if (statement.ContainsKey("label"))
                {
                    // ignore, as it is preprocessed on jumping to the block
                }
                else if (statement.ContainsKey("character"))
                {
                    // ignore, as it is preprocessed on jumping to the block
                }
                // display statement
                else
                {
                    if (statement.Count == 0)
                    {
                        throw new ParseError("Empty object is not a valid statement.");
                    }
                    // check for display statement
                    foreach (string key in statement.Keys)
                    {
                        // if one character
                        if (characters.ContainsKey(key))
                        {
                            target.Display(new List<string>(){ key }, (string)statement[key], statement);
                            run = false;
                        }
                        // if multiple characters, separated by |
                        if (key.Contains('|'))
                        {
                            List<string> chars = key.Split('|').ToList();
                            // make sure all are characters
                            foreach (string charKey in chars)
                            {
                                if (!characters.ContainsKey(charKey))
                                {
                                    throw new ParseError("Character '" + charKey + "' has not been defined, but is used in '" + key + "'.");
                                }
                            }
                            target.Display(chars, (string)statement[key], statement);
                            run = false;
                        }
                    }
                    // if not a control statement and not a display statement, pass it to the target to run
                    if (run)
                    {
                        run = target.Run(statement, this);
                    }
                }
                current++;
            }
        }

        // called by the dialog target when a menu option is chosen
        public void ChooseMenuOption(int option, DialogTarget target)
        {
            if (menu == null || option < 0 || option >= menu.Count)
            {
                throw new ParseError("Option or menu does not exist.");
            }
            var menuOption = menu[option];
            if (!menuOption.ContainsKey("block"))
            {
                throw new ParseError("Menu option must have the 'block' element defined.");
            }
            if (!IsBlock(menuOption["block"]))
            {
                throw new ParseError("Menu option 'block' element must be of type 'block'.");
            }
            current--;
            JumpToBlock(menuOption["block"]);
            menu = null;
            Run(target);
        }

        private void RunIf(Dictionary<string, object> statement, DialogTarget target)
        {
            if (!statement.ContainsKey("cond"))
            {
                throw new ParseError("If statements must have the 'cond' element defined.");
            }
            // evaluate expression
            bool expr = false;
            if (statement["cond"] is bool)
            {
                expr = (bool)statement["cond"];
            }
            else if (statement["cond"] is string)
            {
                expr = DialogBoolParser.Parse((string)statement["cond"]).Run();
            }
            else
            {
                throw new ParseError("If statement 'cond' element must be of type 'bool' or type 'string'.");
            }
            // run if or else
            if (expr)
            {
                if (!IsBlock(statement["if"]))
                {
                    throw new ParseError("If statement 'if' element must be of type 'block'.");
                }
                JumpToBlock(statement["if"]);
            }
            else if (statement.ContainsKey("else"))
            {
                if (!IsBlock(statement["else"]))
                {
                    throw new ParseError("If statement 'else' element must be of type 'block'.");
                }
                JumpToBlock(statement["else"]);
            }
        }

        public void Jump(string label)
        {
            RunJump(new Dictionary<string, object>() { { "jump", label } });
        }
        private void RunJump(Dictionary<string, object> statement)
        {
            string jump = (string)statement["jump"];
            if (!labels.ContainsKey(jump))
            {
                throw new ParseError("Label '" + jump + "' does not exist.");
            }
            // method call
            if (labels[jump].IsMethod)
            {
                JumpToBlock(labels[jump].block);
            }
            // goto
            else
            {
                currentBlock = labels[jump].context;
                current = labels[jump].position;
            }
        }

        private void RunVar(Dictionary<string, object> statement, DialogTarget target)
        {
            string flag = (string)statement["var"];
            var val = statement["val"];
            if (!statement.ContainsKey("val"))
            {
                throw new ParseError("Statement 'var' must contain a 'val' element.");
            }
            if (val is bool)
            {
                flags[flag] = (bool)val;
            }
            else if (val is string)
            {
                flags[flag] = DialogBoolParser.Parse((string)val).Run();
            }
            else
            {
                throw new ParseError("Var statement 'val' element must be of type 'bool' or type 'string'.");
            }
        }

        private void RunCharacter(Dictionary<string, object> statement)
        {
            // name and abbrev
            var name = (string)statement["character"];
            var abbrev = (string)statement["abbrev"];
            var position = "Left";
            if (statement.ContainsKey("side"))
            {
                position = (string)statement["side"];
            }
            characters[abbrev] = new DialogCharacter(name, abbrev, position);
        }

        private void RunLabel(Dictionary<string, object> statement, List<Dictionary<string, object>> context, int position)
        {
            // name/contents, and context/position
            var label = (string)statement["label"];
            List<Dictionary<string, object>> labelBlock = null;
            if (statement.ContainsKey("block"))
            {
                labelBlock = CastBlock(statement["block"]);
            }
            labels[label] = new DialogLabel(label, labelBlock, context, position);
        }

        // jump to a given block, pushing the stack.
        private void JumpToBlock(object block)
        {
            if (!IsBlock(block))
            {
                throw new ParseError("Cannot jump to a statement that is not of type 'block'.");
            }
            callStack.Push(new CallStackEntry(current + 1, currentBlock));
            current = 0;
            currentBlock = CastBlock(block);
            PreprocessBlock(currentBlock);
        }

        private List<Dictionary<string, object>> CastBlock(object block)
        {
            if (block is List<Dictionary<string, object>>)
            {
                return (List<Dictionary<string, object>>)block;
            }
            return ((List<object>)block).Cast<Dictionary<string, object>>().ToList();
        }

        // scan for labels and characters defined
        private void PreprocessBlock(List<Dictionary<string, object>> block)
        {
            // pre-process characters and labels
            for (int i = 0; i < block.Count; i++)
            {
                var statement = block[i];
                // character
                if (statement.ContainsKey("character"))
                {
                    RunCharacter(statement);
                }
                // label
                if (statement.ContainsKey("label"))
                {
                    RunLabel(statement, block, i);
                }
            }
        }

        // checks if a block is a valid block
        private bool IsBlock(object block)
        {
            CastBlock(block);
            return true;
        }
    }

    // a defined character in dialog
    public class DialogCharacter
    {
        public string name { get; internal set; }
        public string abbrev { get; internal set; }
        public string position { get; set; }

        // these are set later on in a 'show' statement
        public string sprite { get; set; }
        public GameObject gameObject { get; set; }
        public string image { get; set; }
        public string transition { get; set; }

        public DialogCharacter(string name, string abbrev, string position)
        {
            this.name = name;
            this.abbrev = abbrev;
            this.position = position;
            transition = "Default";
            image = "Normal";
        }
    }

    // a label you can jump to
    public class DialogLabel
    {
        public string label { get; internal set; }
        public List<Dictionary<string, object>> block { get; internal set; }
        public bool IsMethod { get { return block != null; } }

        public List<Dictionary<string, object>> context { get; internal set; }
        public int position { get; internal set; }

        public DialogLabel(string label, List<Dictionary<string, object>> block,
            List<Dictionary<string, object>> context, int position)
        {
            this.label = label;
            this.block = block;
            this.context = context;
            this.position = position;
        }
    }

    // DialogParser object outputs to one of these
    public interface DialogTarget
    {
        // return false means stop dialog execution (e.g. to wait 1 second). Return true means keep going.
        bool Run(Dictionary<string, object> statement, DialogParser dialog);
        // when a character or "" is specified, this is called in the same fashion as Run
        void Display(List<string> character, string text, Dictionary<string, object> statement);
        // inform that dialog is finished with execution
        void Finish(DialogParser dialog);
        // get custom menu
        List<Dictionary<string, object>> GetMenu(List<Dictionary<string, object>> menu, string type = "Default");
    }
}
