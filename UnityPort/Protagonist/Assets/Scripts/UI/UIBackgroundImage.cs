using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Allows for simple fade in/fade out of a UI background image.
 * Used by the UIBackground class.
 */
public class UIBackgroundImage : MonoBehaviour
{
    float initialAlpha = 0f;
    float targetAlpha = 0f;
    float timerSeconds = 0f;
    float delay = 1f;
    float duration = 1f;

    bool destroy = false;

    Image image;

    Sprite sprite;
    public void Initialize(Sprite sprite)
    {
        if (sprite == null)
        {
            Destroy(gameObject);
        }
        image.sprite = sprite;
        SetAlpha(0f);
    }

	// Use this for initialization
	void Awake()
    {
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update()
    {
        if (timerSeconds < delay + duration)
        {
            timerSeconds += UITime.deltaTime;
            SetAlpha(Mathf.Lerp(initialAlpha, targetAlpha, (timerSeconds - delay) / duration));
        }
        else
        {
            SetAlpha(targetAlpha);
        }
        timerSeconds = Mathf.Clamp(timerSeconds, 0, delay + duration);
        // clean up self if animation sequence is finished
        if (destroy && image.color.a == 0f)
        {
            Destroy(gameObject);
        }
	}

    public void FadeIn(float duration, float delay)
    {
        initialAlpha = image.color.a;
        targetAlpha = 1f;
        this.duration = duration;
        this.delay = delay;
        timerSeconds = 0f;
    }
    public void FadeOut(float duration, float delay, bool destroy = true)
    {
        initialAlpha = image.color.a;
        targetAlpha = 0f;
        this.duration = duration;
        this.delay = delay;
        timerSeconds = 0f;
        this.destroy = destroy;
    }

    private void SetAlpha(float alpha)
    {
        if (image.sprite == null)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            return;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
}
