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
 * Creates buttons given an options list, and waits for the player to choose one,
 * then calls dialog.ChooseMenuOption as is required for menus.
 */
public class DialogMenuBehavior : UIDisplayBase, DialogMenu
{
    // prefab passed in through inspector
    public GameObject button;

    // uses dialog object by calling dialog.ChooseMenuOption
    Dialog dialog;
    DialogTarget target;
    DialogDisplayBehavior display;
    float displayY;

    List<string> options = new List<string>();
    List<DialogMenuButtonBehavior> buttons = new List<DialogMenuButtonBehavior>();

    // note that since the back panel has no text, using SetText will throw.
    UIPanel backPanel;

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
        backPanel = GetComponent<UIPanel>();
        // move up
        SetY(-100f);
        SetTargetY(1.5f * GetSize() + 20);
        // start out transparent
        SetAlpha(0);
        // fade in this menu
        SetState(State.OPENING);
        // create menu sub-items
        float buttonBorder = 5f;
        float edgeBorder = 12f;
        // 0 is the top, so go downwards
        // so, move it down by (border + buttonSize) each time.
        float totalSize = edgeBorder;
        float offset = totalSize + (2f * edgeBorder) + buttonBorder;
        for (int i = 0; i < options.Count; i++)
        {
            GameObject buttonObj = Instantiate(button, transform);
            // move button to y position
            var buttonBehavior = buttonObj.GetComponent<DialogMenuButtonBehavior>();
            totalSize += buttonBehavior.box.GetSize() + buttonBorder;
            buttonBehavior.Initialize(this, -totalSize + offset, 0, 1);
            // set button as option
            buttonBehavior.SetText(options[i]);
            buttons.Add(buttonBehavior);
        }
        // resize back panel and move up to fit buttons
        totalSize += edgeBorder;
        backPanel.SetSize(totalSize, 1f);
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
        Destroy(gameObject);
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
    
    public float GetSize()
    {
        return backPanel.GetSize();
    }

    public override void SetAlpha(float alpha)
    {
        backPanel.SetAlpha(alpha);
    }
}
