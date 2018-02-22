using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles resultion scaling when loading map.
 * Also handles conversion between Map Coordinates and other Unity coordinate systems.
 * Map Coordinates:
 *  Normalized coordinates on domain [0, 1] and range [0, MapHeightToWidthRatio]
 */
public class ResolutionHandler : MonoBehaviour {

    //Scale on (0, 1] of default window's hieght scale (fraction of the screen)
    public float DEFAULT_RESOLUTION_SCALE;


    //data about background texture
    GameObject roomBackgroud;
    SpriteRenderer mapTexture;

    public float PixelsPerUU
    {
        get
        {
            return mapTexture.sprite.texture.width / mapTexture.GetComponent<Renderer>().bounds.size.x;
        }
    }

    public float MapHeightToWidthRatio
    {
        get
        {
            return MapDimensions.y / MapDimensions.x;
        }
    }

    //dimenstions of mapTexture in Unity Units
    public Vector2 MapDimensions
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

        PositionCamera();

        SetInitialResolution();

        ScaleView();
    }


    void Update()
    {
    }

    /**
     * Toggles resolution between initial windowed resolution and fullscreen.
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

    /////////////////////////////////COORDINATE CONVERSION METHODS//////////////////////////////////////////////////////////////////

    public Vector3 MapToUnityCoords(Vector3 point)
    {
        float newX = Mathf.Lerp(-MapDimensions.x / 2f, MapDimensions.x / 2f, point.x);
        float newY = Mathf.Lerp(-MapDimensions.y / 2f, MapDimensions.y / 2f, point.y / MapHeightToWidthRatio);
        return new Vector3(newX, newY, point.z);
    }

    public Vector3 UnityToMapCoords(Vector3 point)
    {
        Debug.Log("BW: " + GetScaledBarWidth());
        Debug.Log("PPUU: " + PixelsPerUU);
        float newX = (point.x / MapDimensions.x) + 0.5f;
        float newY = Mathf.Lerp(0, MapHeightToWidthRatio, (point.y / MapDimensions.y) + 0.5f);
        return new Vector3(newX, newY, point.z);
    }


    //PRIVATE METHODS

    /////////////////////Fullscreen/windowed resolution helper methods//////////////////////////////////////////////////////////////

    private void PositionCamera()
    {
        Camera.main.transform.position = new Vector3(mapTexture.transform.position.x, mapTexture.transform.position.y, Camera.main.transform.position.z);
    }

    /**
     * Sets the game resolution based on screen size and map dimensions.
     */
    private void SetInitialResolution()
    {
        //respect initial resolution if user chose full screen
        if (Screen.fullScreen) { return; }

        //get resolution of initial screen fraction
        //TODO: do compatibility checks
        int windowHeight = (int)(DEFAULT_RESOLUTION_SCALE * Screen.currentResolution.height);
        int windowWidth = (int)(DEFAULT_RESOLUTION_SCALE * Screen.currentResolution.height * (MapDimensions.x / MapDimensions.y));

        //set res
        Screen.SetResolution(windowWidth, windowHeight, false);

        //sometimes this resolution isnt set correctly in the Unity Editor
        Debug.Log("Set Res: " + new Vector2(Screen.width, Screen.height) + ", expected: " + new Vector2(windowWidth, windowHeight));
    }

    /**
     * Finds the optimum fullscreen resolution for a given screen size, ppi given the game assets
     */
    private Resolution GetOptimumFullscreenResolution()
    {
        //TODO: Get rid of temp implementation and actually do this.

        return Screen.resolutions[Screen.resolutions.Length / 2];
    }

    /**
     * Scales view such that the height of the view matches the height of the mapTexture.
     */
    private void ScaleView()
    {
        //get height of mapTexure un Unity units
        float mapTextureHeight = MapDimensions.y;

        /*
         * Scale camera to to mapTexture size--this makes scaling to match full sceme much easier
         * Camera scaling works by setting the "orthographicSize," or the distance from the middle of the view 
         *  to the top in Unity Units.
         * Here we make the height of the camera match the height of the mapTexture, and the width should match beacuse
         *  of how the resolution was set.
         */
        Camera.main.orthographicSize = mapTextureHeight / 2f;
    }


    /////////////////////////////Coordinate System Helper Methods/////////////////////////////////////////////////////////

    //get the with of the black bars scaled to [0, 1] as a multiplier of the screen width
    private float GetScaledBarWidth()
    {
        return (Screen.width - (MapDimensions.x * PixelsPerUU)) / Screen.width / 2f;
    }


    //////////////////////////////Singleton Nonsense///////////////////////////////////////////////////////////////////////

    //I guess this is a singleton now
    //yaaay for good code style
    private static ResolutionHandler instance;

    public static ResolutionHandler GetInstance()
    {
        if (instance == null)
        {
            instance = new ResolutionHandler();
        }
        return instance;
    }

    //TODO: fix duplicate assignment for instance == null case
    public ResolutionHandler()
    {
        instance = this;
    }
}
