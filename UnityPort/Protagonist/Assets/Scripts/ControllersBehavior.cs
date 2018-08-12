using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersBehavior : MonoBehaviour {

    static ControllersBehavior instance;

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
