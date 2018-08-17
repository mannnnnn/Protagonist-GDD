using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PuzzleSpell
{
    void Initialize(string spell, SpellInteractionTarget puzzle, SpellInputTarget player);
}
