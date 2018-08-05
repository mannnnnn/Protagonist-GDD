using Assets.Scripts.Libraries.ProtagonistDialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogDisplayBehavior : MonoBehaviour {

    DialogTextbox dialogBox;
    DialogTextbox nameBox;
    NameBoxBehavior setNameBox;
    DialogBehavior dialogBehavior;
    
    public float duration;
    float timerSeconds = 0;
    float timer { get { return timerSeconds / duration; } }
    public bool active { get { return state == State.OPEN; } }

    public State state { get; private set; }
    public enum State
    {
        CLOSED, OPENING, OPEN, PENDING_CLOSE, CLOSING
    }

    // needs a delayed initialization since the UI elements take some warming up before going to correct position
    bool initialized = false;

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
            nameBox.UpdateBasePosition();
            dialogBox.UpdateBasePosition();
            initialized = true;
        }
        // handle state
        switch (state)
        {
            case State.CLOSED:
                timerSeconds = 0;
                break;
            case State.OPENING:
                timerSeconds += Time.deltaTime;
                if (timer > 1)
                {
                    state = State.OPEN;
                }
                timerSeconds = Mathf.Clamp(timerSeconds, 0, duration);
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
                break;
            case State.CLOSING:
                timerSeconds -= Time.deltaTime;
                if (timer < 0)
                {
                    state = State.CLOSED;
                }
                timerSeconds = Mathf.Clamp(timerSeconds, 0, duration);
                break;
        }
        // set UI components
        SetAlpha(timer);
        SetPosition(new Vector2(0, Mathf.Lerp(-400, 0, timer)));
    }

    public void SetState(State state)
    {
        this.state = state;
    }

    public void SetText(string character, string text)
    {
        setNameBox.SetName(character);
        dialogBox.SetText(text);
    }
    protected void SetAlpha(float alpha)
    {
        nameBox.SetAlpha(alpha);
        dialogBox.SetAlpha(alpha);
    }
    protected void SetPosition(Vector2 pos)
    {
        nameBox.SetPosition(pos);
        dialogBox.SetPosition(pos);
    }

    // helper class used to wrap setting alpha/position/text into one object per GameObject.
    // Then, this behavior wraps it into one method call.
    class DialogTextbox
    {
        GameObject gameObj;
        InputField textbox;
        Text text;
        Image image;
        RectTransform rect;
        Vector2 initialPos;

        public DialogTextbox(GameObject g)
        {
            gameObj = g;
            textbox = gameObj.GetComponentInChildren<InputField>();
            text = gameObj.GetComponentInChildren<Text>();
            image = gameObj.GetComponentInChildren<Image>();
            rect = gameObj.GetComponent<RectTransform>();
        }

        public void UpdateBasePosition()
        {
            initialPos = rect.transform.localPosition;
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
        public void SetPosition(Vector2 pos)
        {
            rect.localPosition = initialPos + pos;
        }
    }
}
