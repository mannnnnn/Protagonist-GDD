using Assets.Scripts.Libraries.ProtagonistDialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogMenuBehavior : MonoBehaviour {

    Dialog dialog;
    List<string> options = new List<string>();
    public List<Dictionary<string, object>> Initialize(List<Dictionary<string, object>> options, Dialog dialog)
    {
        this.dialog = dialog;
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


	}
	
	void Update () {
		
	}
}
