using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : DialogDisplayBase
{
    UIPanel backPanel;

    float centerScreenY;
    float hiddenScreenY;

    // Use this for initialization
    void Start () {
        backPanel = transform.Find("BackPanel").gameObject.GetComponent<UIPanel>();
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

    public float GetSize()
    {
        return backPanel.GetSize();
    }

    public override void SetAlpha(float alpha)
    {
        backPanel.SetAlpha(alpha);
    }
}
