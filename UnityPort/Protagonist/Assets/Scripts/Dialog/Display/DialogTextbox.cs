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

    public float GetY()
    {
        return rect.localPosition.y;
    }
    public void SetY(float height)
    {
        rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, height, rect.position.z);
    }
    public float GetScreenY()
    {
        return ResolutionHandler.RectToScreenPoint(rect.anchoredPosition).y;
    }
    public void SetScreenY(float screenHeight)
    {
        SetY(ResolutionHandler.ScreenToRectPoint(new Vector2(0, screenHeight)).y);
    }

    public float GetSize()
    {
        return rect.rect.height;
    }
    public void SetSize(float size)
    {
        var resize = size - GetSize();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, size);
        SetScreenY(GetScreenY() + resize * 0.5f);
    }

    public Rect GetScreenRect()
    {
        Vector3[] v = new Vector3[4];
        rect.GetWorldCorners(v);
        var bottomLeft = ResolutionHandler.RectToScreenPoint(v[0]);
        var topRight = ResolutionHandler.RectToScreenPoint(v[2]);
        return new Rect(bottomLeft, topRight - bottomLeft);
    }
}
