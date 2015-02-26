using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using Holoville.HOTween.Path;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Plugins.Core;
using System;
using LitJson;

public class SummonPanel : MonoBehaviour {

    public List<Transform> gamelist = new List<Transform>();
    private List<Vector3> vpos = new List<Vector3>();
    private List<Sequence> mySequence = new List<Sequence>();

    public static string posfile = "file/summonpos";
    private GameObject obj;
    public float time = 0.2f;

	// Use this for initialization
	void Start () 
    {
        Util.CloseDialog(DialogType.Main);
        StartCoroutine(LoadModel());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClose()
    {
        Util.CloseDialog(DialogType.Summon);
        io.panelManager.CreatePanel(DialogType.Main);
    }

    void InitSequencePos()
    {
        vpos.Clear();
        JsonData[] data = JsonMapper.ToObject<JsonData[]>(PreLoadConfig.summontext);
        
        int k = 0;
        foreach (JsonData jd in data)
        {
            Vector3 vec = Util.StrToVector3(jd["pos_" + k.ToString()].ToString(), ',');
            vpos.Add(vec);
            k++;
        }

    }

    void LoadSummonData()
    {
        mySequence.Clear();
        for (int i = 0; i < gamelist.Count; i++)
        {
            Sequence sequence = new Sequence(new SequenceParms().TimeScale(time * (gamelist.Count - i + 1)));
            mySequence.Add(sequence);
        }

        for (int i = 0; i < mySequence.Count; i++)
        {
            mySequence[i].Append(HOTween.To(gamelist[i], 1, new TweenParms().Prop("position", vpos[0])));
            mySequence[i].Append(HOTween.To(gamelist[i], 1, new TweenParms().Prop("position", vpos[i + 1])));
        }
    }


    public void OnTenClick()
    {
        if (obj)
            Destroy(obj);

        InitSequencePos();
        LoadSummonData();

        for (int i = 0; i < mySequence.Count; i++)
        {
            mySequence[i].PlayForward();
        }
    }

    public void OnOneClick()
    {
        for (int i = 0; i < mySequence.Count; i++)
        {
            mySequence[i].Kill();
            gamelist[i].position = vpos[0];
        }

        if (obj)
            return;
        obj = AssetManager.GetGameObject("sunwukong", this.transform);
        obj.layer = LayerMask.NameToLayer("NGUI");
        obj.name = "sunwukong";
        obj.transform.localPosition = new Vector3(-218f, -123f, 0f);
        obj.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);

        obj.GetComponent<SkeletonAnimation>().Play("win01", true);
    }

    IEnumerator LoadModel()
    {
        yield return StartCoroutine(AssetManager.LoadAsset("sunwukong", AssetManager.AssetType.Model, false));
    }

    public void OnClickSummonTips()
    {
        io.panelManager.CreatePanel(DialogType.SummonTips);
    }
}
