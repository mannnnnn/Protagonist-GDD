using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface TextScrollTarget
{
    void FinishTextScroll();
}

/**
 * You give this some text, and you manually advance it.
 * It generates scrolling text.
 * Useful for implementing DialogDisplay.
 */
public class TextScroller
{
    string current = "";
    // timing
    string textFull = "";
    int textPosition = 0;
    float textTimer = 0f;
    // pause for different times at , and .
    float textPauseDefault = 0.03f;
    Dictionary<char, float> textPause = new Dictionary<char, float>()
    {
        { ',', 0.2f }, { '.', 0.3f }
    };

    // will say we're done even if we're only close
    int tolerance = 2;

    TextScrollTarget target;
    public TextScroller(TextScrollTarget target = null)
    {
        this.target = target;
    }

    // scrolls text by the given amount (in seconds)
    public void Advance(float amount)
    {
        if (textPosition >= textFull.Length)
        {
            return;
        }
        textTimer += amount;
        while (textPosition < textFull.Length && textTimer > GetDuration(textFull[textPosition]))
        {
            textTimer -= GetDuration(textFull[textPosition]);
            textPosition++;
            if (textPosition >= textFull.Length)
            {
                textPosition = textFull.Length;
                // notify the target that we finished
                if (target != null)
                {
                    target.FinishTextScroll();
                }
                break;
            }
        }
        current = textFull.Substring(0, Math.Min(textPosition + 1, textFull.Length));
    }

    // give a new string to scroll across (starts back at the beginning)
    public void SetNew(string text)
    {
        textFull = text;
        textPosition = 0;
        textTimer = 0f;
    }

    // get part of the string that we've scrolled across so far
    public string Text()
    {
        return current;
    }

    // used for checking if the text is still scrolling
    public bool Finished()
    {
        return textPosition >= textFull.Length - tolerance;
    }

    // check pause amount for a character
    private float GetDuration(char c)
    {
        if (textPause.ContainsKey(c))
        {
            return textPause[c];
        }
        return textPauseDefault;
    }
}