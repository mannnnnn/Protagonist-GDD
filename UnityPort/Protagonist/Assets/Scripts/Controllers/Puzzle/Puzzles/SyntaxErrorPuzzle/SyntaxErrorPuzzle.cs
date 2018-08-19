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
public abstract class SyntaxErrorPuzzle : StandardPuzzleBehavior
{
    public GameObject line;

    public const float STANDARD_LINE_HEIGHT = 1f;
    public readonly Vector2 STANDARD_SIZE = new Vector2(0.7f, 0.7f);

    List<SyntaxErrorLine> lines = new List<SyntaxErrorLine>();
    bool completed = false;
    // flash the letter alpha as part of the completion animation
    float flashTimer = 0f;
    float flashDuration = 0.05f;
    // separate the letters, letting them all fall as part of the completion animation
    float separateTimer = 0f;
    protected float separateDuration = 1f;
    bool separated = false;
    // end puzzle
    float endPuzzleTimer = 0f;
    protected float endPuzzleDuration = 1f;

    protected override void SceneStart()
    {
        CreateLines();
    }

    void Update()
    {
        if (completed)
        {
            // flash transparency
            flashTimer += GameTime.deltaTime;
            if (flashTimer > flashDuration)
            {
                flashTimer = 0f;
                float alpha = Random.Range(0f, 1f);
                foreach (var letter in GetLetters())
                {
                    letter.SetAlpha(alpha);
                }
            }
            // separate letters
            if (!separated)
            {
                separateTimer += GameTime.deltaTime;
                if (separateTimer >= separateDuration)
                {
                    separated = true;
                    // fall away from top center of screen
                    Vector2 pos = ResolutionHandler.MapViewToWorldPoint(new Vector2(0.5f, 1f));
                    foreach (var letter in GetLetters())
                    {
                        Vector2 velocity = ((Vector2)letter.transform.position - pos).normalized * 5f;
                        letter.Fall(velocity);
                    }
                }
            }
            // then end the puzzle
            else
            {
                endPuzzleTimer += GameTime.deltaTime;
                if (endPuzzleTimer >= endPuzzleDuration)
                {
                    EndPuzzle();
                    Destroy(gameObject);
                }
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
                // if there's a hit, we're done
                completed = true;
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
