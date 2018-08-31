using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightedText : MonoBehaviour
{
    List<HighlightedTextLine> highlightedLines = new List<HighlightedTextLine>();
    float lineY;
    public GameObject linePrefab;

    RectTransform rect;
    Text text;
	void Start()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponent<Text>();
        // make sure our font is rendered, otherwise we get 0 on string widths.
        text.text = "Haha Savvy, I was just kidding.";
        Canvas.ForceUpdateCanvases();
        text.text = "";
        lineY = rect.rect.size.y * 0.5f;
    }

    public void SetText(string s)
    {
        StringBreaker sb = new StringBreaker(text.font, text.fontSize);
        List<string> lines = sb.Break(s, rect.rect.width);
        // create lines if needed
        for (int i = highlightedLines.Count; i < lines.Count; i++)
        {
            var line = Instantiate(linePrefab, transform).GetComponent<HighlightedTextLine>();
            var lineRect = line.GetComponent<RectTransform>();
            lineRect.anchoredPosition = new Vector2(lineRect.anchoredPosition.x, lineRect.anchoredPosition.y + lineY);
            line.Initialize(text.font, text.fontSize);
            lineY -= line.GetSize();
            highlightedLines.Add(line);
        }
        // remove unused
        for (int i = lines.Count; i < highlightedLines.Count; i++)
        {
            lineY += highlightedLines[i].GetSize();
            Destroy(highlightedLines[i].gameObject);
            highlightedLines.RemoveAt(i);
            i--;
        }
        // set lines
        for (int i = 0; i < lines.Count; i++)
        {
            highlightedLines[i].Initialize(text.font, text.fontSize);
            highlightedLines[i].SetText(lines[i]);
        }
    }

    /**
     * Helper class that breaks a string up into lines that fit into a given width.
     */
    class StringBreaker
    {
        Font font;
        int size;
        public StringBreaker(Font font, int size)
        {
            this.font = font;
            this.size = size;
        }

        public List<string> Break(string s, float maxWidth)
        {
            List<string> lines = new List<string>() { "" };
            float width = 0f;
            for (int i = 0; i < s.Length;)
            {
                string word = NextWord(s, i);
                i += word.Length;
                if (word == "\n")
                {
                    lines.Add("");
                    width = 0f;
                }
                else
                {
                    float px = Utilities.GetStringWidth(word, font, size);
                    // fit a word on the same line if we can
                    if (width + px <= maxWidth || string.IsNullOrWhiteSpace(word))
                    {
                        lines[lines.Count - 1] += word;
                        width += px;
                    }
                    // otherwise it goes on the next line
                    else
                    {
                        lines.Add(word);
                        width = px;
                    }
                }
            }
            return lines;
        }

        // obtains the next word, starting at position i, in the string.
        // can only return a whitespace char, "\n", or a non-whitespace word.
        private string NextWord(string s, int start)
        {
            string word = "";
            for (int i = start; i < s.Length; i++)
            {
                if (char.IsWhiteSpace(s[i]))
                {
                    // if we start on a whitespace char, output it
                    if (word.Length == 0)
                    {
                        return s[i].ToString();
                    }
                    // if we have a word then hit a whitespace char, output the word
                    else
                    {
                        return word;
                    }
                }
                word += s[i];
            }
            // if we hit the end of the string, output what we have
            return word;
        }
    }
} 
