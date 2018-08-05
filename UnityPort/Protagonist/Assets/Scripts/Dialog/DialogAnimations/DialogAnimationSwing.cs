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
    float startAngle = 0f;
    float endAngle = 0f;

    public void Initialize(float alpha, float endAngle) {
        this.endAngle = endAngle;
        endAlpha = alpha;
    }

    void Start ()
    {
        base.Initialize(0.5f);
        var swing = GetComponent<DialogSwingable>();
        if (swing == null)
        {
            throw new Exception("GameObject " + gameObject.name + " does not have a DialogSwingable component.");
        }
        // set back sprite
        front = GetComponent<SpriteRenderer>();
        front.sprite = swing.front;
        startAlpha = front.color.a;
        startAngle = front.transform.localEulerAngles.z;
        // create a child component that draws the front sprite
        GameObject backObj = new GameObject(gameObject.name + " Swing Back Sprite");
        backObj.transform.parent = transform;
        // when you apply parent, it transforms itself to preserve its absolute transform. So undo that.
        backObj.transform.localPosition = new Vector3(0, 0, -1);
        backObj.transform.localScale = new Vector3(1, 1, backObj.transform.localScale.z);
        backObj.transform.localEulerAngles = new Vector3(0, 0, 0);
        back = backObj.AddComponent<SpriteRenderer>();
        back.sprite = swing.back;
    }
	
	protected override void Update ()
    {
        base.Update();
        front.color = new Color(front.color.r, front.color.g, front.color.b, Mathf.Lerp(startAlpha, endAlpha, timer));
        front.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(startAngle, endAngle, timer));
        if (back != null)
        {
            back.color = new Color(back.color.r, back.color.g, back.color.b, Mathf.Lerp(1 - startAlpha, 1 - endAlpha, timer));
        }
    }

    // clean up front-drawing child
    protected override void Finish()
    {
        Destroy(back.gameObject);
    }
}
