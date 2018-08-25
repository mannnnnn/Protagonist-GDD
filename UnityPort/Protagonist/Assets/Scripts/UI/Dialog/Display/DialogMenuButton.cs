using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Default dialog menu button control.
 * This outputs back to DialogMenu when it is clicked via menu.FinishSelection().
 */
public class DialogMenuButton : UIDisplayBase
{
    StandardDialogMenu menu;
    
    [HideInInspector] public UIPanel box;

    // ow the edge
    float leftEdge;
    float rightEdge;

    // player clicked or not
    public bool selected { get; private set; }

    // Awake and not Start because Initialize is called right after this is created, no time for Start to run
    protected override void Awake()
    {
        base.Awake();
        box = GetComponent<UIPanel>();
        SetState(State.OPENING);
    }

    public void Initialize(StandardDialogMenu menu, float screenY, float left, float right)
    {
        this.menu = menu;
        SetY(screenY);
        SetTargetY(screenY);
        // expand leftwards animation
        leftEdge = left;
        rightEdge = right;
        box.UpdateAnchors(left, right);
        // start out transparent
        SetAlpha(0);
    }

    protected override void Update()
    {
        base.Update();
        // opening animation
        if (state != State.OPEN)
        {
            box.UpdateAnchors(leftEdge, Mathf.Lerp(leftEdge, rightEdge, timer));
        }
        // handle click on this button
        if (Input.GetMouseButtonDown(0))
        {
            if (ScreenResolution.GetScreenRect(box.rect).Contains(Input.mousePosition))
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
