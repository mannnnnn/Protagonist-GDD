using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handles display-related dialog tasks.
 * This includes setting text contents, setting y position (height), alpha, and state.
 * This also handles the text scrolling animation and its related functionality (like talk/speak animation)
 * For the logic related to how dialog scripts are executed, see the Dialog object.
 * For the logic bridging the Dialog object to the display, see the DialogBehavior component.
 */
public class DialogDisplayBehavior : DialogDisplayBase {

    DialogTextbox dialogBox;
    DialogTextbox nameBox;
    NameBoxBehavior setNameBox;
    DialogBehavior dialogBehavior;

    GameObject menu;

    // scroll text
    string textFull = "";
    int textPosition = 0;
    float textTimer = 0f;
    // pause for different times at , and .
    const float textPauseDefault = 0.06f;
    static Dictionary<char, float> textPause = new Dictionary<char, float>()
    {
        { ',', 0.1f }, { '.', 0.2f }
    };
    private float GetTextPause(char c)
    {
        if (textPause.ContainsKey(c))
        {
            return textPause[c];
        }
        return textPauseDefault;
    }
    // move mouths
    List<DialogAnimationBehavior> speakers = new List<DialogAnimationBehavior>();

    protected void Start()
    {
        // get components
        dialogBox = new DialogTextbox(transform.Find("DialogueBox").gameObject);
        nameBox = new DialogTextbox(transform.Find("NameBox").gameObject);
        setNameBox = GetComponentInChildren<NameBoxBehavior>();
        dialogBehavior = GetComponentInParent<DialogBehavior>();
        SetName("");
        // set y position and y target position
        SetY(0);
        SetTargetY(0);
    }

    protected override void Update()
    {
        // handle timer/state
        base.Update();
        // set text scroll
        UpdateTextScroll();
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
    }

    // when we want to close, but have to wait for dialog animations to finish
    protected override bool PendingClose()
    {
        bool close = true;
        foreach (DialogCharacter chr in dialogBehavior.dialog.characters.Values)
        {
            if (chr.gameObject != null)
            {
                chr.gameObject.GetComponent<DialogAnimationBehavior>().SetTransition(chr.transition, true, null);
                chr.gameObject = null;
            }
        }
        // can't close until all dialog animations are destroyed
        GameObject dialogAnim = GameObject.FindGameObjectWithTag("DialogAnimation");
        close = dialogAnim == null;
        return close;
    }

    // y position
    public override float GetY()
    {
        return ResolutionHandler.RectToScreenPoint(new Vector2(0, gameObject.transform.position.y)).y;
    }
    protected override void SetY(float screenY)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
            ResolutionHandler.ScreenToRectPoint(new Vector2(0, screenY)).y, gameObject.transform.position.y);
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

    // call to set the text contents of the display, both nameplate and textbox. Called by DialogBehavior
    public void SetText(List<string> characters, string text, Dialog dialog = null)
    {
        // calculate name
        string character = "";
        if (characters.Count != 0)
        {
            character += string.Join(", ", characters.Select(x => dialog.characters[x].name).Take(characters.Count - 1).ToArray());
            if (characters.Count != 1)
            {
                character += " and ";
            }
            string last = characters[characters.Count - 1];
            character += dialog.characters[last].name;
        }
        // set name
        float center = 0f;
        foreach (string c in characters)
        {
            center += DialogBehavior.sides[dialog.characters[c].position].x;
        }
        center = center / characters.Count;
        SetName(character, center);
        // set text
        textFull = text;
        textPosition = 0;
        textTimer = 0f;
        // set who is speaking
        speakers.Clear();
        if (dialog != null)
        {
            foreach (string c in dialog.characters.Keys)
            {
                // speaking is true if in the character list, false otherwise
                GameObject dialogAnim = dialog.characters[c].gameObject;
                if (dialogAnim != null)
                {
                    bool speak = characters.Contains(c);
                    // set as speaking
                    DialogAnimationBehavior anim = dialogAnim.GetComponent<DialogAnimationBehavior>();
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
    // handles the text scroll animation. called by Update
    private void UpdateTextScroll()
    {
        textTimer += Time.deltaTime;
        if (state == State.CLOSED)
        {
            textTimer = 0f;
        }
        while (textPosition < textFull.Length && textTimer > GetTextPause(textFull[textPosition]))
        {
            textTimer -= GetTextPause(textFull[textPosition]);
            textPosition++;
            if (textPosition >= textFull.Length)
            {
                textPosition = textFull.Length;
                // we finished scrolling, so stop the talking animation
                foreach (DialogAnimationBehavior anim in speakers)
                {
                    if (anim != null)
                    {
                        anim.SetTalk(false);
                    }
                }
                speakers.Clear();
                break;
            }
        }
        SetText(textFull.Substring(0, Math.Min(textPosition + 1, textFull.Length)));
    }
    private void SetText(string text)
    {
        dialogBox.SetText(text);
    }

    // used for checking if the text is being displayed
    public bool TextFinished()
    {
        return textPosition == textFull.Length;
    }
    // used to fast-forward the text scrolling.
    // Note that to advance to the next statement, you need to call Run in DialogBehavior.
    public void AdvanceText(float amount)
    {
        textTimer += amount;
    }

    // dialog menu creation, called by DialogBehavior
    public List<Dictionary<string, object>> SetMenu(List<Dictionary<string, object>> options, string type, Dialog dialog)
    {
        // create a menu
        if (!DialogPrefabs.Menus.ContainsKey(type))
        {
            throw new ParseError("Menu Type with name '" + type + "' is not registered. See the DialogSystem's DialogPrefabs component.");
        }
        // set on same level as the dialog menu group
        GameObject menuObj = Instantiate(DialogPrefabs.Menus[type], dialogBehavior.transform);
        DialogMenuBehavior menu = menuObj.GetComponent<DialogMenuBehavior>();
        if (menu == null)
        {
            throw new ParseError("Prefab for Menu Type '" + type + "' has no DialogMenuBehavior component.");
        }
        return menu.Initialize(options, dialog, dialogBehavior, this);
    }
}