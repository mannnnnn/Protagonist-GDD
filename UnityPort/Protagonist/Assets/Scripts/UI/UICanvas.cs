using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour {

    static Canvas canvas;
    RectTransform rect;

    // holds the UI canvas singleton
    void Start () {
        rect = GetComponent<RectTransform>();
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }
	}

    public static Canvas GetCanvas()
    {
        return canvas;
    }

    void Update()
    {
    }
}
