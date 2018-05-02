using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneTransition t = SceneTransitions.Transition<SlideTransition>(new SceneTransitions.Time(0.4f, 0.25f, 0.4f), sceneName);
            t.side = SceneTransitions.Side.UP;
            if (SceneManager.GetActiveScene().name == "test2")
            {
                t.side = SceneTransitions.Side.DOWN;
            }
        }
    }
}
