using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
 * Base for dialog boxes and groups of dialog boxes.
 * Controls the state and state transition.
 * Handles setting y position (for x position, use AdjustUI), and provides a framework for other functionality.
 * Note that this is abstract, so you must implement these functions on your own later.
 * One implementation, for example, is StandardDialogDisplay.
 */
public abstract class UIDisplayBase : MonoBehaviour
{
    // state and state transitions
    public float duration = 0.5f;
    float timerSeconds = 0;
    protected float timer { get { return timerSeconds / duration; } }
    public bool active { get { return state == State.OPEN; } }

    public State state { get; protected set; }
    public enum State
    {
        CLOSED, OPENING, OPEN, PENDING_CLOSE, CLOSING
    }

    // y position movement.
    // Make sure you call SetTargetY in the Start statement, or else this will move to 0 every time.
    bool toTarget = false;
    float targetY = 0f;
    protected float spd = 600f;

    protected RectTransform rect;

    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    protected virtual void Update()
    {
        // handle state
        switch (state)
        {
            case State.CLOSED:
                timerSeconds = 0;
                break;
            case State.OPENING:
                timerSeconds += UITime.deltaTime;
                if (timer > 1)
                {
                    SetState(State.OPEN);
                    OpenFinish();
                }
                timerSeconds = Mathf.Clamp(timerSeconds, 0, duration);
                break;
            case State.OPEN:
                break;
            // we want to close, but not all dialog sprites are out of the way yet
            case State.PENDING_CLOSE:
                if (PendingClose())
                {
                    SetState(State.CLOSING);
                    CloseStart();
                }
                break;
            case State.CLOSING:
                timerSeconds -= UITime.deltaTime;
                if (timer < 0)
                {
                    SetState(State.CLOSED);
                    CloseFinish();
                }
                timerSeconds = Mathf.Clamp(timerSeconds, 0, duration);
                break;
        }
        // move to target until it is reached
        if (toTarget)
        {
            SetY(Mathf.MoveTowards(GetY(), targetY, spd * UITime.deltaTime));
            if (Mathf.Abs(targetY - GetY()) < 1f)
            {
                toTarget = false;
            }
        }
        SetAlpha(timer);
    }

    // call to set state, such as opening/closing
    public virtual void SetState(State state)
    {
        if (this.state == State.CLOSED && state == State.OPENING)
        {
            OpenStart();
        }
        this.state = state;
    }
    protected virtual bool PendingClose()
    {
        return true;
    }
    // methods called on state changes
    protected virtual void OpenStart()
    {
    }
    protected virtual void OpenFinish()
    {
    }
    protected virtual void CloseStart()
    {
    }
    protected virtual void CloseFinish()
    {
    }

    // y position
    public virtual float GetY()
    {
        return ScreenResolution.RectToScreenPoint(rect, new Vector2(0, 0)).y;
    }
    protected virtual void SetY(float screenY)
    {
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x,
            rect.anchoredPosition.y + ScreenResolution.ScreenToRectPoint(rect, new Vector2(0, screenY)).y);
    }
    public void SetTargetY(float screenY)
    {
        toTarget = true;
        targetY = screenY;
    }

    public abstract void SetAlpha(float alpha);
}
