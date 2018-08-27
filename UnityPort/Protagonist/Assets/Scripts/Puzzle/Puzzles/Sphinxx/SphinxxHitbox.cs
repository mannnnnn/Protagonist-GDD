using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxxHitbox : MonoBehaviour
{
    public float damage = 1f;
    List<SphinxxHitbox> children;
    Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        children = new List<SphinxxHitbox>(GetComponentsInChildren<SphinxxHitbox>());
        children.Remove(this);
    }

    // get this hitbox and all child hitboxes (recursively)
    public IEnumerable<Collider2D> GetHitboxes()
    {
        if (col != null)
        {
            yield return col;
        }
        foreach (SphinxxHitbox child in children)
        {
            foreach (Collider2D hitbox in child.GetHitboxes())
            {
                yield return hitbox;
            }
        }
    }
}