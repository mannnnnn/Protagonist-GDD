using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * The first boss.
 */
public class SphinxxPuzzle : StandardPuzzle
{
    public GameObject sphinxxPrefab;
    GameObject sphinxx;

    protected override void Start()
    {
        base.Start();
        TriMusicPlayer.Get("Tautolodiction").Play();
    }

    protected override void SceneStart()
    {
        sphinxx = Instantiate(sphinxxPrefab);
    }
}
