using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Allows fading in/out of images at the back of the UI but in front of the game.
 * Uses UIBackgroundImage
 */
public class UIBackground : MonoBehaviour
{
    float duration = 0.5f;

    static UIBackground instance;

    public GameObject UIBackgroundImage;
    UIBackgroundImage current;

	// Use this for initialization
	void Start ()
    {
		if (instance != null)
        {
            return;
        }
        instance = this;
	}

    private void StartTransition(Sprite s)
    {
        if (current != null)
        {
            current.FadeOut(duration, duration);
        }
        current = Instantiate(UIBackgroundImage, transform).GetComponent<UIBackgroundImage>();
        current.Initialize(s);
        current.FadeIn(duration, 0f);
    }
    public static void TransitionTo(Sprite s)
    {
        instance.StartTransition(s);
    }
}
