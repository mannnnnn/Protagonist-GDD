using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SadSpell : ProjectileSpell
{
    public GameObject hitEffect;
    public GameObject smokeEffect;

    ParticlesAttached ps;
    protected override void Initialize()
    {
        Vector2 pos = ScreenResolution.MapViewToWorldPoint(new Vector2(0.4f, 0.2f));
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        // create smoke obj
        ps = Instantiate(smokeEffect, transform.position, transform.rotation).GetComponent<ParticlesAttached>();
        ps.Initialize(this.gameObject);
    }

    protected override void StartMoving()
    {
        ps.Play();
    }

    protected override void FinishMoving()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        ps.Destroy();
        Destroy(gameObject);
    }
}
