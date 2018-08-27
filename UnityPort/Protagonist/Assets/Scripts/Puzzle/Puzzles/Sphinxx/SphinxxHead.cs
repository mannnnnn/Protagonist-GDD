using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxxHead : MonoBehaviour
{
    // head rumbles around by choosing a new target position in a circle every so often
    // and then moving to that position
    public float spd = 10f;
    float timerSeconds = 0f;
    public float duration;
    public float radius = 0.5f;
    Vector2 targetPosition;
    Vector2 initialPos;

	void Start ()
    {
        initialPos = transform.localPosition;
        targetPosition = Vector2.zero;
    }
	void Update ()
    {
        timerSeconds += GameTime.deltaTime;
        if (timerSeconds >= duration)
        {
            timerSeconds = 0f;
            targetPosition = Random.insideUnitCircle * radius;
        }
        SetPosition(Vector2.MoveTowards(Vector2.zero, targetPosition, spd * GameTime.deltaTime));
	}

    private void SetPosition(Vector2 pos)
    {
        transform.localPosition = new Vector3(initialPos.x + pos.x, initialPos.y + pos.y, transform.localPosition.z);
    }
}