using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (DialogPrefab menu in prefabs)
        {
            Menus[menu.name] = menu.prefab;
        }
    }
}
