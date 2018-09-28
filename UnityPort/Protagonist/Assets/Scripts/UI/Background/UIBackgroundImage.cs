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

    SpriteRenderer sr;

    Sprite sprite;
    public void Initialize(Sprite sprite)
    {
        if (sprite == null)
        {
            Destroy(gameObject);
        }
        sr.sprite = sprite;
        SetAlpha(0f);
        // scale to fit area
        Vector2 center = ScreenResolution.MapViewToScreenPoint(new Vector2(0.5f, 0.5f));
        Vector2 corner = ScreenResolution.MapViewToScreenPoint(Vector2.zero);
        Vector2 extents = center - corner;
        Vector2 currentExtents = Camera.main.WorldToScreenPoint(sr.bounds.extents) - Camera.main.WorldToScreenPoint(Vector2.zero);
        transform.localScale = new Vector2(extents.x / currentExtents.x, extents.y / currentExtents.y);
        transform.localPosition = new Vector3(corner.x, corner.y, transform.localPosition.z);
    }

	// Use this for initialization
	void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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
        if (destroy && sr.color.a == 0f)
        {
            Destroy(gameObject);
        }
	}

    public void FadeIn(float duration, float delay)
    {
        initialAlpha = sr.color.a;
        targetAlpha = 1f;
        this.duration = duration;
        this.delay = delay;
        timerSeconds = 0f;
    }
    public void FadeOut(float duration, float delay, bool destroy = true)
    {
        initialAlpha = sr.color.a;
        targetAlpha = 0f;
        this.duration = duration;
        this.delay = delay;
        timerSeconds = 0f;
        this.destroy = destroy;
    }

    private void SetAlpha(float alpha)
    {
        if (sr.sprite == null)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
            return;
        }
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }
}
