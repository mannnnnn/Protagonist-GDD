using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles the puzzle letter object that flashes between normal and greek text.
 * Can also be Finish()'d to turn it black and have it fade out, then destroy itself.
 * Controlled by PuzzleSpellInput.
 */
public class PuzzleLetter : MonoBehaviour
{
    bool greek = false;
    bool obscured = false;
    LetterSprite sprites;
    // target is what we want to be, greek or not
    // if greek != target, then we want to transition so that greek == target
    bool target = false;
    float transitionTimer = 0f;
    float transitionDur = 0.3f;

    // transition every 1-4 seconds
    float timer = 0f;
    float duration = 1f;
    // fade out animation
    public bool finished = false;
    float fadeDuration = 1.5f;

    protected SpriteRenderer sr;

    public string letter { get; private set; }
    public void Initialize(string letter, Vector3 pos)
    {
        this.letter = letter;
        transform.position = pos;
    }
    public void Initialize(string letter, Vector3 pos, Vector2 size)
    {
        Initialize(letter, pos);
        transform.localScale = size;
    }

    // GetSize gets called right after creation so this is in Awake
    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    protected virtual void Start()
    {
        sprites = PuzzleLetterImages.Letters[letter];
        duration = Random.Range(1f, 2.5f);
        UpdateSprite();
    }

    protected virtual void Update()
    {
        UpdateSprite();
        if (!finished)
        {
            timer += GameTime.deltaTime;
            // swap forms every so often
            if (timer > duration)
            {
                timer = 0f;
                duration = Random.Range(0.5f, 2.5f);
                SetGreek(!greek);
            }
        }
        // fade out when finished (increment down)
        else
        {
            timer -= GameTime.deltaTime;
            sr.color = new Color(0, 0, 0, timer / duration);
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Method that sets the letter to transition to the greek state or normal state.
    // The transition is handled internally.
    public void SetGreek(bool greek)
    {
        target = greek;
        transitionTimer = 0f;
    }
    // handle transitions, since when we want to transition from Normal -> Greek for example,
    // the letter has to go from Normal -> Obscured Normal -> Obscured Greek -> Greek in a timed manner
    private void UpdateSprite()
    {
        sr.sprite = sprites.GetSprite(greek, obscured);
        // if transitioning
        if (greek != target)
        {
            obscured = true;
            transitionTimer += GameTime.deltaTime;
            // transition at halfway point
            bool transitionGreek = transitionTimer < 0.5f * transitionDur ? greek : !greek;
            sr.sprite = sprites.GetSprite(transitionGreek, obscured);
            if (transitionTimer > transitionDur)
            {
                greek = target;
                obscured = false;
                transitionTimer = 0f;
            }
        }
        // if finished, always display normal sprite
        if (finished)
        {
            sr.sprite = sprites.GetSprite(false, false);
        }
    }

    // we're done with this letter
    // it turns black and fades out
    public void Finish()
    {
        if (finished)
        {
            return;
        }
        sr.color = new Color(0, 0, 0, sr.color.a);
        finished = true;
        // increment down when fading out
        duration = fadeDuration;
        timer = duration;
    }

    // size for spacing reasons on creation
    public Vector2 GetSize()
    {
        return sr.bounds.extents * 2f;
    }
}
