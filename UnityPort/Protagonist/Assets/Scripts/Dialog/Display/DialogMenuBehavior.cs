using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DialogMenu
{
    List<Dictionary<string, object>> Initialize(List<Dictionary<string, object>> options,
        Dialog dialog, DialogTarget target, DialogDisplayBehavior display);
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
    DialogTarget target;
    DialogDisplayBehavior display;
    float displayY;

    List<string> options = new List<string>();
    List<DialogMenuButtonBehavior> buttons = new List<DialogMenuButtonBehavior>();

    // note that since the back panel has no text, using SetText will throw.
    DialogTextbox backPanel;

    // called by Dialog through DialogBehavior
    public List<Dictionary<string, object>> Initialize(List<Dictionary<string, object>> options,
        Dialog dialog, DialogTarget target, DialogDisplayBehavior display)
    {
        this.dialog = dialog;
        this.target = target;
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
        // move up
        SetY(-100f);
        SetTargetY(GetSize() + 5);
        // fade in this menu
        SetState(State.OPENING);
        // create menu sub-items
        float buttonBorder = 5f;
        float edgeBorder = 18f;
        // 0 is the position such that the entire button is just barely offscreen at the bottom.
        // so, move it up by (border + buttonSize) each time.
        float totalSize = edgeBorder - buttonBorder;
        for (int i = options.Count - 1; i >= 0; i--)
        {
            GameObject buttonObj = Instantiate(button, transform);
            // move button to y position
            var buttonBehavior = buttonObj.GetComponent<DialogMenuButtonBehavior>();
            totalSize += buttonBehavior.box.GetSize() + buttonBorder;
            buttonBehavior.Initialize(this, totalSize, 0, 0.8f);
            buttonBehavior.SetText(options[i]);
            // prepend, since we're going bottom to top
            buttons.Insert(0, buttonBehavior);
        }
        // resize back panel and move up to fit buttons
        totalSize += edgeBorder;
        float resize = (totalSize) - GetSize();
        backPanel.SetSize(totalSize);
        // move normal dialog window to the top
        displayY = display.GetY();
        display.SetTargetY(GetSize() + display.GetSize() + edgeBorder);
    }

    // call dialog.ChooseMenuOption when we're done
    protected override void CloseFinish()
    {
        int chosen = 0;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].selected)
            {
                chosen = i;
            }
        }
        dialog.ChooseMenuOption(chosen, target);
    }

    // called by a button when a choice is selected
    public void FinishSelection()
    {
        // close the buttons, so you can't choose another
        foreach (DialogMenuButtonBehavior button in buttons)
        {
            button.SetState(State.PENDING_CLOSE);
        }
        SetState(State.PENDING_CLOSE);
        // slide down and move dialog display back to original position
        SetTargetY(-100f);
        display.SetTargetY(displayY);
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
