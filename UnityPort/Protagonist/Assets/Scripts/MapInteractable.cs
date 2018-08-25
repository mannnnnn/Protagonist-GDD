using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Objects on the map that you can press SPACE to interact with.
 */
public class MapInteractable : MonoBehaviour
{
    public string file;
    public string label;
    public float distance = 0.5f;

    Collider2D col;
	void Start ()
    {
        col = GetComponent<Collider2D>();
	}

	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 point = col.bounds.ClosestPoint(player.transform.position);
            if (Vector2.Distance(point, player.transform.position) < distance)
            {
                Dialog.RunDialog(file, label);
            }
        }
	}
}
