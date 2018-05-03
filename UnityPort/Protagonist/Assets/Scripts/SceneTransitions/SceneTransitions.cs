using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SceneTransitions
{
    // holds durations for the three phases of a scene transition
    public struct Time
    {
        public float inDuration;
        public float holdDuration;
        public float outDuration;
        public Time(float inDuration, float holdDuration, float outDuration)
        {
            this.inDuration = inDuration;
            this.holdDuration = holdDuration;
            this.outDuration = outDuration;
        }
        public Time(float duration) : this(duration, 0, duration)
        {
        }
    }

    // holds cardinal directions used by some transitions
    public enum Side
    {
        NONE, LEFT, RIGHT, UP, DOWN
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

    // creates a scene transition to a given scene, using the SceneTransition of type T.
    // if target scene is null, then just plays a scene transition with no objective.
    public static SceneTransition Transition<T>(Time time, string targetScene) where T : SceneTransition
    {
        GameObject obj = new GameObject();
        SceneTransition trans = obj.AddComponent<T>();
        obj.name = "Scene Transition " + typeof(T).FullName;
        trans.inDuration = time.inDuration;
        trans.holdDuration = time.holdDuration;
        trans.outDuration = time.outDuration;
        trans.targetScene = targetScene;
        return trans;
    }
}
