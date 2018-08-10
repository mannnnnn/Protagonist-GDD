using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// helper class used to wrap setting alpha/y/text/size into one object per GameObject.
// Then, this behavior wraps it into one method call.
public class DialogTextbox
{
    public GameObject gameObj;
    InputField textbox;
    Text text;
    Image image;
    public RectTransform rect;

    public DialogTextbox(GameObject g)
    {
        gameObj = g;
        textbox = gameObj.GetComponentInChildren<InputField>();
        text = gameObj.GetComponentInChildren<Text>();
        image = gameObj.GetComponentInChildren<Image>();
        rect = gameObj.GetComponent<RectTransform>();
    }

    public string GetText()
    {
        return textbox.text;
    }
    public void SetText(string message)
    {
        textbox.text = message;
    }

    public void SetAlpha(float alpha)
    {
        if (text != null)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }
        if (image != null)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
    }

    public float GetLocalY()
    {
        return rect.anchoredPosition.y;
    }
    public void SetLocalY(float height)
    {
        rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, height, rect.position.z);
    }
    public float GetScreenY()
    {
        return ResolutionHandler.RectToScreenPoint(rect, new Vector2(0, 0)).y;
    }
    public void SetScreenY(float screenY)
    {
        SetLocalY(rect.anchoredPosition.y + ResolutionHandler.ScreenToRectPoint(rect, new Vector2(0, screenY)).y);
    }

    public float GetSize()
    {
        return rect.sizeDelta.y;
    }
    public void SetSize(float size)
    {
        var resize = size - GetSize();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, size);
        SetScreenY(GetScreenY() + resize * 0.5f);
    }
}
