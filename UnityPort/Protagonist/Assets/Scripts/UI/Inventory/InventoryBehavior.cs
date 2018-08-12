using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : UIDisplayBase
{
    UIPanel backPanel;
    UIPanel infoPanel;
    UIPanel chestBox;

    float centerScreenY;
    float hiddenScreenY;

    public GameObject testItem;

    // Use this for initialization
    void Start () {
        backPanel = transform.Find("BackPanel").gameObject.GetComponent<UIPanel>();
        infoPanel = transform.Find("InfoPanel").gameObject.GetComponent<UIPanel>();
        chestBox = transform.Find("ChestBox").gameObject.GetComponent<UIPanel>();
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
            if (state == State.CLOSED)
            {
                SetState(State.OPENING);
            }
            if (state == State.OPEN)
            {
                SetState(State.CLOSING);
            }
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
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            GameObject item = Instantiate(testItem, chestBox.rect);
            item.transform.localPosition = ResolutionHandler.ScreenToRectPoint(chestBox.rect, Input.mousePosition);
            item.transform.localRotation = Quaternion.identity;
        }
    }

    public float GetSize()
    {
        return backPanel.GetSize();
    }

    public override void SetAlpha(float alpha)
    {
        backPanel.SetAlpha(alpha);
        infoPanel.SetAlpha(alpha);
    }
}
