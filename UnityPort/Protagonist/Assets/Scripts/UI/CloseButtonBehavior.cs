using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface CloseButtonTarget
{
    void CloseButtonClick();
}

/**
 * Close button that outputs to some target
 */
public class CloseButtonBehavior : MonoBehaviour
{
    RectTransform rect;
    Image image;
    CloseButtonTarget target;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        target = FindTarget();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Rect screenRect = ResolutionHandler.GetScreenRect(rect);
            if (screenRect.Contains(Input.mousePosition))
            {
                target.CloseButtonClick();
            }
        }
    }

    // traverse up the parent tree until a CloseButtonTarget is found
    private CloseButtonTarget FindTarget()
    {
        Transform current = transform;
        while (current != null)
        {
            CloseButtonTarget target = current.GetComponentInParent<CloseButtonTarget>();
            if (target != null)
            {
                return target;
            }
            current = current.parent;
        }
        return null;
    }

    public void SetAlpha(float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
}
