using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Different dialog names have different widths.
 * Additionally, the nameplate will have to be repositioned for different characters (e.g. right side or left side).
 * This component handles that. It is called by DialogDisplayBehavior.
 */
public class NameBoxBehavior : MonoBehaviour {

    [HideInInspector] public AdjustUIBehavior box;
    Text text;
    InputField field;
	void Awake()
    {
        box = GetComponent<AdjustUIBehavior>();
        text = GetComponentInChildren<Text>();
        field = GetComponent<InputField>();
    }
	
	// Set name and scale namebox to size
	public float SetName(string name)
    {
        // get pixels size
        int px = GetStringWidth(name);
        // pixels to width as MapView points
        float mapViewWidth = Mathf.Abs(ResolutionHandler.ScreenToMapViewPoint(new Vector3(px, 0)).x 
            - ResolutionHandler.ScreenToMapViewPoint(new Vector3(0, 0)).x);
        // add on (black bar x position / screenWidth) to shift over, avoiding the black bars, and then a bit (0.1f)
        mapViewWidth += Mathf.Abs(ResolutionHandler.MapViewToScreenPoint(new Vector3(0, 0)).x / Screen.width) + 0.1f;
        mapViewWidth = Mathf.Clamp(mapViewWidth, 0.4f, 1f);
        // set box size
        box.UpdateAnchors(box.left, box.left + mapViewWidth);
        // set text
        field.text = name;
        return mapViewWidth;
    }

    void Update()
    {

    }

    public int GetStringWidth(string message)
    {
        Font myFont = text.font;
        CharacterInfo characterInfo = new CharacterInfo();
        char[] arr = message.ToCharArray();
        // add up all the character widths
        int totalLength = 0;
        foreach (char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, text.fontSize);
            totalLength += characterInfo.advance;
        }
        return totalLength;
    }
}
