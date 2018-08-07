using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
 * Base for dialog boxes and groups of dialog boxes.
 * Controls the state and state transition.
 * Handles setting y position (for x position, use AdjustUIBehavior), and provides a framework for other functionality.
 * Note that this is abstract, so you must implement these functions on your own later.
 * One implementation, for example, is DialogDisplayBehavior.
 */
public abstract class DialogDisplayBase : MonoBehaviour
{
    // state and state transitions
    public float duration;
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
    float targetY = 0f;
    protected float spd = 600f;

    protected virtual void Update()
    {
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
                    SetState(State.OPEN);
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
                }
                break;
            case State.CLOSING:
                timerSeconds -= Time.deltaTime;
                if (timer < 0)
                {
                    SetState(State.CLOSED);
                }
                timerSeconds = Mathf.Clamp(timerSeconds, 0, duration);
                break;
        }
        SetY(Mathf.MoveTowards(GetY(), targetY, spd * Time.deltaTime));
        SetAlpha(timer);
    }

    // call to set state, such as opening/closing
    public virtual void SetState(State state)
    {
        this.state = state;
    }
    protected virtual bool PendingClose()
    {
        return true;
    }

    // y position
    public abstract float GetY();
    protected abstract void SetY(float screenY);
    public void SetTargetY(float screenY)
    {
        targetY = screenY;
    }

    public abstract void SetAlpha(float alpha);
}
