using UnityEngine;
using UnityEngine.UI;

/**
 * A Panel is a UI gameObject that can have a frame, fill, and optionally text.
 * Allows setting a panel's position, text, size, and alpha into one component.
 * Then, this behavior wraps those into one method call.
 */
public class UIPanel : MonoBehaviour
{
    InputField inputbox;
    Text textbox;
    Image frame;
    Image fill;
    RectTransform rectTransform;

    void Awake()
    {
        inputbox = gameObject.GetComponentInChildren<InputField>();
        textbox = gameObject.GetComponentInChildren<Text>();
        frame = transform.Find("Frame")?.gameObject?.GetComponent<Image>();
        fill = transform.Find("Fill")?.gameObject?.GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    float size = 100f;
    void Update()
    {
        rect = new Rect(Input.mousePosition, Vector2.one * size);
        size += Input.GetAxis("Mouse ScrollWheel") * 100f;
    }

    public Rect rect
    {
        get
        {
            return ScreenResolution.GetScreenRect(rectTransform);
        }
        set
        {
            SetScreenRect(value);
        }
    }

    public string text
    {
        get { return inputbox.text; }
        set { inputbox.text = value; }
    }

    public float alpha
    {
        get
        {
            if (textbox != null)
                return textbox.color.a;
            if (frame != null)
                return frame.color.a;
            if (fill != null)
                return fill.color.a;
            return 0f;
        }
        set
        {
            if (textbox != null)
                textbox.color = new Color(textbox.color.r, textbox.color.g, textbox.color.b, value);
            if (frame != null)
                frame.color = new Color(frame.color.r, frame.color.g, frame.color.b, value);
            if (fill != null)
                fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, value);
        }
    }

    Rect ToRect(Vector2 pos, Vector2 size, Vector2 pivot)
    {
        return new Rect(pos - LerpRect(new Rect(Vector2.zero, size), pivot), size);
    }
    void SetScreenRect(Rect screenRect)
    {
        Rect parent = ScreenResolution.GetScreenRect(transform.parent.GetComponent<RectTransform>());
        screenRect = ClampRect(screenRect, parent);
        Vector2 anchorPosMin = LerpRect(parent, rectTransform.anchorMin);
        Vector2 anchorPosMax = LerpRect(parent, rectTransform.anchorMax);
        Vector2 min = screenRect.position - anchorPosMin;
        Vector2 max = (screenRect.position + screenRect.size) - anchorPosMax;
        rectTransform.offsetMin = min;
        rectTransform.offsetMax = max;
    }
    Vector2 LerpRect(Rect rect, Vector2 t)
    {
        return new Vector2(Mathf.Lerp(rect.position.x, rect.position.x + rect.size.x, t.x),
            Mathf.Lerp(rect.position.y, rect.position.y + rect.size.y, t.y));
    }
    Rect ClampRect(Rect current, Rect parent)
    {
        Vector2 min = parent.position;
        Vector2 max = parent.position + parent.size - current.size;
        Vector2 clampedPos = new Vector2(Mathf.Clamp(current.x, min.x, max.x),
            Mathf.Clamp(current.y, min.y, max.y));
        return new Rect(clampedPos, current.size);
    }
}
