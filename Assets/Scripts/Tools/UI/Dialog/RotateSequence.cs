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

public class RotateSequence : MonoBehaviour {

    public List<Transform> gamelist = new List<Transform>();
    private List<Vector3> vpos = new List<Vector3>();
    private List<Sequence> mySequence = new List<Sequence>();


    private List<Vector3> lvpos = new List<Vector3>();
    private List<Sequence> lmySequence = new List<Sequence>();
    private List<Transform> lgamelist = new List<Transform>();

    public static string posfile = "file/pos";

    private Vector3 pos;
    private float xSpeed = 5.0f;

    public float speed = 3.0f;

	// Use this for initialization
    void Awake()
    {
        InitSequence();
    }
	void Start () 
    {
 
	}

    public void InitSequence()
    {
        InitSequencePos();
        RightRotateData();
        LeftRotateData();
    }

    private void  InitSequencePos()
    {
      JsonData[] data = JsonMapper.ToObject<JsonData[]>(PreLoadConfig.text);

       vpos.Clear();
       lvpos.Clear();

       int k = 0;
       foreach (JsonData jd in data)
       {
           Vector3 vec = Util.StrToVector3(jd["pos_" + k.ToString()].ToString(), ',');
            vpos.Add(vec);
            k++;
       }

        lvpos.Add(vpos[0]);
        for (int i = vpos.Count - 1; i > 0; i--)
        {
            lvpos.Add(vpos[i]);
        }

        lgamelist.Add(gamelist[0]);
        for (int i = gamelist.Count - 1; i > 0; i--)
        {
            lgamelist.Add(gamelist[i]);
        }
 
    }


    private void LeftRotateData()
    {
        for (int i = 0; i < lgamelist.Count; i++)
        {
            Sequence sequence = new Sequence(new SequenceParms().Loops(1000, LoopType.Restart));
            lmySequence.Add(sequence);
        }

        for (int i = 0; i < lmySequence.Count; i++)
        {
            for (int j = 0; j < lgamelist.Count - i; j++)
            {
                lmySequence[i].Append(HOTween.To(lgamelist[i], 1, new TweenParms().Prop("position", lvpos[j + i])));
            }

            if (i > 0)
            {
                for (int k = 0; k < i; k++)
                {
                    lmySequence[i].Append(HOTween.To(lgamelist[i], 1, new TweenParms().Prop("position", lvpos[k])));
                }
            }

            lmySequence[i].Append(HOTween.To(lgamelist[i], 1, new TweenParms().Prop("position", lvpos[i])));
        }    
    }

    private void RightRotateData()
    {
        for (int i = 0; i < gamelist.Count; i++)
        {
            Sequence sequence = new Sequence(new SequenceParms().Loops(1000, LoopType.Restart));
            mySequence.Add(sequence);
        }

        for (int i = 0; i < mySequence.Count; i++)
        {
            for (int j = 0; j < gamelist.Count - i; j++)
            {
                mySequence[i].Append(HOTween.To(gamelist[i], 1, new TweenParms().Prop("position", vpos[j + i])));
            }

            if (i > 0)
            {
                for (int k = 0; k < i; k++)
                {
                    mySequence[i].Append(HOTween.To(gamelist[i], 1, new TweenParms().Prop("position", vpos[k])));
                }
            }

            mySequence[i].Append(HOTween.To(gamelist[i], 1, new TweenParms().Prop("position", vpos[i])));
        }    
    }



    private void RightRotate()
    {
     
            for (int i = 0; i < mySequence.Count; i++)
            {
                mySequence[i].PlayForward();
                mySequence[i].timeScale = 2.0f;

                mySequence[i].loops = 10000;
            }
        
    }

    private void LeftRotate()
    {
        for (int i = 0; i < lmySequence.Count; i++)
        {
            lmySequence[i].PlayForward();
            lmySequence[i].timeScale = 2.5f;
            
            lmySequence[i].loops = 10000;
        }

        
    }

    private void PauseRotate()
    {
        for (int i = 0; i < lmySequence.Count; i++)
        {

            lmySequence[i].Pause();
        }

        for (int i = 0; i < mySequence.Count; i++)
        {
            mySequence[i].Pause();
        }
    }

	// Update is called once per frame
	void Update () 
    {

#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            PauseRotate();
        }
#else
        if (Input.touchCount > 0)
         {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                PauseRotate();
            }
// 
//             if (Input.GetTouch(0).phase == TouchPhase.Moved)
//             {
//                 //NGUIDebug.Log("Touch  moved");
//         
//                 pos = Input.GetTouch(0).deltaPosition;
// 
//                 if(pos.x > 0)
//                 {
//                     LeftRotate();
//                 }
//                 else if(pos.x <= 0f)
//                 {
//                     RightRotate();
//                 }
//             }
           }
#endif


}
    

    void OnDrag(Vector2 deta)
    {
        for (int i = 0; i < mySequence.Count; i++)
        {
            if (deta.x > 0)
            {
                mySequence[i].PlayBackwards();
                mySequence[i].timeScale = speed;
            }
            else if (deta.x < 0)
            {
                mySequence[i].PlayForward();
                mySequence[i].timeScale = speed;
            }
        }
    }
}
