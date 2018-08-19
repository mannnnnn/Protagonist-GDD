using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface PuzzleSpell
{
    void Initialize(string spell, SpellInteractionTarget puzzle, SpellInputTarget player);
}

public abstract class StandardPuzzleBehavior : PuzzleBehaviorBase, SpellInteractionTarget
{
    // whether we've finished the transition or not
    protected bool active = false;

    string scene;
    public override void Initialize(string scene)
    {
        this.scene = scene;
    }

    protected virtual void Start()
    {
        // should create transition to the target scene in child
        DontDestroyOnLoad(gameObject);
        SceneTransitions.Transition("Spin", new TransitionTime(1f, 0.3f, 1f), scene);
        SceneManager.sceneLoaded += StartScene;
    }

    protected virtual void StartScene(Scene scene, LoadSceneMode mode)
    {
        // activate destroy on load again
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        // finished transitioning
        active = true;
        SceneManager.sceneLoaded -= StartScene;
        // create player targeting this puzzle
        Puzzles.CreatePlayer(this);
    }

    public virtual void PlacedLetter(string letter) { }
    public virtual void SpellEnd(string spell) { }
    public virtual void SpellFirstHit(string spell, Vector2 pos) { }
    public virtual void SpellHit(string spell, Vector2 pos) { }
    public virtual void SpellLastHit(string spell, Vector2 pos) { }
    public virtual void SpellStart(string spell) { }
}
