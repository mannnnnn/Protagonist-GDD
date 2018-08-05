using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimationJump : TimedDialogAnimation
{
    SpriteRenderer sr;
    float endAlpha = 0f;

    public void Initialize(float alpha)
    {
        endAlpha = alpha;
    }

    void Start ()
    {
        // instant duration
        base.Initialize(0f);
        sr = GetComponent<SpriteRenderer>();
    }
	
	protected override void Update ()
    {
        base.Update();
        // instantly cut out to target alpha
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, endAlpha);
    }
}
