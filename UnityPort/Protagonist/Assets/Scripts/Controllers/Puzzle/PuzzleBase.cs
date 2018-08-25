using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * All spells are given a target scene.
 * If you don't want to change scenes in your puzzle, just don't do anything with it.
 * Otherwise, create a transition using SceneTransitions.Transition(type, duration, scene)
 */
public abstract class PuzzleBase : MonoBehaviour
{
    public abstract void Initialize(string scene);
}

/**
 * Interface that defines a puzzle that player spells can interact with.
 * Standard letter-throwing puzzles must implement this, or else it cannot create a player.
 */
public interface SpellInteractionTarget
{
    void PlacedLetter(string letter);
    void SpellStart(string spell);
    void SpellFirstHit(string spell, Vector2 pos);
    void SpellHit(string spell, Vector2 pos);
    void SpellLastHit(string spell, Vector2 pos);
    void SpellEnd(string spell);
}