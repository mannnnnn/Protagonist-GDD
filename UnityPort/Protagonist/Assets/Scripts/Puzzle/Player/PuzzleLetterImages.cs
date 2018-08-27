using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Simple class that contains the mappings of string letters to groups of sprites.
 */
public class PuzzleLetterImages : MonoBehaviour
{
    public List<LetterSprite> letters;
    public static readonly Dictionary<string, LetterSprite> Letters = new Dictionary<string, LetterSprite>();
    
    void Awake ()
    {
        foreach (LetterSprite spr in letters)
        {
            Letters[spr.name] = spr;
        }
    }
}

/**
 * Groups the 4 sprites of a puzzle letter into one object.
 * A puzzle letter can be displayed normally, using the greek text, normally but faded, and in greek text but faded.
 */
[Serializable]
public class LetterSprite
{
    public string name;
    public Sprite normal;
    public Sprite greek;
    public Sprite fade;
    public Sprite fadeGreek;

    public Sprite GetSprite(bool greek, bool obscured)
    {
        if (!greek)
        {
            if (!obscured)
            {
                return normal;
            }
            else
            {
                return fade;
            }
        }
        else
        {
            if (!obscured)
            {
                return this.greek;
            }
            else
            {
                return fadeGreek;
            }
        }
    }
}