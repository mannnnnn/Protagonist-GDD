using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // register your dialog events here
    public bool Handle(string evt, Dictionary<string, object> args)
    {
        switch (evt)
        {
            case "Hi":
                Debug.Log(args["message"]);
                break;
        }
        return true;
    }
}
