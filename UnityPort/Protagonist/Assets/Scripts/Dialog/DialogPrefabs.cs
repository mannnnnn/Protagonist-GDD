using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPrefabs : MonoBehaviour
{
    public List<DialogPrefab> prefabs;
    [Serializable]
    public class DialogPrefab
    {
        public string name;
        public GameObject prefab;
    }

    // populate the dictionary
    public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();
    void Start()
    {
        foreach (DialogPrefab prefab in prefabs)
        {
            Prefabs[prefab.name] = prefab.prefab;
        }
    }
}
