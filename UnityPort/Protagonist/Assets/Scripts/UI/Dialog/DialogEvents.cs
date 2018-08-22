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
public class DialogEvents : MonoBehaviour {

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
        }
        return true;
    }

    // freeze dialog for a given duration
    private bool Wait(string evt, Dictionary<string, object> args)
    {
        // must contain a valid duration argument to run
        if (!args.ContainsKey("duration"))
        {
            throw new ParseError("'wait' event has no duration argument.");
        }
        float duration = 0;
        try
        {
            duration = float.Parse(args["duration"].ToString(), CultureInfo.InvariantCulture);
        }
        catch (FormatException)
        {
            throw new ParseError("'" + args["duration"].ToString() + "' is not a valid number.");
        }
        // freeze dialog
        DialogFreezer freezer = gameObject.AddComponent<DialogFreezer>();
        freezer.Initialize(duration);
        return false;
    }
}
