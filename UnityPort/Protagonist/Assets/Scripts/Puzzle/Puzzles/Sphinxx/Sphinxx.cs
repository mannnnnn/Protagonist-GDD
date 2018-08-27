using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sphinxx : MonoBehaviour
{
    public float MaxHP = 100f;
    public float HP = 100f;
    List<Collider2D> hitboxes;
    // we change the head animation when it takes damage
    Animator head;

	void Start ()
    {
        hitboxes = GetComponent<SphinxxHitbox>().GetHitboxes().ToList();
        head = transform.Find("Body").Find("Head").GetComponent<Animator>();
    }

    // spell hits call this
    public void Hit(Vector2 pos, float damage)
    {
        Ray ray = new Ray(new Vector3(pos.x, pos.y, Camera.main.transform.position.z), Vector3.forward);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("Default"));
        // only take first hit collider hit
        Collider2D col = null;
        foreach (RaycastHit2D hit in hits)
        {
            if (hitboxes.Contains(hit.collider))
            {
                col = hit.collider;
                break;
            }
        }
        if (col != null)
        {
            Damage(damage * col.gameObject.GetComponent<SphinxxHitbox>().damage);
            if (col.gameObject != head.gameObject)
            {
                head.SetTrigger("Oof");
            }
            else
            {
                head.ResetTrigger("Oof");
            }
            col.GetComponent<Animator>().SetTrigger("Damage");
        }
    }
    void Damage(float damage)
    {
        HP -= damage;
    }
}