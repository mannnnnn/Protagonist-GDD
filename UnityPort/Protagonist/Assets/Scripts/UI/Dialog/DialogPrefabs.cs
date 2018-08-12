using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is accessed through the Inspector.
 * In the inspector, you can add entries like ("Hades", DialogHades) to the prefabs List to map the sprite "Hades" to the DialogHades prefab.
 * You can also do this with menu prefabs, with the menus List.
 * See the DialogSystem prefab -> DialogPrefabs component in the inspector.
 */
public class DialogPrefabs : MonoBehaviour
{
    public List<KeyedPrefab> prefabs;
    public List<KeyedPrefab> menus;

    // populate the dictionaries
    public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();
    public static Dictionary<string, GameObject> Menus = new Dictionary<string, GameObject>();
    void Awake()
    {
        foreach (KeyedPrefab prefab in prefabs)
        {
            Prefabs[prefab.name] = prefab.prefab;
        }
        foreach (KeyedPrefab menu in menus)
        {
            Menus[menu.name] = menu.prefab;
        }
    }
}

[Serializable]
public class KeyedPrefab
{
    public string name;
    public GameObject prefab;
}