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
    float width;

    // player clicked or not
    public bool selected { get; private set; }

    // Awake and not Start because Initialize is called right after this is created, no time for Start to run
    protected override void Awake()
    {
        base.Awake();
        box = GetComponent<UIPanel>();
        SetState(State.OPENING);
    }

    public void Initialize(StandardDialogMenu menu, float screenY, float width)
    {
        this.menu = menu;
        SetY(screenY);
        SetTargetY(screenY);
        // expand leftwards animation
        this.width = width;
        // start out transparent
        SetAlpha(0);
    }

    protected override void Update()
    {
        base.Update();
        // opening animation
        if (state != State.OPEN)
        {
            //TODO: box.size = new Vector2(Mathf.Lerp(0, width, timer), box.size.y);
        }
        // handle click on this button
        if (Input.GetMouseButtonDown(0))
        {
            if (box.rect.Contains(Input.mousePosition))
            {
                selected = true;
                menu.FinishSelection();
            }
        }
    }

    public override float GetY()
    {
        //TODO: return box.y;
        return 0;
    }
    protected override void SetY(float screenY)
    {
        //TODO: box.y = screenY;
    }
    public override void SetAlpha(float alpha)
    {
        box.alpha = alpha;
    }
    public void SetText(string text)
    {
        box.text = text;
    }
}
