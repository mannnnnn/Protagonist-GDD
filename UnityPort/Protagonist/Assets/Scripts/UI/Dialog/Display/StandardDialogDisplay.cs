using Assets.Scripts.Libraries.ProtagonistDialog;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UIDisplayBase;

/**
 * A dialog display is everything in UIDisplayBase, then SetText, TextFinished, AdvanceText, and SetMenu
 */
public interface DialogDisplay
{
    // UIDisplayBase
    State state { get; }
    void SetState(State state);
    float GetY();
    void SetTargetY(float screenY);
    void SetAlpha(float alpha);
    float GetSize();

    // dialog display
    bool Active { get; }
    void SetText(List<string> characters, string text);
    bool TextFinished();
    void Advance(float amount);
    List<Dictionary<string, object>> SetMenu(List<Dictionary<string, object>> options, string type);
}

/**
 * Handles display-related dialog tasks.
 * This includes setting text contents, setting y position (height), alpha, and state.
 * This also handles the text scrolling animation and its related functionality (like talk/speak animation)
 * For the logic related to how dialog scripts are executed, see the DialogParser object.
 * For the logic bridging the DialogParser object to the display, see the Dialog component.
 */
public class StandardDialogDisplay : UIDisplayBase, DialogDisplay, TextScrollTarget
{
    UIPanel dialogBox;
    UIPanel nameBox;
    NameBox setNameBox;
    // if dialog is executing or we aren't closed, we're active
    public bool Active => state != State.CLOSED || Dialog.GetInstance().Enabled;

    GameObject menu;
    TextScroller textScroller;
    
    // move mouths
    List<DialogAnimationBase> speakers = new List<DialogAnimationBase>();

    protected void Start()
    {
        textScroller = new TextScroller(this);
        // get components
        dialogBox = transform.Find("DialogueBox").gameObject.GetComponent<UIPanel>();
        nameBox = transform.Find("NameBox").gameObject.GetComponent<UIPanel>();
        setNameBox = GetComponentInChildren<NameBox>();
        SetName("");
        // set y position and y target position
        SetY(0);
        SetTargetY(0);
        // start out transparent
        SetAlpha(0);
    }

    protected override void Update()
    {
        // handle timer/state
        base.Update();
        // set text scroll
        if (Active)
        {
            textScroller.Advance(UITime.deltaTime);
        }
        SetText(textScroller.Text());
        // set target position
        switch (state)
        {
            case State.OPENING:
            case State.PENDING_CLOSE:
                SetTargetY(GetSize());
                break;
            case State.CLOSING:
            case State.CLOSED:
                SetTargetY(0);
                break;
        }
        GetY();
        // adjust to screen size
        dialogBox.UpdateAnchors();
        nameBox.UpdateAnchors();
    }

    // when we want to close, but have to wait for dialog animations to finish
    protected override bool PendingClose()
    {
        bool close = true;
        foreach (DialogCharacter chr in Dialog.GetInstance().parser.characters.Values)
        {
            if (chr.gameObject != null)
            {
                chr.gameObject.GetComponent<DialogAnimationBase>().SetTransition(chr.transition, true, null);
                chr.gameObject = null;
            }
        }
        // can't close until all dialog animations are destroyed
        GameObject dialogAnim = GameObject.FindGameObjectWithTag("DialogAnimation");
        close = dialogAnim == null;
        return close;
    }
    
    // total vertical size of the two boxes stacked on each other
    public float GetSize()
    {
        return nameBox.GetSize() + dialogBox.GetSize();
    }

    public override void SetAlpha(float alpha)
    {
        nameBox.SetAlpha(alpha);
        if (nameBox.GetText() == "")
        {
            nameBox.SetAlpha(0);
        }
        dialogBox.SetAlpha(alpha);
    }

    // call to set the text contents of the display, both nameplate and textbox. Called by Dialog
    public void SetText(List<string> characters, string text)
    {
        SetName(characters);
        textScroller.SetNew(text);
        StartSpeakers(characters);
    }
    private void SetName(List<string> characters)
    {
        DialogParser parser = Dialog.GetInstance().parser;
        // calculate name
        string character = "";
        if (characters.Count != 0)
        {
            character += string.Join(", ", characters.Select(x => parser.characters[x].name).Take(characters.Count - 1).ToArray());
            if (characters.Count != 1)
            {
                character += " and ";
            }
            string last = characters[characters.Count - 1];
            character += parser.characters[last].name;
        }
        // calculating average position to put name at
        float center = 0f;
        foreach (string c in characters)
        {
            center += Dialog.sides[parser.characters[c].position].x;
        }
        center = center / characters.Count;
        SetName(character, center);
    }
    private void SetName(string character, float center = 0f)
    {
        // get size of text
        float size = setNameBox.SetName(character);
        // clamp position in between the screen sides
        float left = Mathf.Clamp(center - (size * 0.5f), 0, 1 - size);
        if (size >= 1)
        {
            left = 0;
        }
        // set namebox position to where the person is
        setNameBox.box.left = Mathf.Clamp01(left);
        setNameBox.box.right = Mathf.Clamp01(left + size);
        setNameBox.box.UpdateAnchors();
    }
    public void StartSpeakers(List<string> characters)
    {
        DialogParser parser = Dialog.GetInstance().parser;
        // set who is speaking
        speakers.Clear();
        if (parser != null)
        {
            foreach (string c in parser.characters.Keys)
            {
                // speaking is true if in the character list, false otherwise
                GameObject dialogAnim = parser.characters[c].gameObject;
                if (dialogAnim != null)
                {
                    bool speak = characters.Contains(c);
                    // set as speaking
                    DialogAnimationBase anim = dialogAnim.GetComponent<DialogAnimationBase>();
                    anim.SetSpeaking(speak);
                    anim.SetTalk(speak);
                    // add to speakers list to turn off speaking later
                    if (speak)
                    {
                        speakers.Add(anim);
                    }
                }
            }
        }
    }

    // dialog menu creation, called by Dialog
    public List<Dictionary<string, object>> SetMenu(List<Dictionary<string, object>> options, string type)
    {
        // create a menu
        if (!DialogPrefabs.Menus.ContainsKey(type))
        {
            throw new ParseError("Menu Type with name '" + type + "' is not registered. See the DialogSystem's DialogPrefabs component.");
        }
        // set on same level as the dialog menu group
        GameObject menuObj = Instantiate(DialogPrefabs.Menus[type], UICanvas.GetTransform());
        DialogMenu menu = menuObj.GetComponent<DialogMenu>();
        if (menu == null)
        {
            throw new ParseError("Prefab for Menu Type '" + type + "' has no DialogMenu component.");
        }
        return menu.Initialize(options, Dialog.GetInstance().parser, Dialog.GetInstance(), this);
    }

    // text scroll methods
    public bool TextFinished()
    {
        return textScroller.Finished();
    }
    public void Advance(float amount)
    {
        textScroller.Advance(amount);
    }
    public void FinishTextScroll()
    {
        //stop the talking animation
        foreach (DialogAnimationBase anim in speakers)
        {
            if (anim != null)
            {
                anim.SetTalk(false);
            }
        }
        speakers.Clear();
    }
    private void SetText(string text)
    {
        dialogBox.SetText(text);
    }
}