using Assets.Scripts.Libraries.ProtagonistDialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Every DialogAnimation GameObject should have this component.
 * Allows it to perform various actions, such as set image, talk animation, position, and transition.
 * These are automatically created by the dialog system.
 */
public class DialogAnimationBehavior : MonoBehaviour {

    public float height;
    SpriteRenderer sr;
    Animator anim;
    DialogAnimation transition;

    // destroy this gameObj when the next transition finishes
    // SetTransition with destroy=true is called if it is a hide transition
    bool destroy = false;

    // move to position. spd is in mapview units (fraction of screen width per second)
    float spd = 1f;
    private float worldSpd;
    Vector2 pos = Vector2.zero;

    // flip if on left side of the screen
    public bool flipOnLeft = false;

    public bool displaySpeaking = true;
    bool speaking = false;
    // base scale for resizing, and how much to shrink/darken
    Vector2 baseScale;
    Color baseColor;
    float shrink = 0.94f;
    float shrinkSpd = 0.5f;
    float darken = 0.8f;
    float darkenSpd = 2f;

    // Use this for initialization
    void Awake() {
        // get components
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        // calculate height based off of height as a fraction of mapview
        float worldPx = Mathf.Abs(ResolutionHandler.MapViewToWorldPoint(new Vector2(0f, height)).y
            - ResolutionHandler.MapViewToWorldPoint(Vector2.zero).y);
        float scale = worldPx / (2 * sr.sprite.bounds.extents.y);
        baseScale = new Vector3(scale, scale, sr.transform.localScale.z);
        sr.transform.localScale = baseScale;
        // set at bottom of mapview
        transform.position = new Vector3(transform.position.x, ResolutionHandler.MapViewToWorldPoint(Vector2.zero).y, transform.position.z);
        pos = transform.position;
        // calculate speed in world coords
        worldSpd = Mathf.Abs(ResolutionHandler.MapViewToWorldPoint(Vector2.right * spd).x
            - ResolutionHandler.MapViewToWorldPoint(Vector2.zero).x);
        baseColor = sr.color;
    }
	
    public void SetTalk(bool value)
    {
        anim.SetBool("Talk", value);
    }
    public void SetImage(Dialog.DialogImage image)
    {
        anim.SetInteger("Image", (int)image);
    }

    public void SetTransition(string name, bool destroy = false, Dictionary<string, object> data = null)
    {
        this.destroy = destroy;
        // remove old one
        var oldTransition = GetComponent<DialogAnimation>();
        Destroy(oldTransition);
        // set alpha
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1 - (destroy ? 0 : 1));
        // create new transition
        switch (name)
        {
            case "Default":
            case "Fade":
                DialogAnimationFade fade = gameObject.AddComponent<DialogAnimationFade>();
                fade.Initialize(destroy ? 0 : 1);
                transition = fade;
                break;
            case "Jump":
                DialogAnimationJump jump = gameObject.AddComponent<DialogAnimationJump>();
                jump.Initialize(destroy ? 0 : 1);
                transition = jump;
                break;
            case "Swing":
                DialogAnimationSwing swing = gameObject.AddComponent<DialogAnimationSwing>();
                if (!destroy)
                {
                    sr.transform.localEulerAngles = new Vector3(0, 0, 90);
                }
                swing.Initialize(destroy ? 0 : 1, destroy ? 90 : 0);
                transition = swing;
                break;
            default:
                throw new ParseError("Transition named " + transition + " does not exist. Add one in AdjustDialogAnimation.SetTransition");
        }
    }

    public void SetPosition(Vector2 pos, bool instant = false)
    {
        if (instant)
        {
            transform.position = ResolutionHandler.MapViewToWorldPoint(pos);
        }
        this.pos = ResolutionHandler.MapViewToWorldPoint(pos);
    }

    // whether or not this is the character that is speaking
    // if not speaking, shrink a bit and gray out
    public void SetSpeaking(bool speaking)
    {
        this.speaking = speaking;
    }

    void Update()
    {
        // if marked for destruction, destroy when transition is done
        if (destroy && transition != null && transition.Finished())
        {
            Destroy(gameObject);
        }
        // move towards target position
        if ((Vector2)transform.position != pos)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos, worldSpd * UITime.deltaTime);
        }
        // if necessary and on left side of the screen, flip sprite
        if (flipOnLeft)
        {
            if (ResolutionHandler.WorldToMapViewPoint(transform.position).x < 0.5f)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
        // shrink and darken if not speaking
        if (displaySpeaking)
        {
            float currentCol = sr.color.r;
            float currentSize = sr.transform.localScale.x;
            // move towards base if speaking
            if (speaking)
            {
                currentCol = Mathf.MoveTowards(currentCol, baseColor.r, darkenSpd * UITime.deltaTime * baseColor.r);
                currentSize = Mathf.MoveTowards(currentSize, baseScale.x, shrinkSpd * UITime.deltaTime * baseScale.x);
            }
            // move towards smaller and darker if not speaking
            else
            {
                currentCol = Mathf.MoveTowards(currentCol, baseColor.r * darken, darkenSpd * UITime.deltaTime * baseColor.r);
                currentSize = Mathf.MoveTowards(currentSize, baseScale.x * shrink, shrinkSpd * UITime.deltaTime * baseScale.x);
            }
            sr.color = new Color(currentCol, currentCol, currentCol, sr.color.a);
            sr.transform.localScale = new Vector3(currentSize, currentSize, sr.transform.localScale.z);
        }
    }
}