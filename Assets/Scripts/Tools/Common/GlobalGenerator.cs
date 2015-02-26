using UnityEngine;
using System.Collections;

public class GlobalGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.InitGameMangager();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitGameMangager()
    {
        GameObject gameObject = io.manager;
        if (gameObject == null)
        {
            GameObject original = (GameObject)Resources.Load(Const.GamePrefabDir + "GameManager");
            gameObject = (UnityEngine.Object.Instantiate(original) as GameObject);
            gameObject.name = "GameManager";
        }

        gameObject.GetComponent<GameManager>().OnInitScene();
    }
}
