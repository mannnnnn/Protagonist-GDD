using UnityEngine;
using System;
using System.Collections;

public class SlideTransition : SceneTransition
{
    // draw sliding rectangle to screen
    void OnGUI()
    {
        // set the GUI drawing color to have the given alpha
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1);
        // init texture if necessary
        if (tex == null)
        {
            tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.black);
            tex.Apply();
        }
        // if state is OUT, then switch sides to the opposite side
        SceneTransitions.Side effectiveSide = side;
        if (state == State.OUT)
        {
            switch(side)
            {
                case SceneTransitions.Side.LEFT:
                    effectiveSide = SceneTransitions.Side.RIGHT;
                    break;
                case SceneTransitions.Side.RIGHT:
                    effectiveSide = SceneTransitions.Side.LEFT;
                    break;
                case SceneTransitions.Side.UP:
                    effectiveSide = SceneTransitions.Side.DOWN;
                    break;
                case SceneTransitions.Side.DOWN:
                    effectiveSide = SceneTransitions.Side.UP;
                    break;
                default:
                    throw new InvalidOperationException("Cannot use a SlideTransition with side being Side.NONE, please set the side to a valid one.");
            }
        }
        // calculate coordinates for sliding in each direction
        Vector2 start = Vector2.zero;
        Vector2 finish = Vector2.zero;
        switch (effectiveSide)
        {
            // top-left to sliding right on the bottom
            case SceneTransitions.Side.LEFT:
                start = new Vector2(0, 0);
                finish = new Vector2(timer, 1);
                break;
            // sliding left on the top to bottom-right
            case SceneTransitions.Side.RIGHT:
                start = new Vector2(1 - timer, 0);
                finish = new Vector2(1, 1);
                break;
            // top-left to sliding down on the right
            case SceneTransitions.Side.UP:
                start = new Vector2(0, 0);
                finish = new Vector2(1, timer);
                break;
            // sliding up on the left to bottom-right
            case SceneTransitions.Side.DOWN:
                start = new Vector2(0, 1 - timer);
                finish = new Vector2(1, 1);
                break;
            default:
                throw new InvalidOperationException("Cannot use a SlideTransition with side being Side.NONE, please set the side to a valid one.");
        }
        // turn [0, 1]x[0, 1] map-view points to screen points
        Vector3 origin = ResolutionHandler.GetInstance().MapViewToScreenPoint(start);
        Vector3 size = ResolutionHandler.GetInstance().MapViewToScreenPoint(finish) - origin;
        // draw
        GUI.DrawTexture(new Rect(origin, size), tex);
    }
}
