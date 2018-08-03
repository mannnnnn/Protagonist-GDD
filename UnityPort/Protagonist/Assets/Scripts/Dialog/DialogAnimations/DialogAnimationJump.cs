using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimationJump : TimedDialogAnimation
{
    SpriteRenderer sr;

    void Start () {
        // instant duration
        Initialize(0f);
        // get starting opacity
        sr = GetComponent<SpriteRenderer>();
    }
	
	protected override void Update () {
        base.Update();
        // instantly cut out
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
    }
}
