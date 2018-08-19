using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Hold the UI control object's canvas.
 * Used for coordinate conversions.
 */
public class UICanvas : MonoBehaviour {

    static Canvas canvas;
    static Transform rootTransform;
    static UICanvas instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // holds the UI canvas singleton
    void Start() {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            SceneManager.sceneLoaded += SwitchCamera;
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

    void SwitchCamera(Scene scene, LoadSceneMode mode)
    {
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
