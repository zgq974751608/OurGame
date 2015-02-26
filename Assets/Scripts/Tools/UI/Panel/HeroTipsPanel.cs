using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ProtoTblConfig;

public class HeroTipsPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Dictionary<int, HeroData> HeroDataDic = Util.GetDic<MsgHeroData, HeroData>();

        string heroModelName = Encoding.Default.GetString(HeroDataDic[Const.heroid].icon);
        uint leadship = HeroDataDic[Const.heroid].leadership;
        uint attr = HeroDataDic[Const.heroid].element;
        uint life = HeroDataDic[Const.heroid].healthBasic;
        uint mana = HeroDataDic[Const.heroid].mana;
        uint attack = HeroDataDic[Const.heroid].attackBasic;
        uint defense = HeroDataDic[Const.heroid].defenceBasic;
        //string combination = HeroDataDic[Const.heroid].combination;
        string name = Encoding.Default.GetString(HeroDataDic[Const.heroid].name);

        UILabel captianlabel = GameObject.Find("Monsterinfo/capacity/Label").GetComponent<UILabel>();
        captianlabel.text = leadship.ToString();
        UILabel attrlabel = GameObject.Find("Monsterinfo/attribute/Label").GetComponent<UILabel>();
        attrlabel.text = attr.ToString();
       
        UILabel uilife = GameObject.Find("Monsterinfo/HP/Label").GetComponent<UILabel>();
        uilife.text = life.ToString();

        UILabel uimana = GameObject.Find("Monsterinfo/MP/Label").GetComponent<UILabel>();
        uimana.text = mana.ToString();

        UILabel uiattack = GameObject.Find("Monsterinfo/attack/Label").GetComponent<UILabel>();
        uiattack.text = attack.ToString();

        UILabel uidefense = GameObject.Find("Monsterinfo/defense/Label").GetComponent<UILabel>();
        uidefense.text = defense.ToString();

        UILabel uiname = GameObject.Find("Frame/heroname/name").GetComponent<UILabel>();
        uiname.text = name;

        UISprite sprite = GameObject.Find("Monsterpicture/Sprite").gameObject.GetComponent<UISprite>();

        if (sprite.atlas)
        {
            sprite.atlas = null;
            //sprite.MakePixelPerfect();
        }

        sprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
        sprite.spriteName = heroModelName;
        //sprite.MakePixelPerfect();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClose()
    {
        Util.CloseDialog(DialogType.HeroTips);
    }
}
