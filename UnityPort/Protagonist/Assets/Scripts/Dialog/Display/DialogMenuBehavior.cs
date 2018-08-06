using Assets.Scripts.Libraries.ProtagonistDialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Base class for dialog menus.
 * These are automatically created by the dialog system.
 */
public class DialogMenuBehavior : MonoBehaviour {

    Dialog dialog;
    DialogDisplayBehavior display;

    List<string> options = new List<string>();
    List<GameObject> buttons = new List<GameObject>();
    public GameObject button;

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
        // don't need to reshape options for default behavior
        return options;
    }

    // Hopefully runs after Initialize
    void Start () {
        // create a button for each option
        Instantiate(button, transform);
	}
	
	void Update () {
		
	}
}
