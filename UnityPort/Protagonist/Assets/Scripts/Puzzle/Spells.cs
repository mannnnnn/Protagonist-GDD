using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PuzzleSpell
{
    void Initialize(string spell, SpellInteractionTarget puzzle, SpellInputTarget player);
}

/**
 * Maps spells to spell prefabs via the inspector.
 * Spells.CreateSpell is used to use one of these prefabs to create a spell instance.
 * Also contains what letters the player has available.
 */
public class Spells : MonoBehaviour
{
    public List<KeyedPrefab> spells;
    static readonly Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();

    public static readonly Dictionary<string, bool> Letters = new Dictionary<string, bool>()
    {
        { "S", true }, { "E", true }, { "A", true }, { "C", true }, { "T", true }, { "R", true }, { "D", true }
    };

    void Awake()
    {
        foreach (KeyedPrefab prefab in spells)
        {
            Prefabs[prefab.name] = prefab.prefab;
        }
    }

    public static PuzzleSpell CreateSpell(string spell, SpellInteractionTarget puzzle, SpellInputTarget player)
    {
        // if unrecognized, use default spell ""
        GameObject prefab = Prefabs[""];
        if (Prefabs.ContainsKey(spell))
        {
            prefab = Prefabs[spell];
        }
        // create and initialize
        GameObject puzzleObj = Instantiate(prefab);
        PuzzleSpell spellBehavior = puzzleObj.GetComponent<PuzzleSpell>();
        if (spellBehavior == null)
        {
            throw new InvalidOperationException("Spell prefab '" + spell + "' has no component that implements PuzzleSpell.");
        }
        spellBehavior.Initialize(spell, puzzle, player);
        return spellBehavior;
    }
}
