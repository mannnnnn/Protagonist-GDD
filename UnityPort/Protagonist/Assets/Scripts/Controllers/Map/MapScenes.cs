using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * Create 2D grids out of scenes here.
 */
public class MapScenes
{
    // the jungle map
    public static Map JungleMap;
    private static string[,] jungle = new string[3, 4]
          { { "jungle5", "jungle4", "jungle3", "jungle9" },
            { "jungle6",      null, "jungle2",      null },
            { "jungle7", "jungle8", "jungle1",      null } };

    // initializes the Map objects
    static MapScenes()
    {
        JungleMap = new Map(jungle);
    }
}