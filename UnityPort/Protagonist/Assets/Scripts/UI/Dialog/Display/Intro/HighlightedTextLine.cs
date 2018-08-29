using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightedTextLine : MonoBehaviour
{
    Image image;
    RectTransform imageRect;
    Text text;
    RectTransform textRect;

    Font font;
    int fontSize;

    public void Initialize(Font font, int fontSize)
    {
        this.font = font;
        this.fontSize = fontSize;
        text.font = font;
        text.fontSize = fontSize;
        SetText("");
    }

    void Awake()
    {
        image = GetComponentInChildren<Image>();
        imageRect = image.GetComponent<RectTransform>();

        text = GetComponentInChildren<Text>();
        textRect = text.GetComponent<RectTransform>();
    }

    public void SetText(string s)
    {
        Vector2 padding = 5f * Vector2.one + (3f * Vector2.right);
        textRect.sizeDelta = new Vector2(Utilities.GetStringWidth(s, font, fontSize), Utilities.GetStringHeight(font, fontSize)) + padding;
        text.text = s;
        imageRect.sizeDelta = textRect.sizeDelta;
    }

    public float GetSize()
    {
        return imageRect.sizeDelta.y;
    }
} 
