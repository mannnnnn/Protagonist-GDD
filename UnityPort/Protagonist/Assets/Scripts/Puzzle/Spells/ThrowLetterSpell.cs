using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/**
 * Simple interface that is a spell that hits multiple times.
 * The child behaviors just call Hit(gameObject), then this object would use that information to
 * intelligently call SpellInteractionTarget's SpellFirstHit, SpellLastHit, and Hit methods based on when the hits occur.s
 */
public interface MultiHitSpell : PuzzleSpell
{
    void Hit(GameObject gameObj);
}

/**
 * Basic default spell that just throws the letters typed at a target.
 */
public class ThrowLetterSpell : MonoBehaviour, MultiHitSpell
{
    public GameObject throwLetter;

    Vector2 targetPos;
    List<PuzzleLetter> letters;
    int letterHits = 0;

    // time between each letter thrown
    float timerSeconds = 1f;
    float duration = 0.1f;
    int letterIndex = 0;

    string spell;
    SpellInteractionTarget puzzle;
    SpellInputTarget player;
    public void Initialize(string spell, SpellInteractionTarget puzzle, SpellInputTarget player)
    {
        this.spell = spell;
        this.puzzle = puzzle;
        this.player = player;
    }

    void Start()
    {
        letters = new List<PuzzleLetter>(player.GetLetters());
        targetPos = PuzzleCursor.GetPosition();
        PuzzleCursor.LockInnerCrosshair(targetPos);
    }

    void Update()
    {
        timerSeconds += GameTime.deltaTime;
        if (timerSeconds >= duration && letterIndex < letters.Count)
        {
            timerSeconds = 0f;
            // throw the letter by finishing the original letter and spawning a new one in its position
            PuzzleLetter old = letters[letterIndex];
            ThrownLetter thrownLetter = Instantiate(throwLetter, old.transform.position, old.transform.rotation)
                .GetComponent<ThrownLetter>();
            thrownLetter.Initialize(spell[letterIndex].ToString(), targetPos, this);
            old.Finish();
            letterIndex++;
        }
        // when all letters have hit, we're done
        if (letterHits == letters.Count)
        {
            player.CompleteSpell();
            Destroy(gameObject);
        }
    }

    // called by ThrownLetter when a thrown letter hits
    public void Hit(GameObject gameObj)
    {
        if (letterHits == 0)
        {
            puzzle.SpellFirstHit(spell, gameObj.transform.position);
        }
        if (letterHits == letters.Count - 1)
        {
            puzzle.SpellLastHit(spell, gameObj.transform.position);
        }
        puzzle.SpellHit(spell, gameObj.transform.position);
        letterHits++;
    }
}