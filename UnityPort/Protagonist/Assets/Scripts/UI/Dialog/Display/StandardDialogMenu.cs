using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DialogMenu
{
    List<Dictionary<string, object>> Initialize(List<Dictionary<string, object>> options,
        DialogParser dialog, DialogTarget target, DialogDisplay display);
}

/**
 * Base class for dialog menus.
 * These are automatically created by the dialog system.
 * Creates buttons given an options list, and waits for the player to choose one,
 * then calls dialog.ChooseMenuOption as is required for menus.
 */
public class StandardDialogMenu : UIDisplayBase, DialogMenu
{
    // prefab passed in through inspector
    public GameObject button;

    // uses dialog object by calling dialog.ChooseMenuOption
    DialogParser parser;
    DialogTarget target;
    DialogDisplay display;
    float displayY;

    List<string> options = new List<string>();
    List<DialogMenuButton> buttons = new List<DialogMenuButton>();

    // note that since the back panel has no text, using SetText will throw.
    UIPanel backPanel;

    // called by DialogParser through Dialog
    public List<Dictionary<string, object>> Initialize(List<Dictionary<string, object>> options,
        DialogParser parser, DialogTarget target, DialogDisplay display)
    {
        this.parser = parser;
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
        // calculate total size and resize. Total size looks like -B|B|B|B- for button 'B', edge '-', and button border '|'
        float buttonBorder = 5f;
        float edgeBorder = 12f;
        float buttonSize = 52f;
        float totalSize = (2 * edgeBorder) + (options.Count * buttonSize) + ((options.Count - 1) * buttonBorder);
        backPanel.SetSize(totalSize, 1f);
        // move up
        SetY(-100f);
        SetTargetY(GetSize());
        // start out transparent
        SetAlpha(0);
        // fade in this menu
        SetState(State.OPENING);
        // place buttons
        float buttonY = -(2 * edgeBorder) - (0.5f * buttonSize);
        for (int i = 0; i < options.Count; i++)
        {
            buttonY -= buttonSize;
            GameObject buttonObj = Instantiate(button, transform);
            // move button to y position
            var buttonBehavior = buttonObj.GetComponent<DialogMenuButton>();
            buttonBehavior.Initialize(this, buttonY, 0, 1);
            // set button as option
            buttonBehavior.SetText(options[i]);
            buttons.Add(buttonBehavior);
            buttonY -= buttonBorder;
        }
        // move normal dialog window to the top
        displayY = display.GetY();
        display.SetTargetY(GetSize() + display.GetSize() + edgeBorder);
    }

    protected override void Update()
    {
        base.Update();
        backPanel.UpdateAnchors();
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
        parser.ChooseMenuOption(chosen, target);
        Destroy(gameObject);
    }

    // called by a button when a choice is selected
    public void FinishSelection()
    {
        // close the buttons, so you can't choose another
        foreach (DialogMenuButton button in buttons)
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
