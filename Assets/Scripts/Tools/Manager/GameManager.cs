using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour,TimerBehaviour{

	// Use this for initialization
	void Start () {
        //this.InitGui();
        this.InitManagers();
        DontDestroyOnLoad(this);
        io.panelManager.CreatePanel(DialogType.Login);
	}

    public void InitGui()
    {
        GameObject gameObject = io.Gui;
        if (gameObject == null)
        {
            GameObject original = Util.LoadPrefab(Const.PanelPrefabDir + "MainUI.prefab");
            gameObject = (UnityEngine.Object.Instantiate(original) as GameObject);
            gameObject.name = "MainUI";
        }
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void OnInitScene()
    {
        
    }

    private void InitManagers()
    {
        Util.Add<PanelManager>(base.gameObject);
        Util.Add<DialogManager>(base.gameObject);
        Util.Add<MusicManager>(base.gameObject);
        Util.Add<MapManager>(base.gameObject);
    }

    public void TimerUpdate()
    {

    }
}
