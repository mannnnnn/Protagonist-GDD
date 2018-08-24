using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/**
 * This handles the "event" statement in dialog scripts.
 * Register them in the Handle method.
 * Use events for custom actions that call game code.
 */
public class DialogEvents : MonoBehaviour
{
    Dialog dialog;
    DialogDisplay display;
    void Start()
    {
        dialog = GetComponent<Dialog>();
        display = GetComponent<DialogDisplay>();
    }

    // register your dialog events here
    public bool Handle(string evt, Dictionary<string, object> args)
    {
        switch (evt)
        {
            case "Hi":
                Debug.Log(args["message"]);
                break;
            // waits for a click (which is what return false does)
            case "pause":
                return false;
            case "wait":
                return Wait(evt, args);
            // show/hide dialog display
            case "show":
                display.SetState(UIDisplayBase.State.OPENING);
                return true;
            case "hide":
                display.SetState(UIDisplayBase.State.CLOSING);
                return true;
            // change name
            case "changeName":
                return ChangeName(evt, args);
        }
        return true;
    }

    // freeze dialog for a given duration
    private bool Wait(string evt, Dictionary<string, object> args)
    {
        float duration = GetNumberArgument(evt, args, "duration");
        // freeze dialog
        DialogFreezer freezer = gameObject.AddComponent<DialogFreezer>();
        freezer.Initialize(duration);
        return false;
    }

    private bool ChangeName(string evt, Dictionary<string, object> args)
    {
        string character = GetStringArgument(evt, args, "character");
        string abbrev = GetStringArgument(evt, args, "abbrev");
        dialog.parser.characters[abbrev].name = character;
        return true;
    }
    
    private string GetStringArgument(string evt, Dictionary<string, object> args, string key, bool optional = false)
    {
        if (!args.ContainsKey(key))
        {
            if (optional)
            {
                return null;
            }
            throw new ParseError("'" + evt + "' event has no " + key + " argument.");
        }
        return args[key].ToString();
    }
    private float GetNumberArgument(string evt, Dictionary<string, object> args, string key, bool optional = false)
    {
        float value = 0f;
        try
        {
            value = float.Parse(GetStringArgument(evt, args, key, optional), CultureInfo.InvariantCulture);
        }
        catch (FormatException)
        {
            if (optional)
            {
                return 0f;
            }
            throw new ParseError("'" + GetStringArgument(evt, args, key, optional) + "' is not a valid number.");
        }
        return value;
    }
}
