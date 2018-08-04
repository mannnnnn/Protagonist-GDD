using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// adjust width when a name is set
public class NameBoxBehavior : MonoBehaviour {

    AdjustUIBehavior box;
    Text text;
    InputField field;
	void Start ()
    {
        box = GetComponent<AdjustUIBehavior>();
        text = GetComponentInChildren<Text>();
        field = GetComponent<InputField>();
    }
	
	// Update is called once per frame
	public void SetName(string name)
    {
        // get pixels size
        int px = GetStringWidth(name);
        // pixels to width as MapView points
        var convert = ResolutionHandler.GetInstance();
        float mapViewWidth = Mathf.Abs(convert.ScreenToMapViewPoint(new Vector3(px, 0)).x 
            - convert.ScreenToMapViewPoint(new Vector3(0, 0)).x);
        // add on (black bar x position / screenWidth) to shift over, avoiding the black bars
        mapViewWidth += Mathf.Abs(convert.MapViewToScreenPoint(new Vector3(0, 0)).x / Screen.width);
        // set box size
        box.UpdateAnchors(box.left, box.left + mapViewWidth);
        // set text
        field.text = name;
    }

    void Update()
    {
        SetName("HAAAAAAAAAAAAAAAAAAAAAAAAdes");
    }

    public int  GetStringWidth(string message)
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
