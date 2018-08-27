using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxxTail : MonoBehaviour
{
    public CycleScaler cycle;
    float timerSeconds = 0f;
    
	void Start ()
    {
        SetAngle(cycle.Get(timerSeconds));
    }
	void Update ()
    {
        timerSeconds += GameTime.deltaTime;
        SetAngle(cycle.Get(timerSeconds));
	}

    private void SetAngle(float angle)
    {
        transform.localEulerAngles = new Vector3(0f, 0f, angle);
    }
}

/**
 * Cycle from value min to value max using sin(x) scaling
 */
[Serializable]
public class CycleScaler
{
    public float frequency = 1f;
    public float min = 0f;
    public float max = 1f;

    public CycleScaler() { }
    public CycleScaler(float frequency, float min, float max)
    {
        this.frequency = frequency;
        this.min = min;
        this.max = max;
    }

    public float Get(float t)
    {
        float scaledCycle = Mathf.Sin(t * frequency * 2 * Mathf.PI);
        return Mathf.Lerp(min, max, Mathf.InverseLerp(-1, 1, scaledCycle));
    }
}