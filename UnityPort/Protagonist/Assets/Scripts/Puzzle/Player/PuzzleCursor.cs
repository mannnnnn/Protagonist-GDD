using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The cursor that follows the mouse during puzzles.
 * The inside of the cursor detaches from the main cursor when a spell is targeting a certain position.
 * This is controlled by PuzzleSpellInput.
 */
public class PuzzleCursor : MonoBehaviour
{
    public List<Sprite> outerSprites;
    public List<Sprite> innerSprites;

    // change sprite
    float spinSpd = -240f;
    int subimage = 0;
    float timerSeconds = 0f;
    float duration = 0.3f;

    bool lockInner = false;
    Vector2 lockPos;
    
    static PuzzleCursor Instance;
    public static PuzzleCursor GetInstance()
    {
        return Instance;
    }

    SpriteRenderer outer;
    SpriteRenderer inner;
    void Start()
    {
        // only one cursor
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        outer = GetComponent<SpriteRenderer>();
        inner = transform.Find("PuzzleCursorInner").GetComponent<SpriteRenderer>();
        Instance = this;
    }

    // rotate and change sprites
    void Update()
    {
        UpdateImage();
        // normally be at mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        if (inner != null)
        {
            inner.transform.localPosition = new Vector3(0, 0, inner.transform.position.z);
            // if inner crosshair is locked at a position, move it to that position instead
            if (lockInner)
            {
                inner.transform.position = new Vector3(lockPos.x, lockPos.y, inner.transform.position.z);
            }
        }
    }

    private void UpdateImage()
    {
        // spin
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + spinSpd * UITime.deltaTime);
        // change image
        timerSeconds += UITime.deltaTime;
        if (timerSeconds > duration && outerSprites.Count > 0)
        {
            // next image
            subimage = (subimage + 1) % outerSprites.Count;
            timerSeconds = 0f;
            outer.sprite = outerSprites[subimage];
            if (inner != null)
            {
                inner.sprite = innerSprites[subimage];
            }
        }
    }

    public static void LockInnerCrosshair(Vector2 pos)
    {
        GetInstance().lockInner = true;
        GetInstance().lockPos = pos;
    }
    public static void ReleaseInnerCrosshair()
    {
        GetInstance().lockInner = false;
    }

    public static Vector2 GetPosition()
    {
        return GetInstance().transform.position;
    }
}
