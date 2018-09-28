using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UIScalable
{
    float Scale { get; }
}

/**
 * Scales gameObject to fit in the ScreenResolution black bars.
 * UI components should expand to fill the space set by this by getting the Scale
 */
public class UIScaler : MonoBehaviour
{
    RectTransform rect;

    Vector2 baseScale = new Vector2(640, 480);
    public float Scale => rect.rect.width / baseScale.x;

	void Start()
    {
        rect = GetComponent<RectTransform>();
        Vector2 min = ScreenResolution.MapViewToScreenPoint(Vector2.zero);
        Vector2 max = ScreenResolution.MapViewToScreenPoint(Vector2.one);
        rect.offsetMin = new Vector2(min.x, min.y);
        rect.offsetMax = new Vector2(max.x - Screen.width, max.y - Screen.height);
    }
}
