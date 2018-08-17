using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Start a Puzzle 
 */
public class Puzzles : MonoBehaviour
{
    public List<KeyedPrefab> puzzles;
    static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (KeyedPrefab prefab in puzzles)
        {
            Prefabs[prefab.name] = prefab.prefab;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartPuzzle("puzzle1", "puzzle1");
        }
    }

    public static PuzzleBehaviorBase StartPuzzle(string puzzle, string scene)
    {
        if (!Prefabs.ContainsKey(puzzle))
        {
            throw new InvalidOperationException("Puzzle '" + puzzle + "' is not registered in Puzzles.");
        }
        GameObject puzzleObj = Instantiate(Prefabs[puzzle]);
        PuzzleBehaviorBase puzzleBehavior = puzzleObj.GetComponent<PuzzleBehaviorBase>();
        if (puzzleBehavior == null)
        {
            throw new InvalidOperationException("Puzzle prefab '" + puzzle + "' has no PuzzleBehavior component.");
        }
        puzzleBehavior.Initialize(scene);
        return puzzleBehavior;
    }
}
