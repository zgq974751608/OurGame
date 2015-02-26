using UnityEngine;
using System;
using System.Text;

public class io : MonoBehaviour {

    private static GameObject _manager;
    private static GameManager _gameManager;
    private static MapManager _mapManager;
    private static PanelManager _panelManager;
    private static DialogManager _dialogManager;
    private static MusicManager _musicManager;

    private static UIContainer _container;

    public static GameObject manager
    {
        get
        {
            if (io._manager == null)
            {
                io._manager = GameObject.FindWithTag("GameManager");
            }
            return io._manager;
        }
    }

    public static GameManager gameManager
    {
        get
        {
            if (io._gameManager == null)
            {
                io._gameManager = io.manager.GetComponent<GameManager>();
            }
            return io._gameManager;
        }
    }

    public static MapManager mapManager
    {
        get
        {
            if (io._mapManager == null)
            {
                io._mapManager = io.manager.GetComponent<MapManager>();
            }
            return io._mapManager;
        }
    }

    public static PanelManager panelManager
    {
        get
        {
            if (io._panelManager == null)
            {
                io._panelManager = io.manager.GetComponent<PanelManager>();
            }
            return io._panelManager;
        }
    }

    public static GameObject Gui
    {
        get
        {
            return GameObject.FindWithTag("GUI");
        }
    }

    public static UIContainer container
    {
        get
        {
            if (io._container == null)
            {
                io._container = io.Gui.GetComponent<UIContainer>();
            }
            return io._container;
        }
    }

    public static DialogManager dialogManager
    {
        get
        {
            if (io._dialogManager == null)
            {
                io._dialogManager = io.manager.GetComponent<DialogManager>();
            }

            return io._dialogManager;
        }
    }

    public static MusicManager musicManager
    {
        get
        {
            if (io._musicManager == null)
            {
                io._musicManager = io.manager.GetComponent<MusicManager>();
            }
            return io._musicManager;
        }
    }

    public static string f(string format, params object[] args)
    {
        StringBuilder stringBuilder = new StringBuilder();
        return stringBuilder.AppendFormat(format, args).ToString();
    }

    public static string c(params object[] args)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < args.Length; i++)
        {
            stringBuilder.Append(args[i].ToString());
        }
        return stringBuilder.ToString();
    }
}
