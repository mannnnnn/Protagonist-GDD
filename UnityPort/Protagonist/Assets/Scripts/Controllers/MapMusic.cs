using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Controls music on the map using a SyncedMusicPlayer.
 * Maps scene name to music track name, then plays that one.
 */
public class MapMusic : MonoBehaviour {

    SyncedMusicPlayer player;

	// Use this for initialization
	void Start ()
    {
        player = GetComponent<SyncedMusicPlayer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        string scene = SceneManager.GetActiveScene().name;
        GameObject transition = GameObject.FindGameObjectWithTag("SceneTransition");
        if (transition != null)
        {
            scene = transition.GetComponent<SceneTransition>().targetScene;
        }
        string clip = GetClip(scene);
        player.Play(clip, clip == null ? 100f : 1f);
	}

    public string GetClip(string scene)
    {
        switch(scene)
        {
            case "jungle1":
            case "jungle2":
            case "jungle3":
                return "forest1";
            case "jungle4":
            case "jungle5":
            case "jungle6":
                return "forest2";
            case "jungle7":
            case "jungle8":
                return "forest3";
            case "jungle9":
                // do some other stuff based on dialog flags for taut2 and taut3 eventually
                return "taut1";
        }
        return null;
    }
}
