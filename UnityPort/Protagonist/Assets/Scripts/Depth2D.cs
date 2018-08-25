using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Sets z depth equal to y position, so that you can go in front of and in back of objects even in a 2D environment.
 */
public class Depth2D : MonoBehaviour
{
    public float offset = 0f;

	// Use this for initialization
	void Start ()
    {
        
	}
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + (offset * transform.localScale.y));
    }
}
