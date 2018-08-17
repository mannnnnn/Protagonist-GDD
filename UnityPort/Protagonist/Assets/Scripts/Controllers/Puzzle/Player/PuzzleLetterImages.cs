using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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