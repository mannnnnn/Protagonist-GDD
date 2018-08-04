using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// partial to DialogBehavior class so I don't have to put the statement actions in the same file as the logic
public partial class DialogBehavior
{
    public static readonly Dictionary<string, Vector2> sides = new Dictionary<string, Vector2>()
    {
        { "Center", new Vector2(0, 0) },
        { "Left", new Vector2(0, 0) },
        { "Front Left", new Vector2(0, 0) },
        { "Back Left", new Vector2(0, 0) },
        { "Alt Left", new Vector2(0, 0) },
        { "Right", new Vector2(0, 0) },
        { "Front Right", new Vector2(0, 0) },
        { "Back Right", new Vector2(0, 0) },
        { "Alt Right", new Vector2(0, 0) }
    };

    public GameObject ShowAction(Dictionary<string, object> show)
    {

        GameObject showObj = new GameObject();
        return showObj;
    }
}
