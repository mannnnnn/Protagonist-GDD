using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Parent object of all of the controllers.
 * Controllers are the persistent singleton game objects.
 * That is why this game object is persistent and any clone that appears is destroyed.
 */
public class Controllers : MonoBehaviour {

    static Controllers instance;

	// only one may exist, and it exists persistently
	void Awake () {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
}
