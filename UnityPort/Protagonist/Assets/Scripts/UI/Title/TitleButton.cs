using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour {

    public bool debug = false;
    RectTransform rect;
    
	void Start ()
    {
        rect = GetComponent<RectTransform>();

    }

	void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            if (ScreenResolution.GetScreenRect(rect).Contains(Input.mousePosition))
            {
                SceneTransitions.Transition("Fade", new TransitionTime(1f), "jungle1");
            }
        }
	}
}
