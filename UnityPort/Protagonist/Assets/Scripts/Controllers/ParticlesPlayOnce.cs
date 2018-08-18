using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
