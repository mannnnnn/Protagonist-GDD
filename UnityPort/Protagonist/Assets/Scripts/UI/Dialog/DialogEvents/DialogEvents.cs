using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/**
 * This handles the "event" statement in dialog scripts.
 * Register them in the Handle method.
 * Use events for custom actions that call game code.
 */
public partial class DialogEvents : MonoBehaviour
{
    const char evtDelimiter = '.';

    // register your dialog events here
    public bool Handle(string evt, Dictionary<string, object> args)
    {
        // handle map-specific events, such as "jungle.kickProtagonist"
        if (evt.Contains(evtDelimiter))
        {
            string[] evtPath = evt.Split(evtDelimiter);
            string map = evtPath[0];
            string evtName = string.Join(evtDelimiter.ToString(), evtPath.Skip(1).Take(evtPath.Length - 1));
            switch (map)
            {
                case "intro":
                    return HandleIntro(evtName, args);
                case "jungle":
                    return HandleJungle(evtName, args);
            }
        }
        // handle normal events
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
                Dialog.GetDisplay().Show();
                return true;
            case "hide":
                Dialog.GetDisplay().Stop();
                return true;
            // change name
            case "changeName":
                return ChangeName(evt, args);
            case "background":
                return ChangeBackground(evt, args);
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
        Dialog.GetInstance().parser.characters[abbrev].name = character;
        return true;
    }

    private bool ChangeBackground(string evt, Dictionary<string, object> args)
    {
        string image = GetStringArgument(evt, args, "image");
        int layer = (int)Math.Floor(GetNumberArgument(evt, args, "layer", true));
        UIBackground.TransitionTo(image, layer);
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
        return args[key] != null ? args[key].ToString() : null;
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
