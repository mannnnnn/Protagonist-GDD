using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Panel that contains the text display of the Inventory Item's information,
 * along with the eat/discard buttons.
 * Handles display a given item's data, as well as the eat/discard button presses.
 */
public class InventoryInfoBehavior : UIPanel
{
    InventoryDisplayBehavior display;
    Inventory inventory;

    UIPanel imagePanel;
    UIPanel textPanel;
    SpriteRenderer image;
    Text itemName;
    Text itemDesc;
    UIPanel eatButton;
    UIPanel discardButton;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        inventory = transform.parent.GetComponent<Inventory>();
        display = transform.parent.GetComponent<InventoryDisplayBehavior>();
        // item image
        imagePanel = transform.Find("ImagePanel").GetComponent<UIPanel>();
        image = imagePanel.transform.Find("ImageContainer").Find("Image").GetComponent<SpriteRenderer>();
        // name/desc text
        textPanel = transform.Find("TextPanel").GetComponent<UIPanel>();
        itemName = textPanel.transform.Find("Name").GetComponent<Text>();
        itemDesc = textPanel.transform.Find("Description").GetComponent<Text>();
        // buttons
        eatButton = textPanel.transform.Find("EatButton").GetComponent<UIPanel>();
        discardButton = textPanel.transform.Find("DiscardButton").GetComponent<UIPanel>();
    }

    void Update()
    {
        // handle button clicks
        if (display.selectedItem != null && Input.GetMouseButtonDown(0))
        {
            if (ResolutionHandler.GetScreenRect(eatButton.rect).Contains(Input.mousePosition))
            {
                EatItem(display.selectedItem);
            }
            if (ResolutionHandler.GetScreenRect(discardButton.rect).Contains(Input.mousePosition))
            {
                DiscardItem(display.selectedItem);
            }
        }
    }

    // set info display attributes
    public void SetEatButton(bool visible)
    {
        eatButton.SetAlpha(visible ? 1 : 0);
    }
    public void SetDiscardButton(bool visible)
    {
        discardButton.SetAlpha(visible ? 1 : 0);
    }
    public void SetImage(Sprite s)
    {
        image.sprite = s;
    }
    public void SetName(string s)
    {
        itemName.text = s;
    }
    public void SetDesc(string s)
    {
        itemDesc.text = s;
    }
    public override void SetAlpha(float alpha)
    {
        base.SetAlpha(alpha);
        imagePanel.SetAlpha(alpha);
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        textPanel.SetAlpha(alpha);
        itemName.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        itemDesc.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    // button click actions
    private void EatItem(InventoryItemBehavior item)
    {
        item.Eat();
        if (item.type.edible)
        {
            SetImage(null);
            SetEatButton(false);
            SetDiscardButton(false);
            inventory.RemoveItem(item.item);
        }
        SetDesc(item.type.eatText);
    }
    private void DiscardItem(InventoryItemBehavior item)
    {
        if (!item.type.edible)
        {
            return;
        }
        display.selectedItem = null;
        DisplayItem(null);
        inventory.RemoveItem(item.item);
    }

    // set item display info
    public void DisplayItem(InventoryItemBehavior item)
    {
        if (item != null)
        {
            SetImage(item.sr.sprite);
            SetName(item.type.name);
            SetDesc(item.type.text);
            SetEatButton(true);
            SetDiscardButton(true);
            SetDiscardButton(item.type.edible);
        }
        else
        {
            SetImage(null);
            SetName("");
            SetDesc("");
            SetEatButton(false);
            SetDiscardButton(false);
        }
    }
}
