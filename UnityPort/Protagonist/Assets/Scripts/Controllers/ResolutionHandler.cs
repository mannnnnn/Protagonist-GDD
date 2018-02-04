using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour {

    //Scale on (0, 1] of default window's width size (fraction of the screen)
    public float DEFAULT_RESOLUTION_SCALE;

    //data about background texture
    GameObject roomBackgroud;
    SpriteRenderer mapTexture;

    //dimenstions of mapTexture in Unity Units
    Vector2 MapTextureDimensions
    {
        get
        {
            return new Vector2(
                mapTexture.GetComponent<Renderer>().bounds.size.x,
                mapTexture.GetComponent<Renderer>().bounds.size.y
            );
        }
    }

    void Start()
    {
        roomBackgroud = GameObject.FindGameObjectWithTag("RoomBackground");
        mapTexture = roomBackgroud.GetComponent<SpriteRenderer>();

        SetInitialResolution();

        ScaleView();
    }


    void Update()
    {

    }

    /**
     * Toffles resolution between initial windowed resolution and fullscreen.
     */
    public void ToggleFullScreen()
    {
        if (Screen.fullScreen)
        {
            SetInitialResolution();
        }
        else
        {
            Resolution fullScreenResolution = GetOptimumFullscreenResolution();
            Screen.SetResolution(fullScreenResolution.width, fullScreenResolution.height, true);

            Debug.Log("Set fullscreen res: " + fullScreenResolution);
        }
    }

    /**
     * Sets the game resolution based on screen size and map dimensions.
     */
    void SetInitialResolution()
    {
        //respect initial resolution if user chose full screen
        if (Screen.fullScreen) { return; }

        //get resolution of initial screen fraction
        //TODO: do compatibility checks
        int windowHeight = (int)(DEFAULT_RESOLUTION_SCALE * Screen.currentResolution.height);
        int windowWidth = (int)(DEFAULT_RESOLUTION_SCALE * Screen.currentResolution.height * (MapTextureDimensions.x / MapTextureDimensions.y));
        
        //set res
        Screen.SetResolution(windowWidth, windowHeight, false);

        //sometimes this resolution isnt set correctly in the Unity Editor
        Debug.Log("Set Res: " + new Vector2(Screen.width, Screen.height) + ", expected: " + new Vector2(windowWidth, windowHeight)); 
    }

    /**
     * Finds the optimum fullscreen resolution for a given screen size, ppi given the game assets
     */
    Resolution GetOptimumFullscreenResolution()
    {
        //TODO: Get rid of temp implementation and actually do this.

        return Screen.resolutions[Screen.resolutions.Length / 2];
    }

    /**
     * Scales view such that the height of the view matches the height of the mapTexture.
     */
    void ScaleView()
    {
        //get height of mapTexure un Unity units
        float mapTextureHeight = MapTextureDimensions.y;

        /*
         * Scale camera to to mapTexture size--this makes scaling to match full sceme much easier
         * Camera scaling works by setting the "orthographicSize," or the distance from the middle of the view 
         *  to the top in Unity Units.
         * Here we make the height of the camera match the height of the mapTexture, and the width should match beacuse
         *  of how the resolution was set.
         */
        Camera.main.orthographicSize = mapTextureHeight / 2f;
    }
}
