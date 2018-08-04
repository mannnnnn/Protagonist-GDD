using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimationSwing : TimedDialogAnimation
{
    SpriteRenderer front;
    SpriteRenderer back;
    float startAlpha = 0f;
    float endAlpha = 0f;

    public void Initialize(float alpha) {
        endAlpha = alpha;
    }

    void Start ()
    {
        base.Initialize(1.5f);
        var swing = GetComponent<DialogSwingable>();
        if (swing == null)
        {
            throw new Exception("GameObject " + gameObject.name + " does not have a DialogSwingable component.");
        }
        // set back sprite
        back = GetComponent<SpriteRenderer>();
        back.sprite = swing.back;
        startAlpha = front.color.a;
        // create a child component that draws the front sprite
        GameObject frontObj = new GameObject();
        frontObj.transform.parent = transform;
        front = frontObj.AddComponent<SpriteRenderer>();
        front.sprite = swing.front;
    }
	
	protected override void Update ()
    {
        base.Update();
        front.color = new Color(front.color.r, front.color.g, front.color.b, Mathf.Lerp(startAlpha, endAlpha, timer));
        back.color = new Color(back.color.r, back.color.g, back.color.b, Mathf.Lerp(1 - startAlpha, 1 - endAlpha, timer));
    }

    // clean up front-drawing child
    protected override void Finish()
    {
        Destroy(front.gameObject);
    }
}
