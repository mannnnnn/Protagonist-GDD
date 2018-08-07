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
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        image.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    public float GetY()
    {
        return rect.position.y;
    }
    public void SetY(float height)
    {
        rect.position = new Vector3(rect.position.x, height, rect.position.z);
    }
    public float GetScreenY()
    {
        return ResolutionHandler.RectToScreenPoint(rect.position).y;
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
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, size);
    }
}
