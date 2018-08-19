using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// very similar to a normal PuzzleLetter, except it must output to the SyntaxError puzzle when it is hit
public class SyntaxErrorLetter : PuzzleLetter
{
    Vector2 velocity;
    bool falling = false;
    float gravity = 10f;

    public bool correct { get; private set; }
    public void Initialize(string letter, Vector3 pos, Vector2 size, bool correct)
    {
        Initialize(letter, pos, size);
        this.correct = correct;
    }

    protected override void Update()
    {
        base.Update();
        if (falling)
        {
            transform.position = transform.position + (Vector3)velocity * GameTime.deltaTime;
            velocity = new Vector2(velocity.x, velocity.y - gravity * GameTime.deltaTime);
        }
    }

    public bool Contains(Vector2 pos)
    {
        Rect rect = new Rect(transform.position - sr.bounds.extents, sr.bounds.extents * 2f);
        Debug.Log(rect);
        return rect.Contains(pos);
    }

    public void SetAlpha(float alpha)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }

    public void Fall(Vector2 velocity)
    {
        falling = true;
        this.velocity = velocity;
    }
}