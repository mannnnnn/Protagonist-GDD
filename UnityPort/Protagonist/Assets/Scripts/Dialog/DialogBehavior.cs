using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogBehavior : MonoBehaviour, DialogTarget
{
    DialogTextbox dialogBox;
    DialogTextbox nameBox;
    NameBoxBehavior setNameBox;

    Dialog dialog;
    bool active = true;

    void Start()
    {
        // get components
        dialogBox = new DialogTextbox(GameObject.FindGameObjectWithTag("DialogueBox"));
        nameBox = new DialogTextbox(GameObject.FindGameObjectWithTag("NameBox"));
        setNameBox = GetComponentInChildren<NameBoxBehavior>();
        // load dialog
        dialog = DialogLoader.ReadFile("testcase.protd");
        SetAlpha(1);
        dialog.Run(this);
    }

    void Update()
    {
        if (active && Input.GetMouseButtonDown(0))
        {
            dialog.Run(this);
        }
    }

    public void Display(string character, string text, Dictionary<string, object> statement)
    {
        SetText(character, text);
    }

    public bool Run(Dictionary<string, object> statement, Dialog dialog)
    {
        Debug.Log("Run: " + string.Join(",", statement.Keys.Select(x => x.ToString()).ToArray()));
        return true;
    }

    public void Finish(Dialog dialog)
    {
        Debug.Log("done");
        active = false;
        SetAlpha(0);
    }

    public List<Dictionary<string, object>> GetMenu(List<Dictionary<string, object>> menu, string type = "Default")
    {
        return menu;
    }

    public void SetText(string character, string text)
    {
        setNameBox.SetName(character);
        dialogBox.SetText(text);
    }
    public void SetAlpha(float alpha)
    {
        nameBox.SetAlpha(alpha);
        dialogBox.SetAlpha(alpha);
    }
    public void SetPosition(Vector2 pos)
    {
        nameBox.SetPosition(pos);
        dialogBox.SetPosition(pos);
    }

    // helper class used to wrap setting alpha/position/text into one object per GameObject.
    // Then, this behavior wraps it into one method call.
    class DialogTextbox
    {
        GameObject gameObj;
        InputField textbox;
        Text text;
        Image image;
        Vector2 initialPos;

        public DialogTextbox(GameObject g)
        {
            gameObj = g;
            textbox = g.GetComponentInChildren<InputField>();
            text = g.GetComponentInChildren<Text>();
            image = g.GetComponentInChildren<Image>();
            initialPos = g.transform.localPosition;
        }

        public void SetText(string message)
        {
            textbox.text = message;
        }
        public void SetAlpha(float alpha)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            image.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }
        public void SetPosition(Vector2 pos)
        {
            gameObj.transform.localPosition = initialPos + pos;
        }
    }
}
