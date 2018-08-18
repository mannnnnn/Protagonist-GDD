using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class PuzzleBehaviorBase : MonoBehaviour
{
    public abstract void Initialize(string scene);
}

public interface SpellInteractionTarget
{
    void PlacedLetter(string letter);
    void SpellStart(string spell);
    void SpellFirstHit(string spell, Vector2 pos);
    void SpellHit(string spell, Vector2 pos);
    void SpellLastHit(string spell, Vector2 pos);
    void SpellEnd(string spell);
}