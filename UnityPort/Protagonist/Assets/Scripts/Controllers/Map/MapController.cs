using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController
{
    static MapController instance = new MapController();
    public Map map { get; private set; }
    public Vector2Int Position;

    public MapController()
    {
        instance = this;
        map = MapScenes.JungleMap;
        // scan grid to find current position
        Position = new Vector2Int(2, 2);
        for (int i = 0; i < map.Width; i++)
        {
            for (int j = 0; j < map.Height; j++)
            {
                if (map[i, j] == SceneManager.GetActiveScene().name)
                {
                    Position = new Vector2Int(i, j);
                }
            }
        }
	}

    public static MapController GetInstance()
    {
        return instance;
    }
}