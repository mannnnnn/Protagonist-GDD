using Assets.Scripts.Libraries.ProtagonistDialog;
using Assets.Scripts.UI.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayBehavior : UIDisplayBase
{
    Inventory inventory;

    UIPanel backPanel;
    UIPanel chestPanel;
    UIPanel chestBox;
    UIPanel infoPanel;
    UIPanel imagePanel;
    UIPanel textPanel;
    SpriteRenderer image;
    Text itemName;
    Text itemDesc;
    UIPanel eatButton;
    UIPanel discardButton;

    InventoryItemBehavior selectedItem;

    float centerScreenY;
    float hiddenScreenY;

    public bool Open => state != State.CLOSED;

    // Use this for initialization
    void Start ()
    {
        inventory = GetComponent<Inventory>();
        // get panels for display control
        backPanel = transform.Find("BackPanel").GetComponent<UIPanel>();
        infoPanel = transform.Find("InfoPanel").GetComponent<UIPanel>();
        chestBox = transform.Find("ChestBox").GetComponent<UIPanel>();
        chestPanel = transform.Find("ChestPanel").GetComponent<UIPanel>();
        imagePanel = infoPanel.transform.Find("ImagePanel").GetComponent<UIPanel>();
        image = imagePanel.transform.Find("ImageContainer").Find("Image").GetComponent<SpriteRenderer>();
        textPanel = infoPanel.transform.Find("TextPanel").GetComponent<UIPanel>();
        itemName = textPanel.transform.Find("Name").GetComponent<Text>();
        itemDesc = textPanel.transform.Find("Description").GetComponent<Text>();
        eatButton = textPanel.transform.Find("EatButton").GetComponent<UIPanel>();
        discardButton = textPanel.transform.Find("DiscardButton").GetComponent<UIPanel>();
        // move up to middle of screen
        centerScreenY = Screen.height - (Screen.height - GetSize()) * 0.5f;
        hiddenScreenY = centerScreenY - 250f;
        SetY(hiddenScreenY);
        SetTargetY(hiddenScreenY);
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        // open/close
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
            int rnd = Random.Range(0, inventory.itemTypes.Count);
            ItemType item = inventory.itemTypes.ToList()[rnd].Value;
            inventory.AddItem(item);
        }
        // tell dialog whether inv is full or not
        Dialog.flags["inventoryFull"] = inventory.IsFull;
        // interact with items. Select/hover
        ItemInteraction();
        if (selectedItem != null && Input.GetMouseButtonDown(0))
        {
            if (ResolutionHandler.GetScreenRect(eatButton.rect).Contains(Input.mousePosition))
            {
                EatItem(selectedItem);
            }
            if (ResolutionHandler.GetScreenRect(discardButton.rect).Contains(Input.mousePosition))
            {
                DiscardItem(selectedItem);
            }
        }
    }

    // player interacting with items, such as hover over an item or select it
    private void ItemInteraction()
    {
        foreach (InventoryItemBehavior item in GetItemBehaviors())
        {
            item.SetHover(false);
        }
        // find hovered item
        InventoryItemBehavior hoverItem = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("UI"));
        foreach (RaycastHit2D hit in hits)
        {
            InventoryItemBehavior item = hit.collider?.gameObject?.GetComponent<InventoryItemBehavior>();
            if (item != null)
            {
                hoverItem = item;
            }
        }
        // set item has hovered
        if (hoverItem != null)
        {
            hoverItem.SetHover(true);
        }
        // select if clicked on
        if (Input.GetMouseButtonDown(0) && ResolutionHandler.GetScreenRect(chestBox.rect).Contains(Input.mousePosition))
        {
            foreach (InventoryItemBehavior item in GetItemBehaviors())
            {
                item.SetSelected(false);
            }
            selectedItem = hoverItem;
            DisplayItem(selectedItem);
            if (hoverItem != null)
            {
                hoverItem.SetSelected(true);
            }
        }
    }

    private void EatItem(InventoryItemBehavior item)
    {
        if (item.type.edible)
        {
            image.sprite = null;
            eatButton.SetAlpha(0);
            discardButton.SetAlpha(0);
            inventory.RemoveItem(item.item);
        }
        itemDesc.text = item.type.eatText;
    }
    private void DiscardItem(InventoryItemBehavior item)
    {
        selectedItem = null;
        DisplayItem(null);
        inventory.RemoveItem(item.item);
    }

    // set item display info
    private void DisplayItem(InventoryItemBehavior item)
    {
        if (item != null)
        {
            image.sprite = item.sr.sprite;
            itemName.text = item.type.name;
            itemDesc.text = item.type.text;
            eatButton.SetAlpha(1);
            discardButton.SetAlpha(1);
        }
        else
        {
            image.sprite = null;
            itemName.text = "";
            itemDesc.text = "";
            eatButton.SetAlpha(0);
            discardButton.SetAlpha(0);
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
        chestPanel.SetAlpha(alpha);
        imagePanel.SetAlpha(alpha);
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        textPanel.SetAlpha(alpha);
        itemName.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        itemDesc.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
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
