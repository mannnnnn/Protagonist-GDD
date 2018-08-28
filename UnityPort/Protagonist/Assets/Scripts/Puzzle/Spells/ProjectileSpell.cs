using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Base class used by spells that are just one projectile that is thrown at a given position.
 * This includes spells like SEAR, SEA, CARD, etc.
 */
public abstract class ProjectileSpell : MonoBehaviour, PuzzleSpell
{
    bool moving = false;
    bool finished = false;
    // timer that determines how long to wait before moving to target
    float timerSeconds = 0f;
    public float castDuration = 0.5f;
    // starting speed and acceleration
    public float spd = 4f;
    public float acceleration = 4f;
    public float spinSpd = 360f;
    // shrink in size
    public float shrinkMin = 0.4f;
    public float shrinkSpd = 0.3f;

    protected Vector2 targetPos;
    List<PuzzleLetter> letters;

    protected string spell;
    protected SpellInteractionTarget puzzle;
    protected SpellInputTarget player;
    public void Initialize(string spell, SpellInteractionTarget puzzle, SpellInputTarget player)
    {
        this.spell = spell;
        this.puzzle = puzzle;
        this.player = player;
    }

    protected virtual void Start()
    {
        targetPos = PuzzleCursor.GetPosition();
        PuzzleCursor.LockInnerCrosshair(targetPos);
        // finish all letters
        foreach (PuzzleLetter letter in player.GetLetters())
        {
            letter.Finish();
        }
        Initialize();
    }

    protected virtual void Update()
    {
        // first, wait the casting duration
        if (!moving && !finished)
        {
            timerSeconds += GameTime.deltaTime;
            if (timerSeconds >= castDuration)
            {
                moving = true;
                StartMoving();
            }
        }
        // then move to target
        else if (!finished)
        {
            // spin
            transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + spinSpd * GameTime.deltaTime);
            // move
            Vector2 pos = (Vector3)Vector2.MoveTowards(transform.position, targetPos, spd * GameTime.deltaTime);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            spd += acceleration * GameTime.deltaTime;
            // shrink
            transform.localScale = new Vector3(Decay(transform.localScale.x, shrinkSpd, shrinkMin),
                Decay(transform.localScale.y, shrinkSpd, shrinkMin), 1f);
            // if reached target, we're done
            if (Vector2.Distance(transform.position, targetPos) < 0.01f)
            {
                finished = true;
                // tell puzzle and player that we're finished
                puzzle.SpellHit(spell, targetPos);
                player.CompleteSpell();
                // finish effect
                FinishMoving();
            }
        }
    }

    // called when the object Start()'s.
    // override this in a subclass
    protected virtual void Initialize()
    {

    }

    // called when the object starts moving to the target position
    // override this in a subclass
    protected virtual void StartMoving()
    {

    }

    // called when the object is at the target position
    // override this in a subclass
    protected virtual void FinishMoving()
    {

    }

    public static float Decay(float current, float shrinkSpd, float shrinkMin)
    {
        // fraction of (current - shrinkMin) that we want to decrease
        float shrinkFactor = Mathf.Pow(shrinkSpd, GameTime.deltaTime);
        return shrinkMin + (current - shrinkMin) * shrinkFactor;
    }
}
