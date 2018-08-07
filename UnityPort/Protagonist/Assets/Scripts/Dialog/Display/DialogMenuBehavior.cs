using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
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
public class DialogMenuBehavior : DialogDisplayBase, DialogMenu
{
    // prefab passed in through inspector
    public GameObject button;

    RectTransform rect;

    // uses dialog object by calling dialog.ChooseMenuOption
    Dialog dialog;
    DialogDisplayBehavior display;

    List<string> options = new List<string>();
    List<GameObject> buttons = new List<GameObject>();

    // note that since the back panel has no text, using SetText will throw.
    DialogTextbox backPanel;

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
        // we could edit the list of options passed back to dialog, but there's no reason to.
        return options;
    }

    // Hopefully runs after Initialize
    protected virtual void Start()
    {
        backPanel = new DialogTextbox(transform.Find("BackPanel").gameObject);
        // move normal dialog window to the top
        display.SetTargetY(Screen.height - 5);
        // move up
        SetY(0);
        SetTargetY(GetSize() + 5);
        // fade in
        SetState(State.OPENING);
    }

    // y position
    public override float GetY()
    {
        return ResolutionHandler.RectToScreenPoint(new Vector2(0, gameObject.transform.position.y)).y;
    }
    protected override void SetY(float screenY)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
            ResolutionHandler.ScreenToRectPoint(new Vector2(0, screenY)).y, gameObject.transform.position.y);
    }
    public float GetSize()
    {
        return backPanel.GetSize();
    }

    public override void SetAlpha(float alpha)
    {
        backPanel.SetAlpha(alpha);
    }
}
