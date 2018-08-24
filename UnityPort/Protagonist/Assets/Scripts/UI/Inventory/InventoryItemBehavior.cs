using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Inventory items are the ragdoll items that appear in the inventory menu's chest.
 * They follow physics, and can be clicked on to display their information.
 * They can be eaten/discarded (destroyed).
 */
public class InventoryItemBehavior : MonoBehaviour
{
    public SpriteRenderer sr { get; private set; }
    Rigidbody2D body;
    public Item item { get; private set; }
    public ItemType type { get; private set; }
    // grayed out when not selected
    bool selected = false;
    bool hover;
    float notSelectedGray = 0.75f;
    float hoverGray = 1f;
    float selectedGray = 1f;

    // turn kinematic after a while to settle position, instead of right away
    bool turnKinematic = false;
    float duration = 1f;
    float timerSeconds = 0f;

    public void Initialize(Item item, bool active)
    {
        this.item = item;
        type = item.type;
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        if (!active)
        {
            SetAlpha(0);
            turnKinematic = true;
        }
    }

    void Start()
    {
	}

	void Update()
    {
        if (turnKinematic)
        {
            timerSeconds += UITime.deltaTime;
            if (timerSeconds >= duration)
            {
                SetDynamic(false);
                turnKinematic = false;
            }
        }
        // gray if not selected
        float shade = Mathf.MoveTowards(sr.color.r, selected ? selectedGray : notSelectedGray, 1 * UITime.deltaTime);
        sr.color = new Color(shade, shade, shade, sr.color.a);
        // if hovered over, light up a bit
        if (sr.color.r < hoverGray && hover)
        {
            sr.color = new Color(hoverGray, hoverGray, hoverGray, sr.color.a);
        }
    }

    public void SetAlpha(float alpha)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }

    public void SetSelected(bool selected)
    {
        this.selected = selected;
    }
    public void SetHover(bool hover)
    {
        this.hover = hover;
    }

    public void SetDynamic(bool value)
    {
        // if turning kinematic but we want to be dynamic, cancel the change to kinematic
        if (turnKinematic && value)
        {
            turnKinematic = false;
        }
        body.isKinematic = !value;
    }

    // override this if you want to do something when eaten
    public virtual void Eat() { }
}
