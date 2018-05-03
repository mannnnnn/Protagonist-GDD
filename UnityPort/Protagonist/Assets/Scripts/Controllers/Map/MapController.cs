using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController
{
    static MapController instance = new MapController();
    public Map map { get; private set; }
    public Vector2Int Position;

    public MapController()
    {
        instance = this;
        map = MapScenes.JungleMap;
        Position = new Vector2Int(2, 2);
	}

    public static MapController GetInstance()
    {
        return instance;
    }
}