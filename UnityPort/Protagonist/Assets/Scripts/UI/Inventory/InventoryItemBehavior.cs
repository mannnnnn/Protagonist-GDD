using Assets.Scripts.UI.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBehavior : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D body;
    ItemType type;
    // grayed out when not selected
    float notSelected = 0.8f;
    float selected = 1f;

    // turn kinematic after a while to settle position, instead of right away
    bool turnKinematic = false;
    float duration = 1f;
    float timerSeconds = 0f;

    public void Initialize(Item item, bool active)
    {
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
            timerSeconds += Time.deltaTime;
            if (timerSeconds >= duration)
            {
                SetDynamic(false);
                turnKinematic = false;
            }
        }
    }

    public void SetAlpha(float alpha)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
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
}
