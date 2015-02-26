using UnityEngine;
using System.Collections;

public class SummonTipsPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClose()
    {
        Util.CloseDialog(DialogType.SummonTips);
    }
}
