using Assets.Scripts.UI.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : UIDisplayBase
{
    Inventory inventory;

    UIPanel backPanel;
    UIPanel infoPanel;
    UIPanel chestBox;

    float centerScreenY;
    float hiddenScreenY;

    // Use this for initialization
    void Start ()
    {
        inventory = GetComponent<Inventory>();
        // get panel for display control
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
            GameObject item = Instantiate(inventory.prefabs[Random.Range(0, inventory.prefabs.Count)].prefab, chestBox.rect);
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
