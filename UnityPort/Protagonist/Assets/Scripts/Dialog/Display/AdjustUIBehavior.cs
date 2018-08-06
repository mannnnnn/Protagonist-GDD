using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component is on the base level UI elements in the dialogue UI.
 * You give it a left and right values on [0, 1], and it will set the width and x position accordingly to fit.
 */
public class AdjustUIBehavior : MonoBehaviour
{
    public float left = 0f;
    public float right = 1f;

    RectTransform rect;

    // set left and right to resolution handler values
    void Start ()
    {
        rect = gameObject.GetComponent<RectTransform>();
        UpdateAnchors();
    }

    // resets anchor position to those of the left/right fields
    public void UpdateAnchors()
    {
        UpdateAnchors(left, right);
    }
    // temporarily sets anchor position to the given left/right, does not set the object's left/right fields
    public void UpdateAnchors(float left, float right)
    {
        // set anchor to resolution size
        if (rect != null)
        {
            rect.anchorMin = ResolutionHandler.GetInstance().MapViewToScreenPoint(new Vector2(left, 0)) / new Vector2(Screen.width, Screen.height);
            rect.anchorMax = ResolutionHandler.GetInstance().MapViewToScreenPoint(new Vector2(right, 0)) / new Vector2(Screen.width, Screen.height);
        }
    }
}
