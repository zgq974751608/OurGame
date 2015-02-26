using UnityEngine;
using System.Collections;
using AiWanNet;
using AiWanNet.Core;
using System.Collections.Generic;
using System.Text;
using ProtoTblConfig;
using LitJson;

public class ArrangementPanel : MonoBehaviour {

    public static Dictionary<int, string> spritename = new Dictionary<int, string>();
    private List<string> captianname = new List<string>();
    private GameObject obj;

    public static Dictionary<int, string> monsterdic = new Dictionary<int, string>();

    public static List<string> data = new List<string>();
    void Awake()
    {
        LoadEnemySprite();
        TestLoadSprite();
        LoadHeroSprite();
        LoadCaptainSprite();
        PlayerData.GetInstance().CurrentFightHero.Clear();
    }

    void TestLoadSprite()
    {
//        spritename.Add("Headpic0002.PNG");
//        spritename.Add("Headpic0009.PNG");
//        spritename.Add("Headpic0009.PNG");

        captianname.Add("Headpic0008.PNG");

    }

	// Use this for initialization
	void Start () 
    {
        Util.CloseDialog(DialogType.Duplicate);
       // AWConnection.awnetgate.AddEventListener(AWEvent.S2C_INTOEMBATTLE, OnIntoemBattle);
        CEventDispatcher.GetInstance().AddEventListener(CEventType.GAME_DATA, LoadEnemyGroup);     
	}

    public void OnStart()
    {
        clientmsg.c2s_fightbegin fightbegin = new clientmsg.c2s_fightbegin();
        fightbegin.uaid = Global.uid;
        fightbegin.enemyid = 10101;
        AWConnection.instance.SendProtoBufMsg(fightbegin, AWConnection.awnetgate);
        
    }

    public void  LoadEnemyGroup(CBaseEvent evt)
    {
        DungeonManager.enemyWave.Clear();
        int number = ArrangementPanel.data.Count;

        for (int i = 0; i < number; i++)
        {
            JsonData enemy = JsonMapper.ToObject(ArrangementPanel.data[i].ToString());
            int[] enemys = new int[enemy.Count];

            for (int j = 0; j < enemy.Count; j++)
            {
                enemys[j] = int.Parse(enemy[j].ToString());
            }

            DungeonManager.enemyWave.Add(enemys);
        }

        LoadScene.LoadDungeon(10101);
        Util.CloseDialog(DialogType.Arrangement);

    }
    void LoadEnemySprite()
    {
        monsterdic.Clear();
        Dictionary<int, Dungeon> DungeonTextDic = Util.GetDic<MsgDungeon, Dungeon>();
        Dictionary<int, MonsterData> MonsterDataDic = Util.GetDic<MsgMonsterData, MonsterData>();

        string monstinfo = Encoding.Default.GetString(DungeonTextDic[10101].monsterInfo);

        JsonData data = JsonMapper.ToObject<JsonData>(monstinfo);
        GameObject obj = GameObject.Find("Upside/Right").gameObject;
        int count = obj.transform.childCount;

        for (int i = 0; i < data.Count; i++)
        {
            int id = int.Parse(data[i].ToString());
            
            string icon = Encoding.Default.GetString(MonsterDataDic[id].icon);
            if (!monsterdic.ContainsKey(id))
            {
                monsterdic.Add(id, icon);
            }
            
            for (int j = 0; j < count; j++ )
            {
                UISprite sprite = obj.transform.GetChild(j).FindChild("Sprite").GetComponent<UISprite>();

                if(sprite.atlas == null)
                {
                    sprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
                    sprite.spriteName = icon;
                    //sprite.MakePixelPerfect();
                    break;
                }
            }
            
        }
    }

    void LoadHeroSprite()
    {
        GameObject obj = GameObject.Find("Bottom/Herobag/Grid").gameObject;
        if (spritename.Count > 0)
        {
            int i = 0;
            foreach (var element in spritename)
            {
                string value = element.Value;
                int id = element.Key;

                GameObject spriteobj = Instantiate(Resources.Load("RoleUI/Hero", typeof(GameObject))) as GameObject;

                spriteobj.transform.parent = obj.transform;
                spriteobj.transform.position = new Vector3(80 * i, 0f, 0f);
                spriteobj.name = "Hero";

                UISprite sprite = spriteobj.transform.FindChild("Sprite").GetComponent<UISprite>();
                sprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
                sprite.spriteName = value;
                //sprite.MakePixelPerfect();
                i++;
            }
        }   
    }

    void LoadCaptainSprite()
    {
        GameObject obj = GameObject.Find("Bottom/Captainbag/Grid").gameObject;
        if (obj == null)
        {
            return;
        }
        if (captianname.Count > 0)
        {
            for (int i = 0; i < captianname.Count; i++)
            {
                GameObject captain = Instantiate(Resources.Load("RoleUI/Captain", typeof(GameObject))) as GameObject;
                captain.transform.parent = obj.transform;
                captain.transform.position = new Vector3( 80 * i, 0f, 0f);
                captain.name = "Captain";

                UISprite sprite = captain.transform.FindChild("Sprite").GetComponent<UISprite>();
                sprite.atlas = Resources.Load("HeadPic/HeadPic", typeof(UIAtlas)) as UIAtlas;
                sprite.spriteName = captianname[i];
                //sprite.MakePixelPerfect();
            }
        }
    }

    public void OnClose()
    {
        io.panelManager.CreatePanel(DialogType.Duplicate);
        Util.CloseDialog(DialogType.Arrangement);
    }
}
