using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimationFade : TimedDialogAnimation
{
    float startAlpha = 0f;
    float endAlpha = 0f;
    SpriteRenderer sr;

    public void Initialize(float alpha)
    {
        endAlpha = alpha;
    }

    void Start ()
    {
        // 1 second duration
        base.Initialize(1f);
        // get starting opacity
        sr = GetComponent<SpriteRenderer>();
        startAlpha = sr.color.a;
    }
	
	protected override void Update ()
    {
        base.Update();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(startAlpha, endAlpha, timer));
    }
}
