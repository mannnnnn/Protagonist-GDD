using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Simple dialog display that only handles the scrolling text of the intro.
 */
public class IntroDialogDisplay : MonoBehaviour, DialogDisplay
{
    TextScroller textScroller = new TextScroller();
    HighlightedText text;

    public bool Active { get; private set; }

    protected void Start()
    {
        // get components
        text = GetComponent<HighlightedText>();
    }

    void Update()
    {
        textScroller.Advance(UITime.deltaTime);
        string message = "";
        if (Active)
        {
            message = textScroller.Text();
        }
        text.SetText(message);
    }

    // call to set the text contents of the display, both nameplate and textbox. Called by Dialog
    public void SetText(List<string> characters, string text)
    {
        textScroller.SetNew(text);
    }

    // dialog menu creation, called by Dialog
    public List<Dictionary<string, object>> SetMenu(List<Dictionary<string, object>> options, string type)
    {
        throw new InvalidOperationException("The intro dialog display can't have menus.");
    }

    // text scroll methods
    public bool TextFinished()
    {
        return textScroller.Finished();
    }
    public void Advance(float amount)
    {
        textScroller.Advance(amount);
    }

    // can never hide or be deactivated currently
    public void Show() { Active = true; }
    public void Hide() { Active = false; }
    public void Stop() { Active = false; }
}