using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ProtoTblConfig;

public class MonsterTipsPanel : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Dictionary<int, MonsterData> MonsterDataDic = Util.GetDic<MsgMonsterData, MonsterData>();
        string icon = Encoding.Default.GetString(MonsterDataDic[Const.monsterid].icon);
        string name = Encoding.Default.GetString(MonsterDataDic[Const.monsterid].name);

        UILabel uiname = GameObject.Find("Monsterpicture/Label").GetComponent<UILabel>();
        uiname.text = name;

        UISprite sprite = GameObject.Find("Monsterpicture/Sprite").GetComponent<UISprite>();

        if (sprite.atlas)
        {
            sprite.atlas = null;
            //sprite.MakePixelPerfect();
        }

        sprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
        sprite.spriteName = icon;
        //sprite.MakePixelPerfect();
       

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClose()
    {
        Util.CloseDialog(DialogType.MonsterTips);
    }
}
