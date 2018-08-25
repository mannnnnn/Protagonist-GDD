using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDisplays : MonoBehaviour
{
    public List<KeyedPrefab> dialogDisplays;
    static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();
    
    // needs to be in Awake because Dialog uses it on Start
	void Awake()
    {
		foreach (KeyedPrefab prefab in dialogDisplays)
        {
            Prefabs[prefab.name] = prefab.prefab;
        }
	}

    // Use this to swap to a dialog display and destroy the old one.
    // don't use before Dialog's Awake method is called.
    public static void SwapTo(string name)
    {
        DialogDisplay newDisplay = Create(name);
        MonoBehaviour oldDisplay = Dialog.GetDisplay() as MonoBehaviour;
        if (oldDisplay != null)
        {
            Destroy(oldDisplay.gameObject);
        }
        Dialog.SetDisplay(newDisplay);
    }

    // creates the prefab for a given string name
    private static DialogDisplay Create(string name)
    {
        if (!Prefabs.ContainsKey(name) || Prefabs[name] == null)
        {
            throw new InvalidOperationException("Dialog Display " + name + " is not registered in the DialogDisplays component in the inspector.");
        }
        GameObject displayObj = Instantiate(Prefabs[name], Dialog.GetInstance().transform);
        DialogDisplay display = displayObj.GetComponent<DialogDisplay>();
        if (display == null)
        {
            throw new InvalidOperationException("Dialog Display " + name + " prefab has no DialogDisplay component.");
        }
        return display;
    }
}
