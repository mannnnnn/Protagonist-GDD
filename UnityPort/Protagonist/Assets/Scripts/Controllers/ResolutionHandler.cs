using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * ResolutionHandler converts the entire screen into a scaled screen with the black bars.
 * The screen is scaled to the size of the sprite with tag "Background".
 * 
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

    public static float PixelsPerUU
    {
        get
        {
            return GetInstance().mapSprite.sprite.texture.width / GetInstance().mapSprite.bounds.size.x;
        }
    }

    public static float MapHeightToWidthRatio
    {
        get
        {
            return MapDimensions.y / MapDimensions.x;
        }
    }

    //dimensions of mapTexture in Unity Units
    public static Vector2 MapDimensions
    {
        get
        {
            return new Vector2(
                GetInstance().mapSprite.bounds.size.x,
                GetInstance().mapSprite.bounds.size.y
            );
        }
    }
    // center of the background
    public static Vector2 MapCenter
    {
        get
        {
            return new Vector2(
                GetInstance().mapSprite.bounds.center.x,
                GetInstance().mapSprite.bounds.center.y
            );
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        Initialize();
        SceneManager.sceneLoaded += SceneLoaded;
    }
    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Initialize();
    }
    private void Initialize()
    {
        instance = this;
        roomBackground = GameObject.FindGameObjectWithTag("RoomBackground");
        mapSprite = roomBackground.GetComponent<SpriteRenderer>();

        PositionCamera();

        if (!DEBUG_MODE_NOSCALE)
        {
            SetInitialResolution();
        }

        ScaleView();
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        CoverBlackBars();
    }

    // texture that covers the black bars
    // used in CoverBlackBars()
    private Texture2D black;
    // used to cover the black bars with actual black
    protected void CoverBlackBars()
    {
        // set the GUI drawing color to have full alpha
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1);
        // init black texture to cover the side bars
        if (black == null)
        {
            black = new Texture2D(1, 1);
            black.SetPixel(0, 0, Color.black);
            black.Apply();
        }
        // draw the 4 possible black bars:
        Vector3 origin;
        Vector3 size;

        // left bar is Screen(0, 0) to MapView(0, 1)
        origin = Vector2.zero;
        size = MapViewToScreenPoint(new Vector2(0, 1)) - origin;
        GUI.DrawTexture(new Rect(origin, size), black);

        // right bar is MapView(1, 0) to Screen(1, 1)
        origin = MapViewToScreenPoint(new Vector2(1, 0));
        size = new Vector3(Screen.width, Screen.height) - origin;
        GUI.DrawTexture(new Rect(origin, size), black);

        // top bar is Screen(0, 0) to MapView(1, 0)
        origin = Vector2.zero;
        size = MapViewToScreenPoint(new Vector2(1, 0)) - origin;
        GUI.DrawTexture(new Rect(origin, size), black);

        // bottom bar is MapView(0, 1) to Screen(1, 1)
        origin = MapViewToScreenPoint(new Vector2(0, 1));
        size = new Vector3(Screen.width, Screen.height) - origin;
        GUI.DrawTexture(new Rect(origin, size), black);
    }

    /**
     * Toggles resolution between initial windowed resolution and fullscreen.
     */
    public static void ToggleFullScreen()
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
    public static Vector3 MapToWorldPoint(Vector3 point)
    {
        float newX = Mathf.Lerp(-MapDimensions.x / 2f, MapDimensions.x / 2f, point.x) + MapCenter.x;
        float newY = Mathf.Lerp(-MapDimensions.y / 2f, MapDimensions.y / 2f, point.y / MapHeightToWidthRatio) + MapCenter.y;
        return new Vector3(newX, newY, point.z);
    }

    public static Vector3 WorldToMapPoint(Vector3 point)
    {
        float newX = ((point.x - MapCenter.x) / MapDimensions.x) + 0.5f;
        float newY = Mathf.Lerp(0, MapHeightToWidthRatio, ((point.y - MapCenter.y) / MapDimensions.y) + 0.5f);
        return new Vector3(newX, newY, point.z);
    }


    public static Vector3 WorldToMapViewPoint(Vector3 point)
    {
        Vector3 mapCoords = WorldToMapPoint(point);
        mapCoords.y /= MapHeightToWidthRatio;
        return mapCoords;
    }

    public static Vector3 MapViewToWorldPoint(Vector3 point)
    {
        point.y *= MapHeightToWidthRatio; //translate to Map Coords
        return MapToWorldPoint(point);
    }
      
    //Conversions for screen (pixel) coords
    public static Vector3 ScreenToMapPoint(Vector3 point)
    {
        return WorldToMapPoint(Camera.main.ScreenToWorldPoint(point));
    }

    public static Vector3 MapToScreenPoint(Vector3 point)
    {
        return Camera.main.WorldToScreenPoint(MapToWorldPoint(point));
    }

    public static Vector3 ScreenToMapViewPoint(Vector3 point)
    {
        return WorldToMapViewPoint(Camera.main.ScreenToWorldPoint(point));
    }

    public static Vector3 MapViewToScreenPoint(Vector3 point)
    {
        return Camera.main.WorldToScreenPoint(MapViewToWorldPoint(point));
    }

    //Conversions for Viewport coordinates

    public static Vector3 ViewportToMapPoint(Vector3 point)
    {
        return WorldToMapPoint(Camera.main.ViewportToWorldPoint(point));
    }

    public static Vector3 MapToViewportPoint(Vector3 point)
    {
        return Camera.main.WorldToViewportPoint(MapToWorldPoint(point));
    }

    public static Vector3 ViewportToMapViewPoint(Vector3 point)
    {
        return WorldToMapViewPoint(Camera.main.ViewportToWorldPoint(point));
    }
    
    public static Vector3 MapViewToViewportPoint(Vector3 point)
    {
        return Camera.main.WorldToViewportPoint(MapViewToWorldPoint(point));
    }


    //PRIVATE METHODS

    /////////////////////Fullscreen/windowed resolution helper methods//////////////////////////////////////////////////////////////

    private static void PositionCamera()
    {
        Camera.main.transform.position = new Vector3(GetInstance().mapSprite.transform.position.x, GetInstance().mapSprite.transform.position.y, Camera.main.transform.position.z);
    }

    /**
     * Sets the game resolution based on screen size and map dimensions.
     */
    private static void SetInitialResolution()
    {
        //respect initial resolution if user chose full screen
        if (Screen.fullScreen) { return; }

        //get resolution of initial screen fraction
        //TODO: do compatibility checks
        int windowHeight = (int)(GetInstance().DEFAULT_RESOLUTION_SCALE * Screen.currentResolution.height);
        int windowWidth = (int)(GetInstance().DEFAULT_RESOLUTION_SCALE * Screen.currentResolution.height * (MapDimensions.x / MapDimensions.y));

        //set res
        Screen.SetResolution(windowWidth, windowHeight, false);

        //sometimes this resolution isnt set correctly in the Unity Editor
        Debug.Log("Set Res: " + new Vector2(Screen.width, Screen.height) + ", expected: " + new Vector2(windowWidth, windowHeight));
    }

    /**
     * Finds the optimum fullscreen resolution for a given screen size, ppi given the game assets
     */
    private static Resolution GetOptimumFullscreenResolution()
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

    public static bool Ready => instance != null && GetInstance().mapSprite != null;
    public static ResolutionHandler GetInstance()
    {
        if (instance == null)
        {
            throw new InvalidOperationException("There are no objects in the room with the ResolutionHandler behavior.");
        }
        return instance;
    }

    // UI helper methods
    public static Vector2 RectToScreenPoint(RectTransform rect, Vector2 rectPos)
    {
        Rect originalRect = new Rect(rect.rect.position, rect.rect.size);
        Rect screenRect = GetScreenRect(rect);
        float x = FreeLerp(rectPos.x, originalRect.xMin, originalRect.xMax, screenRect.xMin, screenRect.xMax);
        float y = FreeLerp(rectPos.y, originalRect.yMin, originalRect.yMax, screenRect.yMin, screenRect.yMax);
        return new Vector2(x, y);
    }
    public static Vector2 ScreenToRectPoint(RectTransform rect, Vector2 screenPos)
    {
        Rect originalRect = new Rect(rect.rect.position, rect.rect.size);
        Rect screenRect = GetScreenRect(rect);
        float x = FreeLerp(screenPos.x, screenRect.xMin, screenRect.xMax, originalRect.xMin, originalRect.xMax);
        float y = FreeLerp(screenPos.y, screenRect.yMin, screenRect.yMax, originalRect.yMin, originalRect.yMax);
        return new Vector2(x, y);
    }
    public static Rect GetScreenRect(RectTransform rect)
    {
        Vector3[] v = new Vector3[4];
        rect.GetWorldCorners(v);
        var bottomLeft = Camera.main.WorldToScreenPoint(v[0]);
        var topRight = Camera.main.WorldToScreenPoint(v[2]);
        return new Rect(bottomLeft, topRight - bottomLeft);
    }
    public static float FreeLerp(float value, float aMin, float aMax, float bMin, float bMax)
    {
        return (((value - aMin) * ((bMax - bMin) / (aMax - aMin))) + bMin);
    }

    public static Vector2 WorldToCanvasPoint(RectTransform canvas, Camera camera, Vector3 position)
    {
        // viewport is [0, 1]x[0, 1]
        position = camera.WorldToViewportPoint(position);
        // rescale to [0, canvasWidth]x[0, canvasHeight]
        position.x *= canvas.sizeDelta.x;
        position.y *= canvas.sizeDelta.y;
        // adjust for pivot
        position.x -= canvas.sizeDelta.x * canvas.pivot.x;
        position.y -= canvas.sizeDelta.y * canvas.pivot.y;
        return position;
    }

    public static Vector2 CanvasToWorldPoint(RectTransform canvas, Camera camera, Vector3 position)
    {
        // adjust for pivot
        position.x += canvas.sizeDelta.x * canvas.pivot.x;
        position.y += canvas.sizeDelta.y * canvas.pivot.y;
        // rescale to [0, 1]x[0, 1]
        position.x /= canvas.sizeDelta.x;
        position.y /= canvas.sizeDelta.y;
        // viewport is [0, 1]x[0, 1]
        return camera.ViewportToWorldPoint(position);
    }
}
