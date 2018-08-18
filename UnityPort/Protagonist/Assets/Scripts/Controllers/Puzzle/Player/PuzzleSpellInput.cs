﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spells use this to tell the player that it's done casting
// so the player can cast the next one
public interface SpellInputTarget
{
    // called when the spell is finished casting
    void CompleteSpell();
    // the letter game objects used to cast this spell
    List<PuzzleLetter> GetLetters();
}

// creates letters and casts spells based on player input.
public class PuzzleSpellInput : MonoBehaviour, SpellInputTarget
{
    SpellInteractionTarget puzzle;

    public GameObject letterPrefab;
    // letters before casting the spell
    List<PuzzleLetter> letters = new List<PuzzleLetter>();
    // letters after casting the spell
    List<PuzzleLetter> spellLetters = new List<PuzzleLetter>();
    int SpellSize = 7;

    // currently casting a spell or not (casting makes the player unable to type a new spell)
    public bool Casting { get; private set; } = false;

    Vector2 pos;
    void Start()
    {
        pos = ResolutionHandler.MapViewToWorldPoint(new Vector2(0.2f, 0.2f));
        puzzle = GetComponent<PuzzlePlayerBehavior>().puzzle;
    }

    void Update()
    {
        // handle spell letter input
        for (KeyCode i = KeyCode.A; i <= KeyCode.Z; i++)
        {
            var letterStr = i.ToString();
            if (Input.GetKeyDown(i))
            {
                // make sure player has this letter
                if (!Spells.Letters.ContainsKey(letterStr) || !Spells.Letters[letterStr])
                {
                    continue;
                }
                // make sure we can type another letter
                if (Casting || letters.Count >= SpellSize)
                {
                    continue;
                }
                // create letter for this key press
                GameObject letterObj = Instantiate(letterPrefab);
                PuzzleLetter letterBehavior = letterObj.GetComponent<PuzzleLetter>();
                letterBehavior.Initialize(i.ToString(), pos + new Vector2(letters.Count * letterBehavior.GetSize().x, 0));
                letters.Add(letterBehavior);
                puzzle.PlacedLetter(i.ToString());
            }
        }
        // cast a spell if possible
        if (Input.GetMouseButtonDown(0) && letters.Count > 0)
        {
            string spell = "";
            foreach (PuzzleLetter letter in letters)
            {
                spell += letter.letter;
            }
            // if it's in the Spells dictionary, use that one. Otherwise, use the default "" throwing letters
            spellLetters = letters;
            letters = new List<PuzzleLetter>();
            Spells.CreateSpell(spell, puzzle, this);
            puzzle.SpellStart(spell);
            Casting = true;
        }
    }

    public void CompleteSpell()
    {
        Casting = false;
        PuzzleCursor.ReleaseInnerCrosshair();
    }

    public List<PuzzleLetter> GetLetters()
    {
        return spellLetters;
    }
}
