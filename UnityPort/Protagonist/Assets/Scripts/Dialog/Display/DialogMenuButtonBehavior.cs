using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Default dialog menu button control.
 */
public class DialogMenuButtonBehavior : DialogDisplayBase
{
    DialogMenuBehavior menu;

    AdjustUIBehavior width;
    public DialogTextbox box;

    // ow the edge
    float leftEdge;
    float rightEdge;

    // player clicked or not
    public bool selected { get; private set; }

    // Awake and not Start because Initialize is called right after this is created, no time for Start to run
    void Awake()
    {
        box = new DialogTextbox(gameObject);
        width = GetComponent<AdjustUIBehavior>();
        SetState(State.OPENING);
    }

    public void Initialize(DialogMenuBehavior menu, float screenY, float left, float right)
    {
        this.menu = menu;
        SetY(screenY);
        SetTargetY(screenY);
        // expand leftwards animation
        leftEdge = left;
        rightEdge = right;
        width.UpdateAnchors(left, right);
    }

    protected override void Update()
    {
        base.Update();
        // opening animation
        if (state != State.OPEN)
        {
            width.UpdateAnchors(leftEdge, Mathf.Lerp(leftEdge, rightEdge, timer));
        }
        // handle click on this button
        if (Input.GetMouseButtonDown(0))
        {
            if (box.GetScreenRect().Contains(Input.mousePosition))
            {
                selected = true;
                menu.FinishSelection();
            }
        }
    }

    public override float GetY()
    {
        return box.GetScreenY();
    }
    protected override void SetY(float screenY)
    {
        box.SetScreenY(screenY);
    }
    public override void SetAlpha(float alpha)
    {
        box.SetAlpha(alpha);
    }
    public void SetText(string text)
    {
        box.SetText(text);
    }
}
