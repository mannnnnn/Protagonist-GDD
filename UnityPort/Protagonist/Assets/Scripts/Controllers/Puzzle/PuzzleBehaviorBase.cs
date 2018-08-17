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
    void PlacedLetter();
    void SpellStart();
    void SpellFirstHit();
    void SpellHit();
    void SpellLastHit();
    void SpellEnd();
}