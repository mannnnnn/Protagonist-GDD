using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// contains data about one connected 2D grid map
public class Map
{
    // the 2D array containing the scenes
    string[,] scenes;

    // convert (y, x) to (x, y)
    public string this[int x, int y]
    {
        get
        {
            return scenes[y, x];
        }
    }
    public string this[Vector2Int pos]
    {
        get
        {
            return this[pos.x, pos.y];
        }
    }

    // get width and height of the map, in number of scenes
    public int Width { get { return scenes.GetLength(1); } }
    public int Height { get { return scenes.GetLength(0); } }

    // creates a Map object given an array of scenes
    public Map(string[,] scenes)
    {
        this.scenes = scenes;
    }
}
