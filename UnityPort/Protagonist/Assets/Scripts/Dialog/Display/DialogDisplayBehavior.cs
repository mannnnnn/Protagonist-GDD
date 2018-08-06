using Assets.Scripts.Libraries.ProtagonistDialog;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogDisplayBehavior : MonoBehaviour {

    DialogTextbox dialogBox;
    DialogTextbox nameBox;
    NameBoxBehavior setNameBox;
    DialogBehavior dialogBehavior;

    GameObject menu;

    public float duration;
    float timerSeconds = 0;
    float timer { get { return timerSeconds / duration; } }
    public bool active { get { return state == State.OPEN; } }

    public State state { get; private set; }
    public enum State
    {
        CLOSED, OPENING, OPEN, PENDING_CLOSE, CLOSING
    }

    // resize
    float initialSize;
    float targetSize;
    float sizeSpd = 400f;

    // needs a delayed initialization since the UI elements take some warming up before going to correct position
    bool initialized = false;

    // move mouths
    List<DialogAnimationBehavior> speakers = new List<DialogAnimationBehavior>();
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

    // y position deviation from starting position
    float targetHeight = 0f;
    float spd = 600f;

    void Start()
    {
        // get components
        dialogBox = new DialogTextbox(GameObject.FindGameObjectWithTag("DialogueBox"));
        nameBox = new DialogTextbox(GameObject.FindGameObjectWithTag("NameBox"));
        setNameBox = GetComponentInChildren<NameBoxBehavior>();
        dialogBehavior = GetComponent<DialogBehavior>();
    }

    void Update()
    {
        // handle delayed init on getting initial positions
        if (!initialized)
        {
            SetName("");
            nameBox.SetBaseHeight();
            dialogBox.SetBaseHeight();
            initialSize = dialogBox.GetSize();
            targetSize = initialSize;
            // start down
            targetHeight = -300f;
            nameBox.SetHeight(targetHeight);
            dialogBox.SetHeight(targetHeight);
            initialized = true;
        }
        // handle state
        switch (state)
        {
            case State.CLOSED:
                timerSeconds = 0;
                targetSize = initialSize;
                targetHeight = -300f;
                textTimer = 0f;
                break;
            case State.OPENING:
                timerSeconds += Time.deltaTime;
                if (timer > 1)
                {
                    state = State.OPEN;
                }
                timerSeconds = Mathf.Clamp(timerSeconds, 0, duration);
                targetHeight = 0f;
                break;
            case State.OPEN:
                break;
            // we want to close, but not all dialog sprites are out of the way yet
            case State.PENDING_CLOSE:
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
                if (close)
                {
                    state = State.CLOSING;
                }
                targetSize = initialSize;
                break;
            case State.CLOSING:
                timerSeconds -= Time.deltaTime;
                if (timer < 0)
                {
                    state = State.CLOSED;
                }
                timerSeconds = Mathf.Clamp(timerSeconds, 0, duration);
                targetSize = initialSize;
                targetHeight = -300f;
                break;
        }
        // set UI components
        SetAlpha(timer);
        SetSize(Mathf.MoveTowards(dialogBox.GetSize(), targetSize, sizeSpd * Time.deltaTime));
        SetHeight(Mathf.MoveTowards(dialogBox.GetHeight(), targetHeight, spd * Time.deltaTime));
        // set text scroll
        textTimer += Time.deltaTime;
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
        SetText(textFull.Substring(0, textPosition));
    }

    public void SetState(State state)
    {
        this.state = state;
    }

    // call to set the text contents of the display, both nameplate and textbox
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
    private void SetText(string text)
    {
        dialogBox.SetText(text);
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

    public void SetAlpha(float alpha)
    {
        nameBox.SetAlpha(alpha);
        if (nameBox.GetText() == "")
        {
            nameBox.SetAlpha(0);
        }
        dialogBox.SetAlpha(alpha);
    }
    protected void SetHeight(float height)
    {
        nameBox.SetHeight(height);
        dialogBox.SetHeight(height);
    }

    // sets size to initialSize + size
    public void SetTargetSize(float size)
    {
        targetSize = size + initialSize;
    }
    private void SetSize(float size)
    {
        // move namebox up
        nameBox.SetHeight(nameBox.GetHeight() + (size - initialSize));
        // set size
        dialogBox.SetSize(size);
    }

    public List<Dictionary<string, object>> SetMenu(List<Dictionary<string, object>> options, string type, Dialog dialog)
    {
        // create a menu
        if (!DialogPrefabs.Menus.ContainsKey(type))
        {
            throw new ParseError("Menu Type with name '" + type + "' is not registered. See the DialogSystem's DialogPrefabs component.");
        }
        GameObject menuObj = Instantiate(DialogPrefabs.Menus[type], dialogBox.gameObj.transform);
        DialogMenuBehavior menu = menuObj.GetComponent<DialogMenuBehavior>();
        if (menu == null)
        {
            throw new ParseError("Prefab for Menu Type '" + type + "' has no DialogMenuBehavior component.");
        }
        return menu.Initialize(options, dialog);
    }

    private float GetTextPause(char c)
    {
        if (textPause.ContainsKey(c))
        {
            return textPause[c];
        }
        return textPauseDefault;
    }

    // helper class used to wrap setting alpha/position/text into one object per GameObject.
    // Then, this behavior wraps it into one method call.
    class DialogTextbox
    {
        public GameObject gameObj;
        InputField textbox;
        Text text;
        Image image;
        RectTransform rect;
        float initialHeight;

        public DialogTextbox(GameObject g)
        {
            gameObj = g;
            textbox = gameObj.GetComponentInChildren<InputField>();
            text = gameObj.GetComponentInChildren<Text>();
            image = gameObj.GetComponentInChildren<Image>();
            rect = gameObj.GetComponent<RectTransform>();
        }

        public float GetBaseHeight()
        {
            return initialHeight;
        }
        public void SetBaseHeight()
        {
            SetBasePosition(rect.transform.localPosition.y);
        }
        public void SetBasePosition(float height)
        {
            initialHeight = height;
        }

        public string GetText()
        {
            return textbox.text;
        }
        public void SetText(string message)
        {
            textbox.text = message;
        }

        public void SetAlpha(float alpha)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            image.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }

        public float GetHeight()
        {
            return rect.localPosition.y - initialHeight;
        }
        public void SetHeight(float height)
        {
            rect.localPosition = new Vector3(rect.localPosition.x, initialHeight + height, rect.localPosition.z);
        }

        public float GetSize()
        {
            return rect.rect.height;
        }
        public void SetSize(float size)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, size);
        }
    }
}
