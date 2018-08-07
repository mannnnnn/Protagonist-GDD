using Assets.Scripts.Libraries.ProtagonistDialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DialogMenu
{
    List<Dictionary<string, object>> Initialize(List<Dictionary<string, object>> options, Dialog dialog, DialogDisplayBehavior display);
}

/**
 * Base class for dialog menus.
 * These are automatically created by the dialog system.
 * The UI functionality is basically just a stripped-down version of the DialogDisplay.
 */
public class DialogMenuBehavior : MonoBehaviour, DialogMenu
{
    // prefab passed in through inspector
    public GameObject button;

    RectTransform rect;

    Dialog dialog;
    DialogDisplayBehavior display;

    List<string> options = new List<string>();
    List<GameObject> buttons = new List<GameObject>();

    // called by Dialog through DialogBehavior
    public List<Dictionary<string, object>> Initialize(List<Dictionary<string, object>> options, Dialog dialog, DialogDisplayBehavior display)
    {
        this.dialog = dialog;
        this.display = display;
        foreach (Dictionary<string, object> option in options)
        {
            if (!option.ContainsKey("text"))
            {
                throw new ParseError("Menu option does not contain 'text' element.");
            }
            this.options.Add((string)option["text"]);
        }
        // we can edit the list of options passed back to dialog, but we don't need to here.
        return options;
    }

    // Hopefully runs after Initialize
    protected virtual void Start () {
        display.SetTargetY(Screen.height - 5);
    }
}
