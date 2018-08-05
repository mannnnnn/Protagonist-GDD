using Assets.Scripts.Libraries.ProtagonistDialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimationBehavior : MonoBehaviour {

    public float height;
    SpriteRenderer sr;
    Animator anim;
    DialogAnimation transition;

    // destroy this gameObj when the next transition finishes
    // SetTransition with destroy=true is called if it is a hide transition
    bool destroy = false;

    // Use this for initialization
    void Awake() {
        // get components
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ResolutionHandler res = ResolutionHandler.GetInstance();
        // calculate height based off of height as a fraction of mapview
        float worldPx = Mathf.Abs(res.MapViewToWorldPoint(new Vector2(0f, height)).y
            - res.MapViewToWorldPoint(Vector2.zero).y);
        float scale = worldPx / (2 * sr.sprite.bounds.extents.y);
        sr.transform.localScale = new Vector3(scale, scale, sr.transform.localScale.z);
        // set at bottom of mapview
        transform.position = new Vector3(transform.position.x, res.MapViewToWorldPoint(Vector2.zero).y, transform.position.z);
	}
	
    public void SetTalk(bool value)
    {
        anim.SetBool("Talk", value);
    }
    public void SetImage(DialogBehavior.DialogImage image)
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
    // if marked for destruction, destroy when transition is done
    void Update()
    {
        if (destroy && transition != null && transition.Finished())
        {
            Destroy(gameObject);
        }
    }
}