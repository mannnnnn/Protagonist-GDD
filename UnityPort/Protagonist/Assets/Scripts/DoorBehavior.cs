using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Handles doors, which take players from one scene to another in a map
 */
public class DoorBehavior : MonoBehaviour
{
    bool active;
    void Start()
    {
        active = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // make sure collided with a player, this is active, and no SceneTransitions exist
        if (collision.tag == "Player" && active && GameObject.FindGameObjectWithTag("SceneTransition") == null)
        {
            // can only activate once
            active = false;
            // get direction from door to center of screen
            Vector2 direction = gameObject.transform.position - ResolutionHandler.MapViewToWorldPoint(new Vector2(0.5f, 0.5f));
            // cardinally normalize the direction vector
            SceneTransitions.Side side = SceneTransitions.ToSide(direction);
            Vector2Int cardinal = SceneTransitions.ToVector2Int(side);
            // go to the given scene in the map
            MapController.GetInstance().Position += new Vector2Int(cardinal.x, -cardinal.y);
            SceneTransition trans = SceneTransitions.Transition("Slide", new TransitionTime(1f, 0.25f, 1f),
                MapController.GetInstance().map[MapController.GetInstance().Position]);
            trans.side = side;
        }
    }
}
