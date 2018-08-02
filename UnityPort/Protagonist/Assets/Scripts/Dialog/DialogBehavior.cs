using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogBehavior : MonoBehaviour, DialogTarget
{
    InputField dialogBox;
    InputField nameBox;
    Dialog dialog;

    void Start()
    {
        GameObject dBox = GameObject.FindGameObjectWithTag("DialogueBox");
        dialogBox = dBox.GetComponent<InputField>();
        GameObject nBox = GameObject.FindGameObjectWithTag("NameBox");
        nameBox = nBox.GetComponent<InputField>();
        // load dialog
        dialog = DialogLoader.ReadFile("testcase.protd");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dialog.Run(this);
        }
    }

    public void Display(string character, string text, Dictionary<string, object> statement)
    {
        nameBox.text = character;
        dialogBox.text = text;
    }

    public bool Run(Dictionary<string, object> statement, Dialog dialog)
    {
        Debug.Log("Run: " + string.Join(",", statement.Keys.Select(x => x.ToString()).ToArray()));
        return true;
    }

    public void Finish(Dialog dialog)
    {
        Debug.Log("done");
    }

    public List<Dictionary<string, object>> GetMenu(List<Dictionary<string, object>> menu, string type = "Default")
    {
        return menu;
    }
}
