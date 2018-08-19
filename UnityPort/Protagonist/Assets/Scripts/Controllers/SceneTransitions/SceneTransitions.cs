using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// holds durations for the three phases of a scene transition
public struct TransitionTime
{
    public float inDuration;
    public float holdDuration;
    public float outDuration;
    public TransitionTime(float inDuration, float holdDuration, float outDuration)
    {
        this.inDuration = inDuration;
        this.holdDuration = holdDuration;
        this.outDuration = outDuration;
    }
    public TransitionTime(float duration) : this(duration, 0, duration)
    {
    }
}

/**
 * Controller that creates scene transitions.
 */
public class SceneTransitions : MonoBehaviour
{
    public List<KeyedPrefab> transitions;
    static readonly Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (KeyedPrefab prefab in transitions)
        {
            Prefabs[prefab.name] = prefab.prefab;
        }
    }

    // holds cardinal directions used by some transitions
    public enum Side
    {
        NONE, LEFT, RIGHT, UP, DOWN
    }

    // creates a scene transition to a given scene.
    // if target scene is null, then just plays a scene transition with no objective.
    public static SceneTransition Transition(string name, TransitionTime time, string targetScene)
    {
        if (!Prefabs.ContainsKey(name))
        {
            throw new InvalidOperationException("Scene Transition '" + name + "' is not registered in the SceneTransitions controller.");
        }
        GameObject obj = Instantiate(Prefabs[name]);
        obj.tag = "SceneTransition";
        SceneTransition trans = obj.GetComponent<SceneTransition>();
        if (trans == null)
        {
            throw new InvalidOperationException("Scene Transition '" + name + "' has no SceneTransition component.");
        }
        trans.inDuration = time.inDuration;
        trans.holdDuration = time.holdDuration;
        trans.outDuration = time.outDuration;
        trans.targetScene = targetScene;
        return trans;
    }


    // converts a Side to a cardinal vector
    public static Vector2Int ToVector2Int(Side side)
    {
        switch (side)
        {
            case Side.LEFT:
                return Vector2Int.left;
            case Side.RIGHT:
                return Vector2Int.right;
            case Side.UP:
                return Vector2Int.up;
            case Side.DOWN:
                return Vector2Int.down;
            default:
                return Vector2Int.zero;
        }
    }

    // converts a world coordinate direction to a Side
    public static Side ToSide(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                return Side.RIGHT;
            }
            else
            {
                return Side.LEFT;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                return Side.UP;
            }
            else
            {
                return Side.DOWN;
            }
        }
    }
}
