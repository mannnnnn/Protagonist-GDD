using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPosition : SaveLoadTarget
{
    static MapPosition instance = new MapPosition();
    public Map map { get; private set; }
    public Vector2Int Position;

    public MapPosition()
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

    public static MapPosition GetInstance()
    {
        return instance;
    }

    public object GetSaveData()
    {
        return Position;
    }
    public void LoadSaveData(object save)
    {
        Position = (Vector2Int)save;
    }
}