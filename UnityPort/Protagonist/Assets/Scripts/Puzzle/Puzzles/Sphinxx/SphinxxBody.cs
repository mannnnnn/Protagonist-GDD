using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxxBody : MonoBehaviour
{
    public CycleScaler cycle;
    float timerSeconds = 0f;
    float initialY;
    
	void Start ()
    {
        initialY = transform.localPosition.y;
        SetY(cycle.Get(timerSeconds));
    }
	void Update ()
    {
        timerSeconds += GameTime.deltaTime;
        SetY(cycle.Get(timerSeconds));
	}

    private void SetY(float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, initialY + y, transform.localPosition.z);
    }
}