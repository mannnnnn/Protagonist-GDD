using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public partial class DialogBehavior : MonoBehaviour, DialogTarget
{
    Dialog dialog;
    DialogDisplayBehavior display;

    void Start()
    {
        // get display
        display = GetComponent<DialogDisplayBehavior>();
    }

    void Update()
    {
        if (display.active && Input.GetMouseButtonDown(0))
        {
            dialog.Run(this);
        }
        else if (display.state == DialogDisplayBehavior.State.CLOSED)
        {
            // load dialog
            dialog = DialogLoader.ReadFile("testcase.protd");
            display.SetState(DialogDisplayBehavior.State.OPENING);
            dialog.Run(this);
        }
    }

    public void Display(string character, string text, Dictionary<string, object> statement)
    {
        display.SetText(character, text);
    }

    public bool Run(Dictionary<string, object> statement, Dialog dialog)
    {
        Debug.Log("Run: " + string.Join(",", statement.Keys.Select(x => x.ToString()).ToArray()));
        // show statement
        if (statement.ContainsKey("show"))
        {
            if (statement["show"] is Dictionary<string, object>)
            {
                var show = (Dictionary<string, object>)statement["show"];
                ShowAction(show);
            }
            else
            {
                throw new ParseError("Statement 'show' must be a JSON object.");
            }
        }
        return true;
    }

    public void Finish(Dialog dialog)
    {
        Debug.Log("done");
        display.SetState(DialogDisplayBehavior.State.CLOSING);
    }

    public List<Dictionary<string, object>> GetMenu(List<Dictionary<string, object>> menu, string type = "Default")
    {
        return menu;
    }
}
