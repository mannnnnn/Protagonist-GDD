using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxxLeg : MonoBehaviour
{
    public CycleScaler cycle;
    float timerSeconds = 0f;
    float initialYScale;
    
	void Start ()
    {
        initialYScale = transform.localScale.y;
        SetYScale(cycle.Get(timerSeconds));
    }
	void Update ()
    {
        timerSeconds += GameTime.deltaTime;
        SetYScale(cycle.Get(timerSeconds));
	}

    private void SetYScale(float yScale)
    {
        transform.localScale = new Vector3(transform.localScale.x, initialYScale * yScale, transform.localScale.z);
    }
}