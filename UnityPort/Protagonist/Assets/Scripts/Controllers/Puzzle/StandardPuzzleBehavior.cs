using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class that controls the player during puzzles.
 * This controls player input and spellcasting, and calls the puzzle's methods to notify it when spells occur.
 */
public class StandardPuzzleBehavior : PuzzleBehaviorBase, SpellInteractionTarget
{
    // finished the transition
    bool active = false;

    string scene;
    public override void Initialize(string scene)
    {
        this.scene = scene;
    }

    protected virtual void Start()
    {
        // should create transition to the target scene in child
        DontDestroyOnLoad(gameObject);
        SceneTransitions.Transition<SceneTransition>(new SceneTransitions.Time(1f, 1f, 1f), scene);
        SceneManager.sceneLoaded += StartScene;
    }

    protected virtual void StartScene(Scene scene, LoadSceneMode mode)
    {
        // finished transitioning
        active = true;
        SceneManager.sceneLoaded -= StartScene;
        // create player

    }

    public virtual void PlacedLetter() { }
    public virtual void SpellEnd() { }
    public virtual void SpellFirstHit() { }
    public virtual void SpellHit() { }
    public virtual void SpellLastHit() { }
    public virtual void SpellStart() { }
}
