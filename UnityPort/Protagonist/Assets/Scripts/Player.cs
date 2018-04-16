using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //player walk speed in UU
    public float movementSpeed = 0.5f;
    public float walkAnimSpeed = 1f;

    private Animator animator;
    
    //define different animation states of the animator for setting logic
    private enum AnimationState
    {
        PlayerWalkUp,
        PlayerWalkDown,
        PlayerWalkLeft,
        PlayerWalkRight
    }
    //map states to animator triggers
    private Dictionary<string, string> stateTriggerMap;

    private void Start () {
        animator = GetComponent<Animator>();

        stateTriggerMap = new Dictionary<string, string>();
        //TODO: replace with lambda?
        stateTriggerMap.Add("PlayerWalkUp", "WalkUp");
        stateTriggerMap.Add("PlayerWalkDown", "WalkDown");
        stateTriggerMap.Add("PlayerWalkLeft", "WalkLeft");
        stateTriggerMap.Add("PlayerWalkRight", "WalkRight");
	}

    private void Update () {
        Move();
    }


    private void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector3 velocity = movementSpeed * new Vector3(xInput, yInput, 0);

        //TODO: normalize to ensure that diagnonal movement isn't faster
        GetComponent<Rigidbody2D>().MovePosition( transform.position + (velocity * Time.deltaTime)); 

        //change Animator state if needed
        animator.speed = (velocity.magnitude == 0) ? 0f : walkAnimSpeed;

        //find current state based on velocity vector
        //behavior based on what was observed in GML implementation.
        AnimationState currentState;
        if (velocity.x != 0)
        {
            currentState = velocity.x > 0 ? AnimationState.PlayerWalkRight : AnimationState.PlayerWalkLeft;
        }
        else
        {
            currentState = velocity.y > 0 ? AnimationState.PlayerWalkUp : AnimationState.PlayerWalkDown;
        }

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (animator.speed != 0 && !state.IsName(currentState.ToString()))
        {
            animator.SetTrigger(stateTriggerMap[currentState.ToString()]);
        }

        
    }


}
