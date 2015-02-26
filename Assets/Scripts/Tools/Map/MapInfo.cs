using System;
using System.Collections;
using UnityEngine;

public class MapInfo
{
    public string name;
    public MapType type;
    public string backsoundName = string.Empty;

    public MapInfo(string mapName)
	{
		this.name = mapName;
		this.LoadMapFile(this.name);
	}

    public void LoadMapFile(string mapName)
    {
        IniFile ini = new IniFile(Const.MapDataDir + mapName);
    }
}
