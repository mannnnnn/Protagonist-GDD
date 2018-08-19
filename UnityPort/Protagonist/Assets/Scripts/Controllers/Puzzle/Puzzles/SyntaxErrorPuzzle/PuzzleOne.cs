using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PuzzleOne : SyntaxErrorPuzzle
{
    protected override void CreateLines()
    {
        Vector3 pos = ResolutionHandler.MapViewToWorldPoint(new Vector2(0f, 0.9f));
        float x = pos.x;
        float y = pos.y;
        const float z = 10;
        y -= Line("HELLO EVERYONE", new Vector3(x + 1f, y, z));
        y -= Line("THIIS IS A TEST", new Vector3(x + 2f, y, z), STANDARD_SIZE, new List<int>{2, 3});
        y -= Line("PLEASE DONT PRESS Z", new Vector3(x + 0.5f, y, z));
    }
    protected override void EndPuzzle()
    {
        Debug.Log("The end.");
        SceneTransitions.Transition("Fade", new TransitionTime(1f, 0.3f, 1f), "jungle1");
    }
}