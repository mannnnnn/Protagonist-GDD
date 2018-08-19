using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Simple behavior that controls the walls of the inventory UI chest.
 * Places them in the correct positions so that no items can fall out.
 */
public class ChestBoxBehavior : MonoBehaviour {

    public Vector2 side;
    float width = 40f;
    RectTransform parent;

	// Use this for initialization
	void Start() {
        parent = (RectTransform)transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
        Resize();
    }

    private void Resize()
    {
        // go to side specified and scale
        Vector2 size = Vector2.zero;
        if (Mathf.Abs(side.x) > 0)
        {
            size = new Vector2(width, Mathf.Abs(parent.rect.size.y) + (2 * width));
        }
        if (Mathf.Abs(side.y) > 0)
        {
            size = new Vector2(Mathf.Abs(parent.rect.size.x) + (2 * width), width);
        }
        Vector2 pos = (parent.rect.size * 0.5f) + (side * ((size * 0.5f) + (parent.rect.size * 0.5f))) - (parent.rect.size * parent.pivot);
        transform.localScale = size;
        transform.localPosition = pos;
    }
}
