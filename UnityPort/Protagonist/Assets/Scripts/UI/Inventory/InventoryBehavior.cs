using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : DialogDisplayBase
{
    RectTransform rect;

    DialogTextbox backPanel;

    float centerScreenY;
    float hiddenScreenY;

    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
        backPanel = new DialogTextbox(transform.Find("BackPanel").gameObject);
        // move up to middle of screen
        centerScreenY = Screen.height - (Screen.height - GetSize()) * 0.5f;
        hiddenScreenY = centerScreenY - 250f;
        SetY(hiddenScreenY);
        SetTargetY(hiddenScreenY);
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        // handle click on this button
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetState(State.OPENING);
        }
        // set target position
        switch (state)
        {
            case State.OPENING:
            case State.PENDING_CLOSE:
                SetTargetY(centerScreenY);
                break;
            case State.CLOSING:
            case State.CLOSED:
                SetTargetY(hiddenScreenY);
                break;
        }
    }

    public override float GetY()
    {
        return ResolutionHandler.RectToScreenPoint(rect, new Vector2(0, 0)).y;
    }
    protected override void SetY(float screenY)
    {
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x,
            rect.anchoredPosition.y + ResolutionHandler.ScreenToRectPoint(rect, new Vector2(0, screenY)).y);
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
