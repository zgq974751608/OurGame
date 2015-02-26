using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public MapInfo map;

    public string mapName
    {
        get
        {
            return this.map.name;
        }
    }
    public void LoadMap(string mapName)
    {
        this.map = new MapInfo(mapName);
    }

}
