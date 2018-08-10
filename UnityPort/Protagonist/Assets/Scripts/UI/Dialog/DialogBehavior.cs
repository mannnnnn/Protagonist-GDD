using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/**
 * This is the main DialogBehavior class file.
 * This class bridges the script execution logic of the Dialog class to the output of the DialogDisplayBehavior.
 * It also handles statements that the Dialog object cannot directly execute, such as:
 * "show", "hide", "event", and ""
 * These cannot be directly handled by Dialog since these need the Unity Engine access.
 * For the output display for the dialog, see DialogDisplayBehavior.
 * For the script execution logic, see Dialog.
 * For the stick-on addition to DialogBehavior that holds the various statements it handles like "show", see DialogBehaviorActions
 */
public partial class DialogBehavior : MonoBehaviour, DialogTarget
{
    public Dialog dialog { get; private set; }
    DialogEvents events;
    DialogDisplayBehavior display;

    void Start()
    {
        // get display
        display = GetComponentInChildren<DialogDisplayBehavior>();
        events = GetComponent<DialogEvents>();
    }

    void Update()
    {
        if (display.state == DialogDisplayBase.State.CLOSED && Input.GetKeyDown(KeyCode.Z))
        {
            // load dialog
            dialog = DialogLoader.ReadFile("testcase.protd");
            display.SetState(DialogDisplayBase.State.OPENING);
            dialog.Run(this);
        }
        if (Input.GetMouseButtonDown(0) && display.active)
        {
            dialog.Run(this);
        }
    }

    public void Display(List<string> characters, string text, Dictionary<string, object> statement)
    {
        display.SetText(characters, text, dialog);
    }

    public bool Run(Dictionary<string, object> statement, Dialog dialog)
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

    public void Finish(Dialog dialog)
    {
        display.SetState(DialogDisplayBase.State.PENDING_CLOSE);
    }

    public List<Dictionary<string, object>> GetMenu(List<Dictionary<string, object>> menu, string type = "Default")
    {
        // the menu created will call dialog.ChooseMenuOption(option) so we don't need to
        return display.SetMenu(menu, type, dialog);
    }
}
