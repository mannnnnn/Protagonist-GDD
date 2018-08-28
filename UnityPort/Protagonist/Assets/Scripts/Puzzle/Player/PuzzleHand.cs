using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controls the player hands that wiggle up and down during puzzles.
 */
public class PuzzleHand : MonoBehaviour
{
    float verticalMin = -0.5f;
    float verticalMax = 0.3f;
    float verticalSpd = 3f;
    float angleSpd = 3f;

    public float xPosition = 0.12f;
    public float offset = -45f;

    void Start()
    {
        transform.position = ScreenResolution.MapViewToWorldPoint(new Vector2(xPosition, 0f)) + Vector3.forward * transform.position.z;
    }

    void Update()
    {
        Vector2 cursor = PuzzleCursor.GetPosition();
        Vector2 min = ScreenResolution.MapViewToWorldPoint(Vector2.zero);
        Vector2 max = ScreenResolution.MapViewToWorldPoint(Vector2.one);
        cursor = new Vector2(Mathf.Clamp(cursor.x, min.x, max.x), Mathf.Clamp(cursor.y, min.y, max.y));
        // height control
        float yTarget = Utilities.FreeLerp(cursor.y, min.y, max.y, min.y + verticalMin, min.y + verticalMax);
        float yDist = yTarget - transform.position.y;
        float y = Mathf.MoveTowards(transform.position.y, yTarget, Mathf.Abs(yDist) * verticalSpd * GameTime.deltaTime);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        // angle control
        Vector2 direction = cursor - (Vector2)transform.position;
        float angleTarget = Vector2.SignedAngle(Vector2.right, direction.normalized);
        float angleError = Mathf.Abs(Mathf.DeltaAngle(angleTarget, transform.localEulerAngles.z - offset));
        float angle = Mathf.MoveTowardsAngle(transform.localEulerAngles.z - offset, angleTarget, angleError * angleSpd * GameTime.deltaTime);
        if (xPosition < 0.5f)
        {
            angle = ClampAngle(angle, 0f, 67.5f);
        }
        else
        {
            angle = ClampAngle(angle, 112.5f, 180f);
        }
        transform.localEulerAngles = new Vector3(0f, 0f, angle + offset);
    }

    // copy-pasted ClampAngle method, not sure how it works
    private static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        bool inverse = false;
        var tmin = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = !inverse;
            tmin -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        var result = !inverse ? tangle > tmin : tangle < tmin;
        if (!result)
        {
            angle = min;
        }

        inverse = false;
        tangle = angle;
        var tmax = max;
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tmax -= 180;
        }

        result = !inverse ? tangle < tmax : tangle > tmax;
        if (!result)
        {
            angle = max;
        }
        return angle;
    }
}
