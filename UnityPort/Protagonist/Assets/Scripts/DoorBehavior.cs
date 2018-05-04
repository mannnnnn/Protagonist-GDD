using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehavior : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // get direction from door to center of screen
            Vector2 direction = gameObject.transform.position - ResolutionHandler.GetInstance().MapViewToWorldPoint(new Vector2(0.5f, 0.5f));
            // cardinally normalize the direction vector
            SceneTransitions.Side side = SceneTransitions.ToSide(direction);
            Vector2Int cardinal = SceneTransitions.ToVector2Int(side);
            // go to the given scene in the map
            MapController.GetInstance().Position += new Vector2Int(cardinal.x, -cardinal.y);
            SceneTransition trans = SceneTransitions.Transition<SlideTransition>(new SceneTransitions.Time(0.4f, 0.25f, 0.4f), MapController.GetInstance().map[MapController.GetInstance().Position]);
            trans.side = side;
        }
    }
}
