using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles resultion scaling when loading map.
 * Also handles conversion between Map Coordinates and other Unity coordinate systems.
 * Map Coordinates:
 *  Normalized coordinates on domain [0, 1] and range [0, MapHeightToWidthRatio]
 * MapView Coordinates:
 *  The same as map corrdinates, but the range is also normalized to [0, 1]
    Note that this is a non 1:1 transformation and aspect ratios may not be preserved
 */
public class ResolutionHandler : MonoBehaviour {

    //Scale on (0, 1] of default window's hieght scale (fraction of the screen)
    public float DEFAULT_RESOLUTION_SCALE;
    public bool DEBUG_MODE_NOSCALE;

    //data about background texture
    GameObject roomBackground;
    public SpriteRenderer mapSprite;

    public float PixelsPerUU
    {
        get
        {
            return mapSprite.sprite.texture.width / mapSprite.bounds.size.x;
        }
    }

    public float MapHeightToWidthRatio
    {
        get
        {
            return MapDimensions.y / MapDimensions.x;
        }
    }

    //dimensions of mapTexture in Unity Units
    public Vector2 MapDimensions
    {
        get
        {
            return new Vector2(
                mapSprite.bounds.size.x,
                mapSprite.bounds.size.y
            );
        }
    }
    // center of the background
    public Vector2 MapCenter
    {
        get
        {
            return new Vector2(
                mapSprite.bounds.center.x,
                mapSprite.bounds.center.y
            );
        }
    }

    void Awake()
    {
        instance = this;
        roomBackground = GameObject.FindGameObjectWithTag("RoomBackground");
        mapSprite = roomBackground.GetComponent<SpriteRenderer>();

        PositionCamera();

        if (!DEBUG_MODE_NOSCALE) {
            SetInitialResolution();
        }

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

    //Conversion from World Coordinates (Unity Units)
    public Vector3 MapToWorldPoint(Vector3 point)
    {
        float newX = Mathf.Lerp(-MapDimensions.x / 2f, MapDimensions.x / 2f, point.x) + MapCenter.x;
        float newY = Mathf.Lerp(-MapDimensions.y / 2f, MapDimensions.y / 2f, point.y / MapHeightToWidthRatio) + MapCenter.y;
        return new Vector3(newX, newY, point.z);
    }

    public Vector3 WorldToMapPoint(Vector3 point)
    {
        float newX = ((point.x - MapCenter.x) / MapDimensions.x) + 0.5f;
        float newY = Mathf.Lerp(0, MapHeightToWidthRatio, ((point.y - MapCenter.y) / MapDimensions.y) + 0.5f);
        return new Vector3(newX, newY, point.z);
    }


    public Vector3 WorldToMapViewPoint(Vector3 point)
    {
        Vector3 mapCoords = WorldToMapPoint(point);
        mapCoords.y /= MapHeightToWidthRatio;
        return mapCoords;
    }

    public Vector3 MapViewToWorldPoint(Vector3 point)
    {
        point.y *= MapHeightToWidthRatio; //translate to Map Coords
        return MapToWorldPoint(point);
    }
      
    //Conversions for screen (pixel) coords
    public Vector3 ScreenToMapPoint(Vector3 point)
    {
        return WorldToMapPoint(Camera.main.ScreenToWorldPoint(point));
    }

    public Vector3 MapToScreenPoint(Vector3 point)
    {
        return Camera.main.WorldToScreenPoint(MapToWorldPoint(point));
    }

    public Vector3 ScreenToMapViewPoint(Vector3 point)
    {
        return WorldToMapViewPoint(Camera.main.ScreenToWorldPoint(point));
    }

    public Vector3 MapViewToScreenPoint(Vector3 point)
    {
        return Camera.main.WorldToScreenPoint(MapViewToWorldPoint(point));
    }

    //Conversions for Viewport coordinates

    public Vector3 ViewportToMapPoint(Vector3 point)
    {
        return WorldToMapPoint(Camera.main.ViewportToWorldPoint(point));
    }

    public Vector3 MapToViewportPoint(Vector3 point)
    {
        return Camera.main.WorldToViewportPoint(MapToWorldPoint(point));
    }

    public Vector3 ViewportToMapViewPoint(Vector3 point)
    {
        return WorldToMapViewPoint(Camera.main.ViewportToWorldPoint(point));
    }
    
    public Vector3 MapViewToViewportPoint(Vector3 point)
    {
        return Camera.main.WorldToViewportPoint(MapViewToWorldPoint(point));
    }


    //PRIVATE METHODS

    /////////////////////Fullscreen/windowed resolution helper methods//////////////////////////////////////////////////////////////

    private void PositionCamera()
    {
        Camera.main.transform.position = new Vector3(mapSprite.transform.position.x, mapSprite.transform.position.y, Camera.main.transform.position.z);
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



    //////////////////////////////Singleton Nonsense///////////////////////////////////////////////////////////////////////

    //I guess this is a singleton now
    //yaaay for good code style
    private static ResolutionHandler instance;

    public static ResolutionHandler GetInstance()
    {
        if (instance == null)
        {
            throw new InvalidOperationException("There are no objects in the room with the ResolutionHandler behavior.");
        }
        return instance;
    }
}
