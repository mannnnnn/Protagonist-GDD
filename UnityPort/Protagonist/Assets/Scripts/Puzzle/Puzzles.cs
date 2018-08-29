using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Maps puzzles to puzzle prefabs via the inspector.
 * Puzzles.StartPuzzle is used to use one of these prefabs to create a puzzle instance.
 * Puzzles.CreatePlayer(puzzle) is used by puzzles to create a player. Note that a player must be bound to a puzzle,
 * so you must pass in a puzzle instance when creating a player.
 */
public class Puzzles : MonoBehaviour
{
    // puzzle lookup by name
    public List<KeyedPrefab> puzzles;
    static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();

    // player creation in puzzles
    public GameObject playerPrefab;
    static GameObject player;

    void Awake()
    {
        player = playerPrefab;
        foreach (KeyedPrefab prefab in puzzles)
        {
            Prefabs[prefab.name] = prefab.prefab;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartPuzzle("sphinxx", "puzzle1");
        }
    }

    public static PuzzleBase StartPuzzle(string puzzle, string scene)
    {
        if (!Prefabs.ContainsKey(puzzle))
        {
            throw new InvalidOperationException("Puzzle '" + puzzle + "' is not registered in Puzzles.");
        }
        GameObject puzzleObj = Instantiate(Prefabs[puzzle]);
        PuzzleBase puzzleBehavior = puzzleObj.GetComponent<PuzzleBase>();
        if (puzzleBehavior == null)
        {
            throw new InvalidOperationException("Puzzle prefab '" + puzzle + "' has no PuzzleBehavior component.");
        }
        puzzleBehavior.Initialize(scene);
        return puzzleBehavior;
    }

    public static GameObject CreatePlayer(SpellInteractionTarget target)
    {
        GameObject playerObj = Instantiate(player);
        playerObj.GetComponent<PuzzlePlayer>().Initialize(target);
        return playerObj;
    }
}
