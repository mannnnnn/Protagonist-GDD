using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour {

    static Canvas canvas;
    static Transform rootTransform;

    // holds the UI canvas singleton
    void Start() {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }
        if (rootTransform == null)
        {
            rootTransform = GetComponent<Transform>();
        }
    }

    public static Canvas GetCanvas()
    {
        return canvas;
    }
    public static Transform GetTransform()
    {
        return rootTransform;
    }

    void Update()
    {
    }
}
