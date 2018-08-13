using Assets.Scripts.Libraries.ProtagonistDialog;
using Assets.Scripts.UI.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayBehavior : UIDisplayBase
{
    Inventory inventory;

    UIPanel backPanel;
    UIPanel infoPanel;
    UIPanel chestBox;

    float centerScreenY;
    float hiddenScreenY;

    public bool Open => state != State.CLOSED;

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
            inventory.AddItem("default");
        }
        // tell dialog whether inv is full or not
        Dialog.flags["inventoryFull"] = inventory.IsFull;
    }

    public float GetSize()
    {
        return backPanel.GetSize();
    }

    public override void SetAlpha(float alpha)
    {
        backPanel.SetAlpha(alpha);
        infoPanel.SetAlpha(alpha);
        SetItemsAlpha(alpha);
    }

    protected override void OpenStart()
    {
        SetItemsDynamic(true);
    }
    protected override void CloseFinish()
    {
        SetItemsDynamic(false);
    }

    private IEnumerable<InventoryItemBehavior> GetItemBehaviors()
    {
        foreach (ItemType type in inventory.items.Keys)
        {
            foreach (Item item in inventory.items[type])
            {
                InventoryItemBehavior itemBehavior = item.gameObject?.GetComponent<InventoryItemBehavior>();
                if (itemBehavior != null)
                {
                    yield return itemBehavior;
                }
            }
        }
    }
    private void SetItemsAlpha(float alpha)
    {
        foreach (InventoryItemBehavior item in GetItemBehaviors())
        {
            item.SetAlpha(alpha);
        }
    }
    private void SetItemsDynamic(bool value)
    {
        foreach (InventoryItemBehavior item in GetItemBehaviors())
        {
            item.SetDynamic(value);
        }
    }
    // only affects the display, not the actual inventory data.
    // For that, see the Inventory class.
    public void AddItem(Item item)
    {
        GameObject gameObj = Instantiate(Inventory.Prefabs[item.type.img], chestBox.rect);
        gameObj.GetComponent<InventoryItemBehavior>().Initialize(item, Open);
        Vector3 objSize = gameObj.GetComponent<Collider2D>().bounds.extents * 2f;
        // get screen rectangle of the chest box, pick some x position along the top part
        Rect chest = ResolutionHandler.GetScreenRect(chestBox.rect);
        chest.position = new Vector2(chest.position.x, chest.position.y + chest.size.y * 0.5f);
        chest.size = new Vector2(chest.size.x, chest.size.y * 0.5f);
        chest.min += (Vector2)objSize;
        chest.max -= (Vector2)objSize;
        Vector2 pos = new Vector2(Random.Range(chest.xMin, chest.xMax), Random.Range(chest.yMin, chest.yMax));
        // move item obj to position
        gameObj.transform.localPosition = ResolutionHandler.ScreenToRectPoint(chestBox.rect, pos);
        gameObj.transform.localRotation = Quaternion.identity;
        item.gameObject = gameObj;
    }
    public void ClearItems()
    {
        foreach (InventoryItemBehavior item in GetItemBehaviors())
        {
            Destroy(item.gameObject);
        }
    }
}
