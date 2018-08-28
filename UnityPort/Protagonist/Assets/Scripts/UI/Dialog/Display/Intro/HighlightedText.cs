using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightedText : MonoBehaviour
{
    string message = "Did you ever hear the tragedy of Darth Plagueis The Wise? I thought not. It's not a story the Jedi would tell you. It's a Sith legend. Darth Plagueis was a Dark Lord of the Sith, so powerful and so wise he could use the Force to influence the midichlorians to create life… He had such a knowledge of the dark side that he could even keep the ones he cared about from dying. The dark side of the Force is a pathway to many abilities some consider to be unnatural. He became so powerful… the only thing he was afraid of was losing his power, which eventually, of course, he did. Unfortunately, he taught his apprentice everything he knew, then his apprentice killed him in his sleep. Ironic. He could save others from death, but not himself.";

    RectTransform rect;
    Text text;
	void Start()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }

	void Update()
    {
        SetText(message);
    }

    void SetText(string s)
    {
        List<string> lines = new List<string>();
        float lineWidth = rect.rect.width;
        Debug.Log(lineWidth);
        // split into lines by width
        string[] words = s.Split(' ');
        int width = 0;
        foreach (string word in words)
        {
            int px = Utilities.GetStringWidth(word, text.font, text.fontSize);
            if (lines.Count == 0)
            {
                lines.Add(word);
                width = px;
            }
            else if (width + px <= lineWidth)
            {
                lines[lines.Count - 1] += ' ' + word;
                width += px;
            }
            else
            {
                lines.Add(word);
                width = 0;
            }
        }
        text.text = string.Join("\n", lines);
    }
}
