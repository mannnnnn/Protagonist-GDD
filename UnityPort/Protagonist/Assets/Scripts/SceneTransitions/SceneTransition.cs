using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // current state of the SceneTransition
    protected State state = State.IN;
    // represents what state the SceneTransition is in
    public enum State
    {
        IN, HOLD, OUT
    }

    // durations for each state, in seconds
    public float inDuration;
    public float holdDuration;
    public float outDuration;
    public SceneTransitions.Side side = SceneTransitions.Side.NONE;

    // times each state
    float timerSeconds;

    // the scene to transition to
    public string targetScene;

    // triggered scene change or not
    bool changedScene = false;

    // texture drawn to screen
    protected Texture2D tex;
    // cover the black bars during transitions
    // used in CoverBlackBars()
    private Texture2D black;

    // handle timing and state changes
    void Update()
    {
        timerSeconds += Time.deltaTime;
        switch (state)
        {
            // increment timer up from 0 until inDuration is reached
            case State.IN:
                if (timerSeconds >= inDuration)
                {
                    timerSeconds = 0;
                    state = State.HOLD;
                }
                break;
            // increment timer up from 0 until holdDuration is reached
            case State.HOLD:
                if (timerSeconds >= holdDuration)
                {
                    timerSeconds = 0;
                    // trigger the scene change
                    if (!changedScene)
                    {
                        changedScene = true;
                        // if it's to a valid scene
                        if (targetScene != null && targetScene != "")
                        {
                            DontDestroyOnLoad(gameObject);
                            SceneManager.LoadScene(targetScene);
                        }
                        // otherwise do a blank transition
                        else
                        {
                            state = State.OUT;
                        }
                    }
                }
                break;
            // increment timer up from 0 until outDuration is reached
            case State.OUT:
                if (timerSeconds >= outDuration)
                {
                    timerSeconds = 0;
                    Destroy(gameObject);
                }
                break;
        }
    }

    // get a normalized version of the timer
    // returns a value [0, 1], increasing on State.IN, 1 on State.HOLD, decreasing on State.OUT
    public float timer
    {
        get
        {
            switch (state)
            {
                case State.IN:
                    if (inDuration == 0)
                    {
                        return 0;
                    }
                    return timerSeconds / inDuration;
                case State.HOLD:
                    return 1;
                case State.OUT:
                    if (outDuration == 0)
                    {
                        return 0;
                    }
                    return 1 - (timerSeconds / outDuration);
            }
            return 0;
        }
    }

    // draw on the screen based on the timer, override this
    void OnGUI()
    {
        // cover black bars on the side
        CoverBlackBars();

        // set the GUI drawing color to have the given alpha
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, timer);
        // init texture if necessary
        if (tex == null)
        {
            tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.black);
            tex.Apply();
        }
        // draw texture to fill screen
        Vector3 topLeft = ResolutionHandler.GetInstance().MapViewToScreenPoint(new Vector2(0, 0));
        Vector3 size = ResolutionHandler.GetInstance().MapViewToScreenPoint(new Vector2(1, 1)) - topLeft;
        GUI.DrawTexture(new Rect(topLeft, size), tex);
    }

    // used to cover the black bars during transitions
    protected void CoverBlackBars()
    {
        // set the GUI drawing color to have full alpha
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1);
        // init black texture to cover the side bars
        if (black == null)
        {
            black = new Texture2D(1, 1);
            black.SetPixel(0, 0, Color.black);
            black.Apply();
        }
        // draw the 4 possible black bars:
        Vector3 origin;
        Vector3 size;

        // left bar is Screen(0, 0) to MapView(0, 1)
        origin = Vector2.zero;
        size = ResolutionHandler.GetInstance().MapViewToScreenPoint(new Vector2(0, 1)) - origin;
        GUI.DrawTexture(new Rect(origin, size), black);

        // right bar is MapView(1, 0) to Screen(1, 1)
        origin = ResolutionHandler.GetInstance().MapViewToScreenPoint(new Vector2(1, 0));
        size = new Vector3(Screen.width, Screen.height) - origin;
        GUI.DrawTexture(new Rect(origin, size), black);

        // top bar is Screen(0, 0) to MapView(1, 0)
        origin = Vector2.zero;
        size = ResolutionHandler.GetInstance().MapViewToScreenPoint(new Vector2(1, 0)) - origin;
        GUI.DrawTexture(new Rect(origin, size), black);

        // bottom bar is MapView(0, 1) to Screen(1, 1)
        origin = ResolutionHandler.GetInstance().MapViewToScreenPoint(new Vector2(0, 1));
        size = new Vector3(Screen.width, Screen.height) - origin;
        GUI.DrawTexture(new Rect(origin, size), black);
    }

    //Add callback on room chance when created
    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneChange;
    }
    //Add callback on room change when finished
    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneChange;
    }

    // when the scene changes, go from hold state to out state
    protected virtual void SceneChange(Scene scene, LoadSceneMode mode)
    {
        state = State.OUT;
    }
}
