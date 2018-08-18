using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class SlideTransition : SceneTransition
{
    // output to the player's movement during the transition
    public float playerSpeed = 1f;
    public Vector2 playerMovement;

    // the fraction of the animation taken up by the black slider
    protected float slideTime = 0.5f;

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
        // since the sliding bar only takes part of the transition
        float slideTimer = Mathf.InverseLerp(1 - slideTime, 1, timer);
        // calculate coordinates for sliding in each direction
        Vector2 start = Vector2.zero;
        Vector2 finish = Vector2.zero;
        switch (effectiveSide)
        {
            // top-left to sliding right on the bottom
            case SceneTransitions.Side.LEFT:
                start = new Vector2(0, 0);
                finish = new Vector2(slideTimer, 1);
                break;
            // sliding left on the top to bottom-right
            case SceneTransitions.Side.RIGHT:
                start = new Vector2(1 - slideTimer, 0);
                finish = new Vector2(1, 1);
                break;
            // top-left to sliding down on the right
            case SceneTransitions.Side.UP:
                start = new Vector2(0, 0);
                finish = new Vector2(1, slideTimer);
                break;
            // sliding up on the left to bottom-right
            case SceneTransitions.Side.DOWN:
                start = new Vector2(0, 1 - slideTimer);
                finish = new Vector2(1, 1);
                break;
            default:
                throw new InvalidOperationException("Cannot use a SlideTransition with side being Side.NONE, please set the side to a valid one.");
        }
        // turn [0, 1]x[0, 1] map-view points to screen points
        Vector3 origin = ResolutionHandler.MapViewToScreenPoint(start);
        Vector3 size = ResolutionHandler.MapViewToScreenPoint(finish) - origin;
        // draw
        GUI.DrawTexture(new Rect(origin, size), tex);

        // set player movement
        switch (side)
        {
            case SceneTransitions.Side.LEFT:
                playerMovement = Vector2.left * playerSpeed;
                break;
            case SceneTransitions.Side.RIGHT:
                playerMovement = Vector2.right * playerSpeed;
                break;
            case SceneTransitions.Side.UP:
                playerMovement = Vector2.up * playerSpeed;
                break;
            case SceneTransitions.Side.DOWN:
                playerMovement = Vector2.down * playerSpeed;
                break;
            default:
                throw new InvalidOperationException("Cannot use a SlideTransition with side being Side.NONE, please set the side to a valid one.");
        }
    }

    // when the scene changes, go from hold state to out state
    // and also move the player to its new scene position if the player exists
    protected override void SceneChange(Scene scene, LoadSceneMode mode)
    {
        state = State.OUT;
        // move the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        if (player != null)
        {
            // get the size of the screen in world coordinates
            Vector3 origin = ResolutionHandler.MapViewToWorldPoint(Vector2.zero);
            Vector3 size = ResolutionHandler.MapViewToWorldPoint(Vector2.one) - origin;
            // convert side to a cardinal direction
            Vector3 position = (Vector2)SceneTransitions.ToVector2Int(side);
            // scale by half of the screen size
            // then since the player comes out of the opposite side of the screen, scale by -1
            position.Scale(size * 0.5f * -1f);
            // put the vector at the center of the screen
            position = position + ResolutionHandler.MapViewToWorldPoint(new Vector2(0.5f, 0.5f));
            // set player position
            player.transform.position = position;
        }
    }
}
