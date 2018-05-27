using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    //player walk speed in UU
    public float movementSpeed = 2f;
    public float walkAnimSpeed = 1f;

    private Animator animator;

    // rigidbody2D component
    private Rigidbody2D rb;

    // for debug purposes, don't move on the first few frames
    // while loading in, the game tends to be very framey, leading to movement jumps across walls
    int ready = 3;

    private void Start ()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update ()
    {
        if (ready <= 0)
        {
            Move();
        }
        else
        {
            ready--;
        }
    }

    private Vector2 GetInputVelocity()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("SceneTransition");
        if (obj != null)
        {
            SlideTransition slide = obj.GetComponent<SlideTransition>();
            if (slide != null)
            {
                return slide.playerMovement;
            }
        }
        // if player is in control
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        return movementSpeed * new Vector2(xInput, yInput);
    }

    private void Move()
    {
        // get input
        Vector2 velocity = GetInputVelocity();

        // move the player
        rb.MovePosition(rb.position + (velocity * Time.deltaTime));

        //change Animator state if needed
        string state = "Idle";
        if (velocity.magnitude > movementSpeed * 0.1f)
        {
            // move if not idle
            state = "Move";
        }
        
        // take cardinal vector of velocity, and plug into Animator blend tree
        Vector2 cardinal = Vector2.zero;
        if (Mathf.Abs(velocity.x) >= Mathf.Abs(velocity.y))
        {
            cardinal = new Vector2(Math.Sign(velocity.x), 0);
        }
        else
        {
            cardinal = new Vector2(0, Math.Sign(velocity.y));
        }
        if (cardinal != Vector2.zero)
        {
            animator.SetFloat("VelocityX", cardinal.x);
            animator.SetFloat("VelocityY", cardinal.y);
        }

        // set animator move/idle state
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName(state))
        {
            animator.SetTrigger(state);
        }
    }
}
