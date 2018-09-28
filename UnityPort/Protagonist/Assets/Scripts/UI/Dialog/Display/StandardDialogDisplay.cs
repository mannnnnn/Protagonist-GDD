using Assets.Scripts.Libraries.ProtagonistDialog;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UIDisplayBase;

/**
 * A dialog display is everything controlling activation, and SetText, TextFinished, Advance, and SetMenu.
 * This allows the dialog system to control and output to a display.
 */
public interface DialogDisplay
{
    bool Active { get; }
    void Show();
    // Hide means the dialog display can choose when to deactivate when able.
    void Hide();
    // Stop means the dialog display must start deactivating immediately.
    void Stop();

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

    protected override void Awake()
    {
        base.Awake();
        textScroller = new TextScroller(this);
        // get components
        dialogBox = transform.Find("DialogueBox").gameObject.GetComponent<UIPanel>();
        nameBox = transform.Find("NameBox").gameObject.GetComponent<UIPanel>();
        setNameBox = GetComponentInChildren<NameBox>();
    }

    protected void Start()
    {
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
        //TODO: return nameBox.size.y + dialogBox.size.y;
        return 0;
    }

    public override void SetAlpha(float alpha)
    {
        nameBox.alpha = alpha;
        if (nameBox.text == "")
        {
            nameBox.alpha = 0;
        }
        dialogBox.alpha = alpha;
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
        nameBox.text = character;
        //TODO: nameBox.x = center;
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
        GameObject menuObj = Instantiate(DialogPrefabs.Menus[type], UICanvas.root);
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
        dialogBox.text = text;
    }

    public void Show()
    {
        if (state != State.OPEN)
        {
            SetState(State.OPENING);
        }
    }

    public void Hide()
    {
        if (state != State.CLOSING && state != State.CLOSED)
        {
            state = State.PENDING_CLOSE;
        }
    }

    public void Stop()
    {
        if (state != State.CLOSED)
        {
            state = State.CLOSING;
        }
    }
}