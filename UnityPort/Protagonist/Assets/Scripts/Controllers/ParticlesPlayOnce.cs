using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Plays particles until none exist, then destroys itself.
 * Make sure particles will stop existing at some point by emitting 0 and only using a burst,
 * then setting the duration to be obscenely long so that it doesn't burst again before the first is done.
 */
public class ParticlesPlayOnce : MonoBehaviour
{
    ParticleSystem[] ps;
	// Use this for initialization
	void Start () {
        ps = GetComponentsInChildren<ParticleSystem>();
        foreach (var p in ps)
        {
            p.Play();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        // play until not alive
        bool alive = false;
        foreach (var p in ps)
        {
            if (p.particleCount > 0)
            {
                alive = true;
            }
        }
        if (!alive)
        {
            Destroy(gameObject);
        }
    }
}
