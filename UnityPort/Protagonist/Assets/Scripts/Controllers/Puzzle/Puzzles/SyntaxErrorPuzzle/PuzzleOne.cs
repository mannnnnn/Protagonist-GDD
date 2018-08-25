using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/**
 * Example class of how to use the SyntaxErrorPuzzle abstract class to create Syntax Error Puzzles easily.
 */
public class PuzzleOne : SyntaxErrorPuzzle
{
    protected override void CreateLines()
    {
        Vector3 pos = ScreenResolution.MapViewToWorldPoint(new Vector2(0f, 0.9f));
        float x = pos.x;
        float y = pos.y;
        const float z = 10;
        y -= Line("HELLO EVERYONE", new Vector3(x + 1f, y, z));
        y -= Line("THIIS IS A TEST", new Vector3(x + 2f, y, z), STANDARD_SIZE, new List<int>{2, 3});
        y -= Line("PLEASE DONT PRESS Z", new Vector3(x + 0.5f, y, z));
    }
    protected override void EndPuzzle()
    {
        SceneTransitions.Transition("Fade", new TransitionTime(1f, 0.3f, 1f), "jungle1");
    }
}