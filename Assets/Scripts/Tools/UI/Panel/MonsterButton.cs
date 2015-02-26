using UnityEngine;
using System.Collections;

public class MonsterButton : BaseButton
{
 
	// Use this for initialization
	void Start () {
	
	}

    public void OnClick()
    {
        UISprite sprite = this.gameObject.GetComponent<UISprite>();
        if (sprite.atlas)
        {
            foreach (var element in ArrangementPanel.monsterdic)
            {
                if (sprite.spriteName == element.Value)
                {
                    Const.monsterid = element.Key;
                    break;
                }
            }

            io.panelManager.CreatePanel(DialogType.MonsterTips);
        } 
 

    }

	// Update is called once per frame
	void Update () {
	
	}
}
