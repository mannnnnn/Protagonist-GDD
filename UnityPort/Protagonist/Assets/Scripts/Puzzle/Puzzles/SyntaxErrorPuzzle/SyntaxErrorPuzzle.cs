using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Base class for Syntax Error puzzles,
 * which are puzzles that display some text, and have the player find the error in the text.
 * Extend this class to create syntax error puzzles by implementing CreateLines and EndPuzzle.
 * You can easily create lines of text in the puzzle using the Line method.
 * See PuzzleOne for an example.
 */
public abstract class SyntaxErrorPuzzle : StandardPuzzle
{
    public GameObject line;

    public const float STANDARD_LINE_HEIGHT = 1f;
    public readonly Vector2 STANDARD_SIZE = new Vector2(0.7f, 0.7f);

    List<SyntaxErrorLine> lines = new List<SyntaxErrorLine>();
    bool completed = false;
    FlickerThenFall endAnimation;
    // which music to play
    string triMusic = "Syntax1";

    protected override void Start()
    {
        base.Start();
        TriMusicPlayer.Get(triMusic).Play();
    }

    protected override void SceneStart()
    {
        CreateLines();
    }

    void Update()
    {
        if (completed)
        {
            TriMusicPlayer.Get(triMusic).Stop();
            if (endAnimation == null)
            {
                EndPuzzle();
                Destroy(gameObject);
            }
        }
    }

    // override these in subclasses. Call Line() a bunch at different positions
    protected abstract void CreateLines();
    // probably go to previous room and set some dialog flag in a sublclass
    protected abstract void EndPuzzle();

    // used to create one line. Returns height so you can stack them more easily
    protected float Line(string lineText, Vector3 pos, Vector2 size = default(Vector2), List<int> correct = null, float initialDelay = 2f, float letterDelay = 0.075f)
    {
        // handle non-constant defaults
        if (correct == null)
        {
            correct = new List<int>();
        }
        if (size == default(Vector2))
        {
            size = STANDARD_SIZE;
        }
        // create the line
        SyntaxErrorLine lineBehavior = Instantiate(line).GetComponent<SyntaxErrorLine>();
        lineBehavior.Initialize(lineText, pos, size, initialDelay, letterDelay, correct);
        lines.Add(lineBehavior);
        return STANDARD_LINE_HEIGHT * size.y;
    }

    // when a spell hits somewhere, scan every letter for a collision
    public override void SpellHit(string spell, Vector2 pos)
    {
        if (completed)
        {
            return;
        }
        foreach (var letter in GetLetters())
        {
            if (letter.correct && letter.Contains(pos))
            {
                // if there's a hit, we're done. Play the end animation
                completed = true;
                endAnimation = gameObject.AddComponent<FlickerThenFall>();
                endAnimation.Initialize(GetLetters());
            }
        }
    }
    private IEnumerable<SyntaxErrorLetter> GetLetters()
    {
        foreach (var line in lines)
        {
            foreach (var letter in line.GetLetters())
            {
                yield return letter;
            }
        }
    }
}
