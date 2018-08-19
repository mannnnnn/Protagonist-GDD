using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Particle emitter that follows some given game object.
 * This is necessary, and it can't be a child, because destroying the parent destroys the child,
 * which causes all of the particles to instantly disappear.
 */
public class ParticlesAttached : MonoBehaviour
{
    public Vector3 offset;

    GameObject parent;
    public void Initialize(GameObject parent, Vector3 offset = default(Vector3))
    {
        this.parent = parent;
    }

    bool destroy = false;
    ParticleSystem[] ps;
	// Use this for initialization
	void Start ()
    {
        ps = GetComponentsInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (parent != null)
        {
            transform.position = parent.transform.position + offset;
        }
        // destroy when no more particles exist when marked to destroy
        if (destroy)
        {
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

    public void Play()
    {
        foreach (var p in ps)
        {
            p.Play();
        }
    }
    public void Stop()
    {
        foreach (var p in ps)
        {
            p.Stop();
        }
    }

    // set this to be destroyed when all particles run out
    public void Destroy()
    {
        Stop();
        destroy = true;
    }
}
