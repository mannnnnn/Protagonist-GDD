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
    public List<DialogPrefab> prefabs;
    public List<DialogPrefab> menus;
    [Serializable]
    public class DialogPrefab
    {
        public string name;
        public GameObject prefab;
    }

    // populate the dictionaries
    public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();
    public static Dictionary<string, GameObject> Menus = new Dictionary<string, GameObject>();
    void Awake()
    {
        foreach (DialogPrefab prefab in prefabs)
        {
            Prefabs[prefab.name] = prefab.prefab;
        }
        foreach (DialogPrefab menu in menus)
        {
            Menus[menu.name] = menu.prefab;
        }
    }
}
