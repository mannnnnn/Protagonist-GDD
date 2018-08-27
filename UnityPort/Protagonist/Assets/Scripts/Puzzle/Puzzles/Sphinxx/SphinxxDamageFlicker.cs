using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxxDamageFlicker : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;

    public float min = 0.4f;
    public float max = 0.7f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // flicker when 
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Oof"))
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Random.Range(min, max));
        }
        else
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
    }
}