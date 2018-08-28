using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Different dialog names have different widths.
 * Additionally, the nameplate will have to be repositioned for different characters (e.g. right side or left side).
 * This component handles that. It is called by StandardDialogDisplay.
 */
public class NameBox : MonoBehaviour
{
    [HideInInspector] public UIPanel box;
    Text text;
    InputField field;
	void Awake()
    {
        box = GetComponent<UIPanel>();
        text = GetComponentInChildren<Text>();
        field = GetComponent<InputField>();
    }
	
	// Set name and scale namebox to size
	public float SetName(string name)
    {
        // get pixels size
        int px = Utilities.GetStringWidth(name, text.font, text.fontSize);
        // pixels to width as MapView points
        float mapViewWidth = Mathf.Abs(ScreenResolution.ScreenToMapViewPoint(new Vector3(px, 0)).x 
            - ScreenResolution.ScreenToMapViewPoint(new Vector3(0, 0)).x);
        // add on (black bar x position / screenWidth) to shift over, avoiding the black bars, and then a bit (0.1f)
        mapViewWidth += Mathf.Abs(ScreenResolution.MapViewToScreenPoint(new Vector3(0, 0)).x / Screen.width) + 0.1f;
        mapViewWidth = Mathf.Clamp(mapViewWidth, 0.4f, 1f);
        // set box size
        box.right = box.left + mapViewWidth;
        box.UpdateAnchors();
        // set text
        field.text = name;
        return mapViewWidth;
    }

    void Update()
    {

    }
}
