using Assets.Scripts.Controllers;
using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/**
 * This is the main Dialog class file.
 * Use Dialog.RunDialog(file, ?label) to start dialog.
 * This class bridges the script execution logic of the DialogParser class to the output of the DialogDisplayBehavior.
 * It also handles statements that the DialogParser object cannot directly execute, such as:
 * "show", "hide", "event", and ""
 * These cannot be directly handled by DialogParser since these need the Unity Engine access.
 * For the output display for the dialog, see DialogDisplayBehavior.
 * For the script execution logic, see DialogParser.
 * For the stick-on addition to Dialog that holds the various statements it handles like "show", see DialogActions
 */
public partial class Dialog : MonoBehaviour, DialogTarget, SaveLoadTarget
{
    static Dialog instance;

    public DialogParser parser { get; private set; }
    DialogEvents events;
    DialogDisplay display;

    public bool Active { get; private set; } = false;

    void Start()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        // get display
        display = GetComponentInChildren<DialogDisplay>();
        events = GetComponent<DialogEvents>();
        SaveLoad.Register("flags", this);
    }

    public static Dialog GetInstance()
    {
        return instance;
    }
    public static void RunDialog(string file, string label = null)
    {
        if (instance.display.state != UIDisplayBase.State.CLOSED)
        {
            return;
        }
        // load dialog file
        instance.parser = DialogLoader.ReadFile(file);
        instance.display.SetState(UIDisplayBase.State.OPENING);
        if (label != null && label != "")
        {
            instance.parser.Jump(label);
        }
        instance.Active = true;
        instance.parser.Run(instance);
    }
    public static void Advance()
    {
        instance.parser.Run(instance);
    }

    public void Display(List<string> characters, string text, Dictionary<string, object> statement)
    {
        display.SetText(characters, text, parser);
    }

    public bool Run(Dictionary<string, object> statement, DialogParser parser)
    {
        // Debug.Log("Run: " + string.Join(",", statement.Keys.Select(x => x.ToString()).ToArray()));
        // nameplate-less statement
        if (statement.ContainsKey(""))
        {
            Display(new List<string>(), (string)statement[""], statement);
            return false;
        }
        // show statement
        if (statement.ContainsKey("show"))
        {
            if (!(statement["show"] is Dictionary<string, object>))
            {
                throw new ParseError("Statement 'show' must be a JSON object.");
            }
            var show = (Dictionary<string, object>)statement["show"];
            ShowAction(show);
            return true;
        }
        // hide statement
        if (statement.ContainsKey("hide"))
        {
            if (!(statement["hide"] is Dictionary<string, object>))
            {
                throw new ParseError("Statement 'hide' must be a JSON object.");
            }
            var hide = (Dictionary<string, object>)statement["hide"];
            HideAction(hide);
            return true;
        }
        // event statement
        if (statement.ContainsKey("event"))
        {
            string evt = (string)statement["event"];
            Dictionary<string, object> args = null;
            if (statement.ContainsKey("args"))
            {
                if (!(statement["args"] is Dictionary<string, object>))
                {
                    throw new ParseError("Element 'args' in statement 'event' must be a JSON object.");
                }
                args = (Dictionary<string, object>)statement["args"];
            }
            return events.Handle(evt, args);
        }
        // if nothing returns above, then throw this
        throw new ParseError("Character or statement '" + statement.Keys.First() + "' does not exist.");
    }

    public void Finish(DialogParser parser)
    {
        display.SetState(UIDisplayBase.State.PENDING_CLOSE);
        instance.Active = false;
    }

    public List<Dictionary<string, object>> GetMenu(List<Dictionary<string, object>> menu, string type = "Default")
    {
        // the menu created will call parser.ChooseMenuOption(option) so we don't need to
        return display.SetMenu(menu, type, parser);
    }

    // save/load flags
    public object GetSaveData()
    {
        return DialogParser.flags;
    }
    public void LoadSaveData(object save)
    {
        DialogParser.flags = (Dictionary<string, bool>)save;
    }
}
