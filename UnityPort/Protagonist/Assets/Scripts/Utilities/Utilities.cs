using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Utilities
{
    public static float GetStringWidth(string s, Font font, int fontSize)
    {
        CharacterInfo characterInfo = new CharacterInfo();
        char[] arr = s.ToCharArray();
        // add up all the character widths
        int totalLength = 0;
        foreach (char c in arr)
        {
            font.GetCharacterInfo(c, out characterInfo, fontSize);
            totalLength += characterInfo.advance;
        }
        return totalLength;
    }
    public static float GetStringHeight(Font f, int fontSize)
    {
        return f.ascent * fontSize * 0.1f;
    }

    public static float FreeLerp(float value, float aMin, float aMax, float bMin, float bMax)
    {
        return (((value - aMin) * ((bMax - bMin) / (aMax - aMin))) + bMin);
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
