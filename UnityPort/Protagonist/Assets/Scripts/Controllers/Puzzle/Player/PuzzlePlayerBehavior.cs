using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that creates the player's various parts on game startup, such as the hands and cursor.
 */
public class PuzzlePlayerBehavior : MonoBehaviour
{
    public SpellInteractionTarget puzzle { get; private set; }

    public GameObject cursor;
    public GameObject rightHand;
    public GameObject leftHand;

    public void Initialize(SpellInteractionTarget puzzle)
    {
        this.puzzle = puzzle;
    }

    void Start()
    {
        Instantiate(cursor);
    }

    void Update()
    {

    }
}
