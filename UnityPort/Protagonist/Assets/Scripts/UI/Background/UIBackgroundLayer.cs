using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * One layer used in UIBackground. Uses UIBackgroundImage.
 */
public class UIBackgroundLayer : MonoBehaviour
{
    float duration = 0.5f;

    static UIBackgroundLayer instance;

    public GameObject UIBackgroundImage;
    UIBackgroundImage current;

    public bool Temporary { get; set; } = false;

    public void StartTransition(Sprite s)
    {
        if (current != null)
        {
            current.FadeOut(duration, duration);
        }
        current = Instantiate(UIBackgroundImage, transform).GetComponent<UIBackgroundImage>();
        current.Initialize(s);
        current.FadeIn(duration, 0f);
    }

    // destroy self if not needed
    void Update()
    {
        if (transform.childCount == 0 && Temporary)
        {
            Destroy(gameObject);
        }
    }
}
