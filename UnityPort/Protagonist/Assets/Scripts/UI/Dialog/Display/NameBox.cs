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
        //TODO: box.clampX = true;

        text = GetComponentInChildren<Text>();
        field = GetComponent<InputField>();
    }
	
	// Set name and scale namebox to size
	public float SetName(string name)
    {
        float px = Utilities.GetStringWidth(name, text.font, text.fontSize);
        //TODO: box.size = new Vector2(px, box.size.y);
        //TODO: return box.size.x;
        return 0;
    }

    void Update()
    {

    }
}
